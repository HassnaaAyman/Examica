using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UI.Examica.API.Dtos;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Persistence;

namespace UI.Examica.API
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
            // ===== Add our DbContext ========
            services.AddDbContext<ExamicaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("UI.Examica.API")
                ));

            // ===== Add Identity ========
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ExamicaDbContext>()
                .AddDefaultTokenProviders();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // ===== Use UnitOfWork as a Dependecy Injection ========
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<RegisterDto, AppUser>();
                config.CreateMap<PricingPlanDto, PricingPlan>();
                config.CreateMap<PricingPlan, PricingPlanDto>();
                config.CreateMap<AddQuestionDto, Question>();
                config.CreateMap<OrganizationDto, Organization>();
                config.CreateMap<ResultDto, Result>();
                config.CreateMap<AppUser, UserDto>();
                // OrganizationAdmin to UserDto
                config.CreateMap<OrganizationAdmin, UserDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.AppUser.Id))
                .ForMember(dest => dest.UserName,
                        opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.PhoneNumber,
                        opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.AppUser.Email));
                // OrganizationExaminer to UserDto
                config.CreateMap<OrganizationExaminer, UserDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.AppUser.Id))
                .ForMember(dest => dest.UserName,
                        opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.PhoneNumber,
                        opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.AppUser.Email));
                // OrganizationExaminee to UserDto
                config.CreateMap<OrganizationExaminee, UserDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.AppUser.Id))
                .ForMember(dest => dest.UserName,
                        opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.PhoneNumber,
                        opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.AppUser.Email));
                // Organizationobserver to UserDto
                config.CreateMap<OrganizationObserver, UserDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.AppUser.Id))
                .ForMember(dest => dest.UserName,
                        opt => opt.MapFrom(src => src.AppUser.UserName))
                .ForMember(dest => dest.PhoneNumber,
                        opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.AppUser.Email));
                // OrganizationAdmin to OrganizationDto
                config.CreateMap<OrganizationAdmin, OrganizationDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Organization.Id))
                .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Organization.Name))
                .ForMember(dest => dest.Image,
                        opt => opt.MapFrom(src => src.Organization.Image))
                .ForMember(dest => dest.OwnerId,
                        opt => opt.MapFrom(src => src.Organization.OwnerId));
                // OrganizationExaminer to OrganizationDto
                config.CreateMap<OrganizationExaminer, OrganizationDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Organization.Id))
                .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Organization.Name))
                .ForMember(dest => dest.Image,
                        opt => opt.MapFrom(src => src.Organization.Image))
                .ForMember(dest => dest.OwnerId,
                        opt => opt.MapFrom(src => src.Organization.OwnerId));
                // OrganizationExaminee to OrganizationDto
                config.CreateMap<OrganizationExaminee, OrganizationDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Organization.Id))
                .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Organization.Name))
                .ForMember(dest => dest.Image,
                        opt => opt.MapFrom(src => src.Organization.Image))
                .ForMember(dest => dest.OwnerId,
                        opt => opt.MapFrom(src => src.Organization.OwnerId));
                // OrganizationObserver to OrganizationDto
                config.CreateMap<OrganizationObserver, OrganizationDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.Organization.Id))
                .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Organization.Name))
                .ForMember(dest => dest.Image,
                        opt => opt.MapFrom(src => src.Organization.Image))
                .ForMember(dest => dest.OwnerId,
                        opt => opt.MapFrom(src => src.Organization.OwnerId));

                // Map QuestionOption to OptionDTO
                config.CreateMap<QuestionOption, OptionDto>()
                .ForMember(dest => dest.Id,
                        opt => opt.MapFrom(src => src.OptionId))
                .ForMember(dest => dest.Name,
                        opt => opt.MapFrom(src => src.Option.Name));

                // Map Question to QuestionDTO
                config.CreateMap<Question, QuestionDto>()
               .ForMember(dest => dest.Options,
                       // Map Each QuestionOption in List to optionDTO
                       opt => opt.MapFrom(src => AutoMapper.Mapper.Map<List<QuestionOption>, List<OptionDto>>(src.QuestionOptions)));

                // Map QuestionComplexQuestion to QuestionDTO
                config.CreateMap<QuestionComplexQuestion, QuestionDto>()
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Question.Title))
                .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Question.Type))
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.IsPublic,
                opt => opt.MapFrom(src => src.Question.IsPublic))
                .ForMember(dest => dest.Level,
                opt => opt.MapFrom(src => src.Question.Level))
                .ForMember(dest => dest.Mark,
                opt => opt.MapFrom(src => src.Question.Mark))
               .ForMember(dest => dest.Options,
                opt => opt.MapFrom(src => AutoMapper.Mapper.Map<List<QuestionOption>, List<OptionDto>>(src.Question.QuestionOptions)));

                // Map ComplexQuestion to ComplexQuestionDTO
                config.CreateMap<ComplexQuestion, ComplexQuestionDto>()
               .ForMember(dest => dest.Questions,
                       opt => opt.MapFrom(src => AutoMapper.Mapper.Map<List<QuestionComplexQuestion>, List<QuestionDto>>(src.QuestionComplexQuestions)));

                config.CreateMap<ComplexQuestionDto, ComplexQuestion>();

                // Map Question Complex Question to Question DTO
                config.CreateMap<QuestionComplexQuestion, ComplexQuestionDto>();


                // Map ExamQuestion to QuestionDTO
                config.CreateMap<ExamQuestion, QuestionDto>()
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Question.Title))
                .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.Question.Type))
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dest => dest.IsPublic,
                opt => opt.MapFrom(src => src.Question.IsPublic))
                .ForMember(dest => dest.Level,
                opt => opt.MapFrom(src => src.Question.Level))
                .ForMember(dest => dest.Mark,
                opt => opt.MapFrom(src => src.Question.Mark))
               .ForMember(dest => dest.Options,
                opt => opt.MapFrom(src => AutoMapper.Mapper.Map<List<QuestionOption>, List<OptionDto>>(src.Question.QuestionOptions)));


                // Complex Question To List of Question DTO
                config.CreateMap<ComplexQuestion, List<QuestionDto>>()
                    .ConvertUsing(src => src.QuestionComplexQuestions.Select(qcq =>
                           AutoMapper.Mapper.Map<Question, QuestionDto>(qcq.Question)
                        ).ToList()
                    );

                // Map Exam Complex Question to Complex Question DTO
                config.CreateMap<ExamComplexQuestion, ComplexQuestionDto>()
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.ComplexQuestion.Title))
                .ForMember(dest => dest.OrganizationId,
                opt => opt.MapFrom(src => src.ComplexQuestion.OrganizationId))
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.ComplexQuestionId))
                .ForMember(dest => dest.IsPublic,
                opt => opt.MapFrom(src => src.ComplexQuestion.IsPublic))
                .ForMember(dest => dest.Questions,
                     opt => opt.MapFrom(src => AutoMapper.Mapper.Map<ComplexQuestion, List<QuestionDto>>(src.ComplexQuestion)));

                // Map Exam to ExamDto
                config.CreateMap<Exam, ExamDto>()
                .ForMember(dest => dest.Questions,
                opt => opt.MapFrom(src => AutoMapper.Mapper.Map<List<ExamQuestion>, List<QuestionDto>>(src.ExamQuestions)))
                .ForMember(dest => dest.ComplexQuestions,
                opt => opt.MapFrom(src => AutoMapper.Mapper.Map<List<ExamComplexQuestion>, List<ComplexQuestionDto>>(src.ExamComplexQuestions)));
            });



            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
