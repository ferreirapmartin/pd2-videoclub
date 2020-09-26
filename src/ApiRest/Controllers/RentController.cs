using ApiRest.Messages;
using ApiRest.Support;
using AutoMapper;
using DataAccess.Contexts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IMapper mapper;

        public RentController(IServiceProvider serviceProvider, IMapper mapper)
        {
            this.serviceProvider = serviceProvider;
            this.mapper = mapper;
        }

        private async Task<Rent> GetRent(IVideoclubDbContext ctx, Guid productId, Guid clientId)
            => await ctx.Rents.Where(i => i.ClientId == clientId && i.ProductId == productId)
                                      .FirstOrDefaultAsync()
                                      .ConfigureAwait(false);


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

            return isNew ? Created(UrlUtls.URI(this, nameof(RentController), nameof(RentController.Get), new { productId, clientId }), result)
                         : (IActionResult)Ok(result);
        }
    }
}
