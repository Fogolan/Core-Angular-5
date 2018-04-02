using System;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PartyPlanner.Api.Services.User.Models;
using PartyPlanner.Data.Entities;
using PartyPlanner.Data.Models;
using PartyPlanner.Infrastructure.Contants;
using StructureMap;

namespace PartyPlanner.Web.Api
{
  public class Startup
  {
    private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure

    private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey))
      ;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      string connection = Configuration.GetConnectionString("DefaultConnection");
      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

      // Configure JwtIssuerOptions
      services.Configure<JwtIssuerOptions>(options =>
      {
        options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
        options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
        options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
      });
      services.AddDbContext<PartyPlannerContext>(options =>
        options.UseSqlServer(connection));
      services.AddCors();
      services.AddAuthorization(options =>
      {
        options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
          .RequireAuthenticatedUser().Build();
      });
      services.AddAuthorization(options =>
      {
        options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
      });
      services.AddIdentity<UserIdentity, IdentityRole>
        (o =>
        {
          // configure identity options
          o.Password.RequireDigit = false;
          o.Password.RequireLowercase = false;
          o.Password.RequireUppercase = false;
          o.Password.RequireNonAlphanumeric = false;
          o.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<PartyPlannerContext>()
        .AddDefaultTokenProviders();
      services.AddMvc()
        .AddControllersAsServices();
      services.AddApplicationInsightsTelemetry(Configuration);
      return ConfigureIoC(services);
    }


    public IServiceProvider ConfigureIoC(IServiceCollection services)
    {
      var container = new Container();

      container.Configure(config =>
      {
        // Register stuff in container, using the StructureMap APIs...
        config.Scan(_ =>
        {
          _.AssemblyContainingType(typeof(Startup));
          _.AssembliesFromPath(Environment.CurrentDirectory);
          _.WithDefaultConventions();
          _.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
          _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>)); // Handlers with no response
          _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>)); // Handlers with a response
          _.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
          _.ConnectImplementationsToTypesClosing(typeof(IPipelineBehavior<,>));
          _.WithDefaultConventions();
        });

        config.For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
        config.For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
        config.For<IMediator>().Use<Mediator>();

        //Populate the container using the service collection
        config.Populate(services);
      });

      return container.GetInstance<IServiceProvider>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

        ValidateAudience = true,
        ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _signingKey,

        RequireExpirationTime = false,
        ValidateLifetime = false,
        ClockSkew = TimeSpan.Zero
      };
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseMvc();
    }
  }
}
