using NetTopologySuite;
using NetTopologySuite.IO.Converters;
using ZeDeliveryCodeChallenge;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    options.SerializerSettings.Converters.Add(new GeometryConverter(geometryFactory));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultDbString");
builder.Services.AddDbContext<AppDbContext>(config => config.UseNpgsql(
    connectionString,
    pjr => pjr.UseNetTopologySuite()));

builder.Services.AddTransient<IPartnerRepository, PartnerRepository<AppDbContext>>();
builder.Services.AddScoped<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();