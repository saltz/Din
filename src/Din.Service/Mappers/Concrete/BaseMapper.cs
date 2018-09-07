using AutoMapper;

namespace Din.Service.Mappers.Concrete
{
    public abstract class BaseMapper
    {
        private Mapper Mapper { get; set; }
        public IRuntimeMapper Instance {get;}

        protected BaseMapper(MapperConfiguration config)
        {
            Mapper = new Mapper(config);
            Instance = Mapper.DefaultContext.Mapper;
        }
    }
}
