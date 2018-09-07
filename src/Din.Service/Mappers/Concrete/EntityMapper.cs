using AutoMapper;
using Din.Service.Mappers.Interfaces;

namespace Din.Service.Mappers.Concrete
{
    public class EntityMapper : BaseMapper, IEntityMapper
    {
        public EntityMapper(MapperConfiguration config) : base(config)
        {
        }
    }
}