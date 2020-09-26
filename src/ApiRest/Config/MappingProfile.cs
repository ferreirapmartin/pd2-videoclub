using ApiRest.Messages;
using ApiRest.Support;
using AutoMapper;
using Domain.Entities;

namespace ApiRest.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            AllowNullDestinationValues = true;
            DisableConstructorMapping();

            CreateMap<Rent, RentResponse>()
                .ForMember(i => i.ClientId, o => o.MapFrom(j => j.ClientId))
                .ForMember(i => i.ProductId, o => o.MapFrom(j => j.ProductId))
                .ForPath(i => i.Details.Status, o => o.MapFrom(j => StatusHelper.Parse(j.Status)))
                .ForPath(i => i.Details.Until, o => o.MapFrom(j => j.Until.HasValue ? j.Until.Value.ToString(DateHelper.Format) : null));
        }
    }
}
