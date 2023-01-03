using Microsoft.Extensions.Configuration;
using EmployeeAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using EmployeeAPI.MappingProfile;
using EmployeeAPI.Identity;
using Microsoft.AspNetCore.Identity;
using EmployeeAPI.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using EmployeeAPI.ServiceContract;
using EmployeeAPI.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeAPI
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
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("conStr"),b => b.MigrationsAssembly("EmployeeAPI")));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("conStr")));
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IUserService, UserService>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddDefaultTokenProviders();

            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddControllers();
            services.AddAutoMapper(typeof(DtoMappingProfile));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeAPI", Version = "v1" });
            });
            
            //JWT Authentication
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSetting>(appSettingsSection);
            var appSetting = appSettingsSection.Get<AppSetting>();
            var key = Encoding.ASCII.GetBytes(appSetting.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44347/")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeAPI v1"));
            }
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //// Data
            //IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.
            //    GetRequiredService<IServiceScopeFactory>();
            //using (IServiceScope scope = serviceScopeFactory.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService
            //        <RoleManager<ApplicationRole>>();
            //    var userManager = scope.ServiceProvider.GetRequiredService
            //        <UserManager<ApplicationUser>>();
            //    //Create Admin Role
            //    if (!await roleManager.RoleExistsAsync("Admin"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Admin";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create Employee Role
            //    if (!await roleManager.RoleExistsAsync("Employee"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Employee";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create Admin User

            //    if (await userManager.FindByNameAsync("admin") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "admin";
            //        user.Email = "admin@gmail.com";
            //        var userPassword = "Admin@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Admin");
            //        }
            //    }

            //    //Create Employee User

            //    if (await userManager.FindByNameAsync("employee") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "employee";
            //        user.Email = "employee@gmail.com";
            //        var userPassword = "Admin@123";
            //        var chkuser = await userManager.CreateAsync(user, userPassword);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Employee");
            //        }
            //    }
            //}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}