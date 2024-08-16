using Microsoft.EntityFrameworkCore;
using FruitApi.Database;
using FruitApi.Repository;  
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();


// Add the DbContext - so the App will work with the PostgreSQL Database
builder.Services.AddDbContext<FruitDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFruitRepository, FruitRepositoryImpl>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


if (!app.Environment.IsDevelopment())   
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.Run();
