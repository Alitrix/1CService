using _1CService.Application.DTO;
using _1CService.Application.DTO.Responses.Queries;
using _1CService.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1CService.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlankOrder, ResponseBlankOrderDetailDTO>();
                //.ForMember(response=>response.Number, 
                    //opt=>opt.MapFrom(blank=>blank.Number));

            CreateMap<BlankOrder, BlankOrderDTO>();
        }
    }
}
