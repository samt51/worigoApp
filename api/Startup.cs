using CorePush.Apple;
using CorePush.Google;
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
using Worigo.Core;
using Worigo.Core.Mapping;
using Worigo.DataAccess.Abstrack;
using Worigo.DataAccess.Concrete;
using Worigo.DataAccess.Concrete.Entity_Framwork;
using static Worigo.Core.INotificationService;

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
            //#region Validators
            //services.AddScoped<IValidator<HotelDto>, HotelValidator>();
            //services.AddScoped<IValidator<AllImagesDto>, AllImagesValidator>();
            //services.AddScoped<IValidator<CommentDto>, CommentValidator>();
            //services.AddScoped<IValidator<DepartmanDto>, DepartmanValidator>();
            //services.AddScoped<IValidator<EmployeesDto>, EmployeesValidator>();
            //services.AddScoped<IValidator<ServiceValueDto>, ServiceValueValidator>();
            //services.AddScoped<IValidator<RoomDto>, RoomValidator>();
            //services.AddScoped<IValidator<ServicesDto>, ServicesValidator>();
            //services.AddScoped<IValidator<RoomTypeDto>, RoomTypeValidator>();
            //services.AddScoped<IValidator<UserRoleDto>, UserRoleValidator>();
            //services.AddScoped<IValidator<VertificationCodeDto>, VertificationCodeValidator>();
            //services.AddScoped<IValidator<CompaniesDto>, CompaniesValidator>();
            //services.AddScoped<IValidator<EmployeesTypeDto>, EmployeesTypeValidator>();
            //services.AddScoped<IValidator<AddHotelAdminModelDto>, HotelAdminAddValidator>();
            //services.AddScoped<IValidator<ManagementAddDto>, ManagementAddDtoValidator>();
            //services.AddScoped<IValidator<ServiceOfHotelDto>, ServiceOfHotelValidator>();
            //services.AddScoped<IValidator<AddNewMenuRequest>, NewMenuAddValidator>();
            //services.AddScoped<IValidator<OrderRequestDto>, OrderAddValidator>();

            //#endregion

            #region Dependecy Injections

            //services.AddScoped<IServiceValueOfEmployeeTypeDal, EfServiceValueOfEmployeeTypeDal>();
            //services.AddScoped<IServiceValueOfEmployeeTypeService, ServiceValueOfEmployeeTypeManager>();
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();

            services.AddTransient<IEmployeeOfOrderDal, EfEmployeeOfOrderDal>();
            services.AddTransient<IEmployeeOfOrderService, EmployeeOfOrdersManager>();

            services.AddTransient<INotificationService, NotificationService>();



            services.AddTransient<ICustomerDal, EfCustomerDal>();
            services.AddTransient<ICustomerService, CustomerManager>();


            services.AddTransient<IOrderDal, EfOrderDal>();
            services.AddTransient<IOrderService, OrderManager>();

            services.AddTransient<IContentsOfFoodDal, EfContentsOfFoodDal>();
            services.AddTransient<IContentsOfFoodService, ContentsOfFoodManager>();

            services.AddTransient<IOrderDal, EfOrderDal>();
            services.AddTransient<IOrderService, OrderManager>();

            services.AddTransient<IFoodMenuDal, EfFoodMenuDal>();
            services.AddTransient<IFoodMenuService, FoodMenuManager>();

            services.AddTransient<IFoodMenuDetailDal, EfFoodMenuDetailDal>();
            services.AddTransient<IFoodMenuDetailService, FoodMenuDetailManager>();

            services.AddTransient<IDirectorsDepartmansDal, EfDirectorsDepartmansDal>();
            services.AddTransient<IDirectorsDepartmansService, DirectorsDepartmansManager>();

            services.AddTransient<ITasksOfEmployeesDal, EfTasksOfEmployeesDal>();
            services.AddTransient<ITasksOfEmployeesService, TasksOfEmployeesManager>();

            services.AddTransient<IUserRoleDal, EfUserRoleDal>();
            services.AddTransient<IUserRoleService, UserRoleManager>();

            services.AddTransient<ICommentDal, EfCommentDal>();
            services.AddTransient<ICommentService, CommentManager>();

            services.AddTransient<IDepartmanDal, EfDepartmanDal>();
            services.AddTransient<IDepartmanService, DepartmanManager>();

            services.AddTransient<IUserDal, EfUserDal>();
            services.AddTransient<IUserService, UserManager>();

            services.AddTransient<IEmployeesDal, EfEmployeesDal>();
            services.AddTransient<IEmployeesService, EmployeesManager>();

            services.AddTransient<IHotelDal, EfHotelDal>();
            services.AddTransient<IHotelService, HotelManager>();

            services.AddTransient<IRoomDal, EfRoomDal>();
            services.AddTransient<IRoomService, RoomManager>();

            services.AddTransient<IServicesDal, EfServiceDal>();
            services.AddTransient<IServicesService, ServicesManager>();

            services.AddTransient<IHotelOfServiceDal, EfHotelOfServiceDal>();
            services.AddTransient<IHotelOfServicesService, HotelOfServiceManager>();

            services.AddTransient<IServicesValuesDal, EfServicesValueDal>();
            services.AddTransient<IServiceValueService, ServiceValueManager>();

            services.AddTransient<IServiceOfValueDal, EfServiceOfValueDal>();
            services.AddTransient<IServiceOfValueService, ServiceOfValueManager>();

            services.AddTransient<IRoomTypeDal, EfRoomTypeDal>();
            services.AddTransient<IRoomTypeService, RoomTypeManager>();

            services.AddTransient<IVerificationCodeDal, EfVerificationCodeDal>();
            services.AddTransient<IVerificationCodeService, VerificationCodeManager>();

            services.AddTransient<IAllImagesDal, EfAllImagesDal>();
            services.AddTransient<IAllImagesService, AllImagesManager>();

            services.AddTransient<ICompaniesDal, EfCompaniesDal>();
            services.AddTransient<ICompaniesService, CompaniesManager>();

            services.AddTransient<IEmployeesTypeDal, EfEmployeesTypeDal>();
            services.AddTransient<IEmployeesTypeService, EmployeesTypeManager>();

            services.AddTransient<IManagementOfHotelsDal, EfManagementOfHotelsDal>();
            services.AddTransient<IManagementOfHotelService, ManagementOfHotelManager>();

            #endregion


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
