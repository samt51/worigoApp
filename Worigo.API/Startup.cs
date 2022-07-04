using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Worigo.API.Filter;
using Worigo.API.Middlewares;
using Worigo.Business.Abstrack;
using Worigo.Business.Concrete;
using Worigo.Core.Dtos.ListDto;
using Worigo.Core.FluentValidation;
using Worigo.Core.Mapping;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.Concrete.Entity_Framwork;

namespace Worigo.API
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
            services.AddMvc(x =>
            {
                x.Filters.Add(typeof(ValidatorActionFilter));
            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
              .AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });
            services.AddCors();
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
 
            var tokenKey = Configuration.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Worigo.API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(IProfile));
            services.AddScoped<IValidator<HotelDto>, HotelValidator> ();
            services.AddScoped<IValidator<AllImagesDto>, AllImagesValidator> ();
            services.AddScoped<IValidator<CommentDto>, CommentValidator> ();
            services.AddScoped<IValidator<DepartmanDto>, DepartmanValidator> ();
            services.AddScoped<IValidator<EmployeesDto>, EmployeesValidator> ();
            services.AddScoped<IValidator<GeneralServiceDto>, GeneralServiceValidator> ();
            services.AddScoped<IValidator<RoomDto>, RoomValidator> ();
            services.AddScoped<IValidator<ServicesDto>, ServicesValidator>();
            services.AddScoped<IValidator<RoomTypeDto>, RoomTypeValidator>();
            services.AddScoped<IValidator<UserRoleDto>, UserRoleValidator>();
            services.AddScoped<IValidator<VertificationCodeDto>, VertificationCodeValidator>();
     
            services.AddScoped<IAllImagesDal, EfAllImagesDal>();
            services.AddScoped<IAllImagesService, AllImagesManager>();

            services.AddScoped<IGeneralServiceAndServiceDal, EfIGeneralServiceAndServiceDal>();
            services.AddScoped<IGeneralServiceAndServiceService, GeneralServiceAndServiceManager>();

            services.AddScoped<IUserRoleDal, EfUserRoleDal>();
            services.AddScoped<IUserRoleService, UserRoleManager>();

            services.AddScoped<ICommentDal, EfCommentDal>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<IDepartmanDal, EfDepartmanDal>();
            services.AddScoped<IDepartmanService, DepartmanManager>();

            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IUserService, UserManager>();

            services.AddScoped<IEmployeesDal, EfEmployeesDal>();
            services.AddScoped<IEmployeesService, EmployeesManager>();

            services.AddScoped<IGeneralServiceDal, EfGeneralServiceDal>();
            services.AddScoped<IGeneralServiceService, GeneralServiceManager>();

            services.AddScoped<IHotelDal, EfHotelDal>();
            services.AddScoped<IHotelService, HotelManager>();

            services.AddScoped<IRoomDal, EfRoomDal>();
            services.AddScoped<IRoomService, RoomManager>();

            services.AddScoped<IServicesDal, EfServiceDal>();
            services.AddScoped<IServicesService, ServicesManager>();

            services.AddScoped<IRoomTypeDal, EfRoomTypeDal>();
            services.AddScoped<IRoomTypeService, RoomTypeManager>();

            services.AddScoped<IVertificationCodeDal, EfVertificationCodeDal>();
            services.AddScoped<IVertificationCodeService, VertificationCodeManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Worigo.API v1"));
            }
            app.UseHttpsRedirection();
            app.UseCustomException();
            //app.ConfigureExceptionHanler();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();
            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
