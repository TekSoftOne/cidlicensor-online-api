using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using OR.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OR.CloudStorage.Configurations;
using OR.CloudStorage;
using OR.Web.Twilio;
using AutoMapper;

namespace OR.Web
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public SymmetricSecurityKey SigningKey { get; set; }
    }

    public class Startup
    {
        readonly string _corsAllowList = "_corsAllowList";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AzureBlobOptions>(Configuration.GetSection("BlobStorage"));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("OR.Web")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<Configuration.Twilio>(Configuration.GetSection("Twilio"));

            services.AddRazorPages();

            var mappingConfig = new MapperConfiguration(mapper =>
           {
               mapper.AddProfile(new MapperProfile());
           });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IDataFactory, DataFactory>();

            services.AddScoped<IBlobStorageService, BlobStorageService>();

            services.AddScoped<IRequestStorageManager, RequestStorageManager>();

            services.AddScoped<IVerification, Verification>();

            services.AddControllers().AddNewtonsoftJson();
            //Simplify the password requirements
            services.AddIdentityCore<IdentityUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            var jwtOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var issuer = "onlineRequestApi";
            var audience = jwtOptions[nameof(JwtIssuerOptions.Audience)];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("-t#B7@-@np%_kuEms7(6FPS6Zr3tS8mZ)FKq;T"));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audience;
                options.SigningKey = signingKey;
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

            };

            services.AddCors(options => options.AddPolicy(_corsAllowList, p => p.WithOrigins(
                "http://localhost:4900",
                "http://onlinerequest-api.cidlicensor.support",
                "https://onlinerequest-api.cidlicensor.support",
                "http://onlinerequest.cidlicensor.support",
                  "https://onlinerequest.cidlicensor.support")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
                                                                 ));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Online Request Apis", Version = "v1" });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = issuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Request");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors(_corsAllowList);
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
