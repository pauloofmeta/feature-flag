using Carguero.FeatureFlag.Abstrations;
using Carguero.FeatureFlag.Data;
using Carguero.FeatureFlag.Data.Repositories;
using Carguero.FeatureFlag.Extensions;
using Carguero.FeatureFlag.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtToken(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(o => 
    o.UseSqlServer(builder.Configuration.GetConnectionString("DbFeatures")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IPrincipal, Principal>();
builder.Services.AddSingleton<IFeatureDefinitionProvider, MyFeatures>();
builder.Services.AddFeatureManagement();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
