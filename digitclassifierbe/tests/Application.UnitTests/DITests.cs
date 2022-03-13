using Application.Services;
using Application.Services.Implementation;
using DataAcces.Persistence.Context;
using DataAcces.Repositories;
using DataAcces.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Application.UnitTests.Mock_Helpers;

namespace Application.UnitTests
{
    public class DITests : IDisposable
    {
        private ServiceProvider serviceProvider;

        public DITests()
        {
            var services = InitializeCommonServices();

            serviceProvider = services.BuildServiceProvider();

            DatabaseInitializer.Initialize(ResolveService<DatabaseContext>());
        }

        public ServiceCollection InitializeCommonServices()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(Assembly.Load(nameof(Application)));
            services.AddDbContext<DatabaseContext>(options => {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                options.EnableSensitiveDataLogging();
            }
            );
            services.AddTransient<IEmailSyntaxValidator, EmailSyntaxValidator>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IHistoryRepository, HistoryRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();

            services.AddTransient<IRatingService, RatingService>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IFavoritesService, FavoritesService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IRatingService, RatingService>();

            return services;
        }

        public T ResolveService<T>()
        {
            return serviceProvider.GetService<T>();
        }

        public void AddMockHttpClient(object mockResponseValue)
        {
            serviceProvider.Dispose();

            var newServiceCollection = InitializeCommonServices();
            newServiceCollection.AddMockHttpClient(mockResponseValue);

            serviceProvider = newServiceCollection.BuildServiceProvider();
        }

        public void Dispose()
        {
            ResolveService<DatabaseContext>().Database.EnsureDeleted();
            serviceProvider.Dispose();
            GC.Collect();
        }

    }
}
