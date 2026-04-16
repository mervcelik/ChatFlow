using Application.Features.Messages.Commands.Create;
using Application.Features.Messages.Commands.Update;
using Application.Features.Messages.Queries.GetListByRoom;
using AutoMapper;
using Core.Application.Dtos;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Messages.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Message, CreateMessageCommand>().ReverseMap();
        CreateMap<Message, CreatedMessageResponse>().ReverseMap();

        CreateMap<Message, UpdateMessageCommand>().ReverseMap();
        CreateMap<Message, UpdatedMessageResponse>().ReverseMap();

        CreateMap<Message, GetListMessageResponse>()
            .ForMember(dest => dest.ReadByCount, opt => opt.MapFrom(src => src.ReadBy.Count));

        CreateMap<ReadReceipt, ReadReceiptDto>().ReverseMap();

        CreateMap<Paginate<Message>, GetListResponse<GetListMessageResponse>>().ReverseMap();
    }
}
