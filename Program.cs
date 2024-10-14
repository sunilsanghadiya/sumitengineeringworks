using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using sew;
using sew.Database;
using sew.Helpers;
using sew.Middlewares;
using sew.Repository;
using sew.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var configurations = builder.Configuration;

builder.Services.AddDbContext<AppDbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Config settings inject
builder.Services.Configure<ServiceSettings>(configurations.GetSection("ServiceSettings"));


#region JWT Service add
var key = Encoding.ASCII.GetBytes("sdfs^&&#%GFHeyf456fffWEWRCCxstr6wecewr673674rfhsdvfyu3r46R%E%TSFdsdfsdf");

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true,
        ValidateLifetime = true
    };
});
#endregion

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<EmailSenderService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SumitEngineeringWorks", Version = "v1" });
    c.AddSecurityDefinition("Authorization Token", new OpenApiSecurityScheme()
    {
        Description = "Authorization Token (Example: eg67t34refgvgfdfgsgfDrtfsgfee545)",
        Name = "AuthorizatioToken",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Authorization Token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Authorization Token"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthModule V1 Docs");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

// Middleware to enable authentication
app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();

