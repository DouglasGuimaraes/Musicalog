using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Models.Interfaces.Service;
using Models.ViewModel;
using Repository.Context;
using Repository.Repositories;
using Services;
using System.Configuration;

namespace Infrastructure.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAlbumService, AlbumService>();
            return services;
        }

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<AlbumRepository>();
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AlbumViewModel, Album>();
                cfg.CreateMap<Album, AlbumViewModel>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
