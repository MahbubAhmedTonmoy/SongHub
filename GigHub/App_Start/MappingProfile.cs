using AutoMapper;
using GigHub.DTO;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile 
    {
        // Global.asax.cs tell automapper have a profile
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDTO>();
            Mapper.CreateMap<Gig, GigDTO>();
            Mapper.CreateMap<Notification, NotificationDTO>();
        }

    }
}