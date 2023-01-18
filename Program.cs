using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o=>
    {
        o.Cookie.Name = "__Host-spa";
        o.Cookie.SameSite = SameSiteMode.Strict;
        o.Events.OnRedirectToLogin = (context) =>{
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

builder.Services.AddDbContext<HouseDbContext>(o=>
    o.UseQueryTrackingBehavior(Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IHouseRepository,HouseRepository>();
builder.Services.AddScoped<IBidRepository,BidRepository>();
builder.Services.AddMvc();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseAuthentication();

// app.UseCors(p=>p.WithOrigins("http://localhost:3000")
//     .AllowAnyHeader().AllowAnyMethod());

//app.UseHttpsRedirection();

app.MapHouseEndpoints();
app.MapBidEndpoints();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(e=>e.MapDefaultControllerRoute());

app.MapFallbackToFile("index.html");

app.Run();
