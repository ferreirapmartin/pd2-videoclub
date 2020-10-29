using ApiRest.Messages;
using ApiRest.Support;
using AutoMapper;
using DataAccess.Contexts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ApiRest.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RentController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;
        private readonly IRentXmlSchemaValidator rentXmlSchemaValidator;

        public RentController(IServiceProvider serviceProvider, IMapper mapper, IRentXmlSchemaValidator rentXmlSchemaValidator)
        {
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
            this.rentXmlSchemaValidator = rentXmlSchemaValidator;
        }

        private async Task<Rent> GetRent(IVideoclubDbContext ctx, Guid productId, Guid clientId)
            => await ctx.Rents.Where(i => i.ClientId == clientId && i.ProductId == productId)
                                      .FirstOrDefaultAsync()
                                      .ConfigureAwait(false);


        private async Task<string> GetBody()
        {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        private RentRequest DeserializeXml(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(RentRequest));
            using TextReader reader = new StringReader(xmlString);
            return (RentRequest)serializer.Deserialize(reader);
        }

        [HttpGet("{productId}/{clientId}")]
        public async Task<IActionResult> Get(Guid productId, Guid clientId)
        {
            using var ctx = serviceProvider.GetService<IVideoclubDbContext>();
            var rent = await GetRent(ctx, productId, clientId).ConfigureAwait(false);

            if (rent is null)
                return NotFound();

            return Ok(mapper.Map<RentResponse>(rent));
        }

        [HttpPut]
        [Consumes("application/json", "application/xml")]
        public async Task<IActionResult> Put([FromBody]RentRequest request)
        {
            var productId = Guid.Parse(request.ProductId);
            var clientId = Guid.Parse(request.ClientId);
            var status = StatusHelper.Parse(request.Details?.Status);
            var until = request.Details?.Until != null ? DateHelper.Parse(request.Details.Until) : (DateTime?)null;

            using var ctx = serviceProvider.GetService<IVideoclubDbContext>();
            var rent = await GetRent(ctx, productId, clientId).ConfigureAwait(false);
            var isNew = rent is null;

            if (isNew)
            {
                rent = new Rent(productId, clientId, status, until);
                ctx.Rents.Add(rent);
            }
            else
            {
                rent.ChangeStatus(status, until);
            }
            await ctx.SaveChangesAsync().ConfigureAwait(false);

            var result = mapper.Map<RentResponse>(rent);

            return isNew ? Created(UrlUtls.URI(this, nameof(RentController), nameof(RentController.Get), new { productId, clientId, version = "1.0" }), result)
                         : (IActionResult)Ok(result);
        }

        [HttpPut]
        [MapToApiVersion("2.0")]
        [Consumes("application/xml")]
        public async Task<IActionResult> PutV2()
        {
            var xmlBody = await GetBody().ConfigureAwait(false);
            var errors = rentXmlSchemaValidator.Validate(xmlBody);
            if (errors.Any())
            {
                foreach (var error in errors)
                    ModelState.AddModelError(string.Empty, error);
                return ValidationProblem();
            }
            
            return await Put(DeserializeXml(xmlBody)).ConfigureAwait(false);
        }
    }
}
