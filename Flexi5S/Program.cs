//using Flexi5S.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using MongoDB.Driver;

//internal class Program
//{
//    private static void Main(string[] args)
//    {

//        const string connectionUri = "mongodb+srv://kiranbk:<kiranbk>@kiketcl.a5r9mnr.mongodb.net/?retryWrites=true&w=majority&appName=KiketCL";

//        var builder = WebApplication.CreateBuilder(args);

//        // Add services to the container.
//        // Add Authentication services to the DI container


//        builder.Services.AddCors(options =>
//        {
//            options.AddPolicy("AllowLocalhost", builder =>
//            {
//                builder.WithOrigins("http://localhost:3000") // Your frontend URL
//                    .AllowAnyHeader()
//                    .AllowAnyMethod();
//            });
//        });



//        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(options =>
//            {
//                options.Authority = "dev-fzf1ql1knabosj7z.us.auth0.com"; // Replace with your Auth0 domain
//                                                                         //options.Audience = "YOUR_API_IDENTIFIER"; // This is the API identifier you set in Auth0
//            });

//        builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
//        {
//            return new MongoClient(connectionUri);
//        });
//        builder.Services.AddSingleton<MongoDBServices>();



//        builder.Services.AddControllers();
//        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//        builder.Services.AddEndpointsApiExplorer();
//        builder.Services.AddSwaggerGen();

//        var app = builder.Build();

//        // Configure the HTTP request pipeline.
//        if (app.Environment.IsDevelopment())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI();
//        }

//        app.UseHttpsRedirection();
//        app.UseCors("AllowLocalhost");
//        app.UseAuthentication();
//        app.UseAuthorization();

//        app.MapControllers();

//        app.Run();
//    }
//}


//using Flexi5S.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using MongoDB.Driver;

//internal class Program
//{
//    private static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);

//        const string connectionUri =  "mongodb+srv://kiranbk:<kiranbk>@kiketcl.a5r9mnr.mongodb.net/?retryWrites=true&w=majority&appName=KiketCL";

//        // Add services to the container.
//        builder.Services.AddCors(options =>
//        {
//            options.AddPolicy("AllowLocalhost", builder =>
//            {
//                //builder.SetIsOriginAllowed(origin => origin.Contains("localhost"))
//                builder.WithOrigins("http://localhost:3000")
//                       .AllowAnyHeader()
//                       .AllowAnyMethod();
//            });
//        });

//        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(options =>
//            {
//                options.Authority = "https://dev-fzf1ql1knabosj7z.us.auth0.com";
//                options.Audience = "https://localhost:7275/"; // API Identifier in Auth0


//            });

//        builder.Services.AddSwaggerGen(options =>
//        {
//            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//            {
//                Title = "Flexi5 API",
//                Version = "v1"
//            });

//            // Add JWT Authentication configuration
//            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//            {
//                Name = "Authorization",
//                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
//                Scheme = "Bearer",
//                BearerFormat = "JWT",
//                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//                Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
//            });

//            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//            {
//                {
//                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//                    {
//                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                        {
//                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    },
//                    new string[] { }
//                }
//            });
//        });


//        builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
//        {
//            return new MongoClient(connectionUri);
//        });
//        builder.Services.AddSingleton<MongoDBServices>();

//        builder.Services.AddControllers();
//        builder.Services.AddEndpointsApiExplorer();
//        builder.Services.AddSwaggerGen();

//        builder.Logging.ClearProviders();
//        builder.Logging.AddConsole();

//        var app = builder.Build();

//        // Error handling middleware
//        app.Use(async (context, next) =>
//        {
//            try
//            {
//                await next();
//            }
//            catch (Exception ex)
//            {
//                context.Response.StatusCode = 500;
//                await context.Response.WriteAsync($"Internal Server Error: {ex.Message}");
//            }
//        });

//        // Configure the HTTP request pipeline.
//        if (app.Environment.IsDevelopment())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI();
//        }

//        app.UseHttpsRedirection();
//        app.UseCors("AllowLocalhost");
//        app.UseAuthentication();
//        app.UseAuthorization();

//        app.MapControllers();

//        app.Run();
//    }
//}



using Flexi5S.Authorization;
using Flexi5S.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Security.Claims;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        const string connectionUri = "mongodb+srv://kiranbk:<kiranbk>@kiketcl.a5r9mnr.mongodb.net/?retryWrites=true&w=majority&appName=KiketCL";

        // Add services to the container.
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost", builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });

        //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options =>
        //    {
        //        options.Authority = "https://dev-fzf1ql1knabosj7z.us.auth0.com"; // Auth0 domain
        //        options.Audience = "https://localhost:7275/"; // API Identifier
        //    });

        // 1. Add Authentication Services
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).AddJwtBearer(options =>
        //{
        //    options.Authority = "https://dev-fzf1ql1knabosj7z.us.auth0.com/";
        //    options.Audience = "https://web.flexitake5api/";
        //});


        //var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
            options.Audience = builder.Configuration["Auth0:Audience"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                NameClaimType = ClaimTypes.NameIdentifier
            };
        });

        builder.Services
          .AddAuthorization(options =>
          {
              options.AddPolicy(
                "read:messages",
                policy => policy.Requirements.Add(
                  new HasScopeRequirement("read:messages", "dev-fzf1ql1knabosj7z.us.auth0.com")
                )
              );
          });

        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        //var app = builder.Build();
        //app.UseAuthentication();
        //app.UseAuthorization();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Flexi5 API",
                Version = "v1"
            });

            // Add JWT Authentication configuration
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'"
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            return new MongoClient(connectionUri);
        });
        builder.Services.AddSingleton<MongoDBServices>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        var app = builder.Build();

        // Error handling middleware
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Internal Server Error: {ex.Message}");
            }
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("AllowLocalhost");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
