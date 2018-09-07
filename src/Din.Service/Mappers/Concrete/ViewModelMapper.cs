using AutoMapper;
using Din.Service.Mappers.Interfaces;

namespace Din.Service.Mappers.Concrete
{
    public class ViewModelMapper : BaseMapper, IViewModelMapper
    {
        public ViewModelMapper(MapperConfiguration config) : base(config)
        {
        }
    }
}
