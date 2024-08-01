using ApplicationLogic.Models;
using AutoMapper;
using SWIFT_MT799_Logic;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SwiftMT799Message, SWIFT_MT799_Message_Model>();
    }
}