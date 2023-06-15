using _1CService.Application.DTO;
using _1CService.Application.Models.Responses.Queries;
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
            CreateMap<BlankOrder, ResponseBlankOrderDetailDTO>()
                .ForMember(response => response.Number, opt => opt.MapFrom(blankorder => blankorder.Number))
                .ForMember(response => response.Date, opt => opt.MapFrom(blankorder => blankorder.Date))
                .ForMember(response => response.Manager, opt => opt.MapFrom(blankorder => blankorder.Manager))
                .ForMember(response => response.Contragent, opt => opt.MapFrom(blankorder => blankorder.Contragent))
                .ForMember(response => response.Urgency, opt => opt.MapFrom(blankorder => blankorder.Urgency))
                .ForMember(response => response.CompletionDate, opt => opt.MapFrom(blankorder => blankorder.CompletionDate))
                .ForMember(response => response.ExecuteState, opt => opt.MapFrom(blankorder => blankorder.ExecuteState))
                .ForMember(response => response.ImagePreview, opt => opt.MapFrom(blankorder => blankorder.ImagePreview))
                .ForMember(response => response.Materials, opt => opt.MapFrom(blankorder => blankorder.Materials))
                .ForMember(response => response.Products, opt => opt.MapFrom(blankorder => blankorder.Products))
                .ForMember(response => response.Comments, opt => opt.MapFrom(blankorder => blankorder.Comments));

            CreateMap<BlankOrder, BlankOrderDTO>()
                .ForMember(response => response.Number, opt => opt.MapFrom(blankorder => blankorder.Number))
                .ForMember(response => response.Date, opt => opt.MapFrom(blankorder => blankorder.Date))
                .ForMember(response => response.Manager, opt => opt.MapFrom(blankorder => blankorder.Manager))
                .ForMember(response => response.Contragent, opt => opt.MapFrom(blankorder => blankorder.Contragent))
                .ForMember(response => response.Urgency, opt => opt.MapFrom(blankorder => blankorder.Urgency))
                .ForMember(response => response.CompletionDate, opt => opt.MapFrom(blankorder => blankorder.CompletionDate))
                .ForMember(response => response.ExecuteState, opt => opt.MapFrom(blankorder => blankorder.ExecuteState));
        }
    }
}
