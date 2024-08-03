using Core.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace infrastrcture.Mappings
{
    public class ChatMappingProfile :Profile
    {
        public ChatMappingProfile()
        {
            CreateMap<Chat, ChatMappingProfile>();
            CreateMap<ChatMappingProfile, Chat>();
        }
    }
}
