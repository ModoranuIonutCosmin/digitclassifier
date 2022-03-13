using Api.Middleware;
using Api.ResponseHelpers;
using Application.Exceptions;
using Application.Services;
using Application.Services.Implementation;
using DataAcces.Persistence.Context;
using DataAcces.Repositories;
using DataAcces.Repositories.Interfaces;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.Load(nameof(Application)));

            services.AddControllers();
            services.AddDbContext<DatabaseContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    //Console.WriteLine("Dev env");
                    connStr = Configuration.GetConnectionString("SqlServer");
                    options.UseSqlServer(connStr);
                }
                else
                {
                    //Console.WriteLine("Prod env");
                    // Use connection string provided at runtime by Heroku.
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                    //var connUrl = Configuration.GetConnectionString("DATABASE_URL");

                    Console.WriteLine(connUrl);

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];

                    connStr = $"Server={pgHost}; Port={pgPort}; User Id={pgUser}; Password={pgPass}; Database={pgDb}; SslMode=Require; Trust Server Certificate=true";
                    options.UseNpgsql(connStr);
                }
            });
            

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IHistoryRepository, HistoryRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IFavoritesService, FavoritesService>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IEmailSyntaxValidator, EmailSyntaxValidator>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IRatingService, RatingService>();

            services.AddSingleton<HttpClient>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });



            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddProblemDetails(options =>
            {
                options.Map<UserAlreadyExistsException>(details => 
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.Conflict));
                options.Map<UserEmailTakenException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.Conflict));
                options.Map<InvalidRatingValueException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.Conflict));
                options.Map<RatingAlreadyExistsException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.Conflict));

                options.Map<NullReferenceException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.BadRequest));
                options.Map<ArgumentException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.BadRequest));
                options.Map<NullReferenceException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.BadRequest));

                options.Map<AuthenticationFailedException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.Forbidden));

                options.Map<UserNotFoundException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.NotFound));
                options.Map<PredictionNotFoundException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.NotFound));
                options.Map<RatingDoesNotExistsException>(details =>
                    details.MapToProblemDetailsWithStatusCode(HttpStatusCode.NotFound));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }
        
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<ExceptionsMiddleware>();
            app.UseMiddleware<TokenMiddleware>();
            app.UseProblemDetails();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
