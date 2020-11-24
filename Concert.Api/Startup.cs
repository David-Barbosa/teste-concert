using System;
using System.Linq;
using System.Text;
using Concert.Api.Configurations;
using Concert.Api.Security;
using Concert.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


namespace Concert.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private const string ISSUER = "c1f51f42";
        private const string AUDIENCE = "c6bbbb645024";
        private const string SECRET_KEY = "c1f51f42-5727-4d15-b787-c6bbbb645024";

        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.AddControllers();

            services.AddCors();
            services.AddMvcCore(opt =>
            {
                opt.UseCentralRoutePrefix(new RouteAttribute("api/v1"));
            }).AddApiExplorer();

            //services.AddMvcCore().AddApiExplorer();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ISSUER,

                        ValidateAudience = true,
                        ValidAudience = AUDIENCE,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = _signingKey,

                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = ISSUER;
                options.Audience = AUDIENCE;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireClaim("Concert", "User"));
                options.AddPolicy("Admin", policy => policy.RequireClaim("Concert", "Admin"));
            });

            services.AddResponseCompression();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Concert Api", Version = "v1" });
            });

            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Concert - V1");
            });

            Runtime.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
