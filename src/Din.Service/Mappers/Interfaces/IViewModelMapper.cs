using AutoMapper;

namespace Din.Service.Mappers.Interfaces
{
    public interface IViewModelMapper
    {
        IRuntimeMapper Instance { get; }
    }
}