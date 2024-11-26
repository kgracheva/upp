using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using upp.Dtos.Chat;
using upp.Entities;

namespace MagicAds.Application.Mapping;
public class ChatMapper : Profile
{
    public ChatMapper()
    {
        CreateMap<Chat, ChatDto>()
            .ForMember(dest => dest.Users, opt => opt.MapFrom(e => e.ChatUsers));

        CreateMap<ChatUser, ChatUserDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(e => e.User.Info.Lastname + " " + e.User.Info.Name) );
            
        CreateMap<Message, MessageDto>();
    }
}
