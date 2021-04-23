using AutoMapper;
using desafio_rdi.domain.Models;
using desafio_rdi.webapi.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace desafio_rdi.webapi.Configuration
{
    public static class RegisterAutoMapper
    {
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CardDto, CustomerCardRequest>().ReverseMap();
                cfg.CreateMap<CardResponseDto, CustomerCard>()
                                        .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
                                        .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                                        .ForMember(dest => dest.CardId, opt => opt.MapFrom(src => src.CardId)).ReverseMap();

                cfg.CreateMap<ValidateCardRequestDto, ValidateCard>().ReverseMap();

            });
            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
