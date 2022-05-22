using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Configuration;
using TicketServiceAPI;
using TicketServiceAPI.DataValid;
using AutoMapper;
using TicketServiceAPI.BLL.Mapper;
using FluentValidation;
using TicketService.model;
using TicketServiceAPI.BLL.ValidationModel;
using TicketServiceAPI.DB;
using Microsoft.Extensions.Logging;
using TicketService;
using TicketServiceAPI.BLL.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;
SegmentsContext.InitSegment(configuration);
SegmentRepository.InitSegmentRepository(configuration);
ValidSchema.InitValidSchema(configuration);
builder.Services.AddScoped<IValidator<Sale>, SaleValidator>();
builder.Services.AddScoped<IValidator<Refund>,RefundValidator>();


builder.Services.AddApiVersioning();
builder.Services.AddAutoMapper(map => map.AddProfile(new SegmentProfile()));
builder.Services.AddDbContext<SegmentsContext>(options =>
           options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    { 
        Title = "Система субсидированных перевозок",
        Version = "v1",
        Description = "Описание TicketSericeAPI"
    }
    );
    var path = Path.Combine(System.AppContext.BaseDirectory, "TicketServiceAPI.xml");
    c.IncludeXmlComments(path);
    
});


var app = builder.Build();

app.UseCustomExeptionHandler();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Система субсидированных перевозок V1");
});

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
