using AutoMapper;
using MyEshopDemo.Models;
using MyEshopDemo.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEshopDemo.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Message, MessageDto>();
            Mapper.CreateMap<MessageDto, Message>();
        }
    }
}