using AutoMapper;

namespace BLL
{
    public class AutoMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression config)
        {
            //ConfigureDALToEntity(config);
            //ConfigureDALToDAL(config);
            ConfigureEntityToEntity(config);
        }
        public static void ConfigureEntityToEntity(IMapperConfigurationExpression config)
        {
            //config.CreateMap<Entity.Meeting_ModelView, Entity.Meeting_ModelView>().ReverseMap();
            //config.CreateMap<Entity.Meeting, Entity.Meeting>().ReverseMap();
        }
    }
}
