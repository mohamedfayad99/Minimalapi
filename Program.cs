using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalVilla;
using MinimalVilla.Data;
using MinimalVilla.Models;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MAppingConfic));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<Program>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/api/Coupon", (ILogger<Program> _loger) =>
{
    _loger.Log(LogLevel.Information, "Getting All Coupon");
    return Results.Ok(CouponStore.coupons);
}).WithName("Getting coupons");

app.MapGet("/api/Coupon/{id:int}", (ILogger<Program> _loger, int id) =>
{
    
    _loger.Log(LogLevel.Information, "Getting  Coupon Using id");
    var coupon = CouponStore.coupons.FirstOrDefault(f => f.Id == id);
    if (coupon == null)
    {
        return Results.BadRequest("this coupon not Exist in my data");
    }
    return Results.Ok(coupon);
}).WithName("Get coupon");

app.MapPost("/api/Coupon", async (ILogger<Program> _loger, IMapper _Map, IValidator<couponcreateddto> _Validate, [FromBody] couponcreateddto coupondto) =>
{
    var validator = await _Validate.ValidateAsync(coupondto);
    if (!validator.IsValid)
    {
        return Results.BadRequest("Invalid Coupon Request ");
    }
    if (CouponStore.coupons.FirstOrDefault(u => u.Name.ToLower() == coupondto.Name.ToLower()) != null)
    {
        return Results.BadRequest("This Name is Already Exist");
    }
    //Map<Distination>(Source);
    Coupon coupon = _Map.Map<Coupon>(coupondto);
    //coupon = new Coupon()
    //{
    //    Name = coupondto.Name,
    //    Persent = coupondto.Persent,
    //    isActive = coupondto.isActive

    //};
    coupon.Id = CouponStore.coupons.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
    CouponStore.coupons.Add(coupon);
    _loger.Log(LogLevel.Information, "Creating Coupon");
    CuoponDTO cuoponDTO = _Map.Map<CuoponDTO>(coupon);
    //{
    //    Id = coupon.Id,
    //    Name = coupon.Name,         
    //    Persent = coupon.Persent,
    //    isActive = coupon.isActive,
    //    created = coupon.created
    //};
    // return Results.CreatedAtRoute("GetCoupon", new {id=coupon.Id}, cuoponDTO);
    return Results.Created($"/api/coupon/{cuoponDTO.Id}", cuoponDTO);
}).WithName("CreateCoupon").Accepts<couponcreateddto>("application/json").Produces<CuoponDTO>(201);



app.MapPut("/api/Coupon", async (ILogger<Program> _loger, IMapper _Map, IValidator<couponupdatedto> _Validate, [FromBody] couponupdatedto coupondto) =>
{
    var validator = await _Validate.ValidateAsync(coupondto);
    if (!validator.IsValid)
    {
        return Results.BadRequest("Invalid Coupon Request ");
    }

    Coupon updatedcupon = CouponStore.coupons.FirstOrDefault(f => f.Id == coupondto.Id);
    if (updatedcupon == null)
    {
        return Results.BadRequest("this coupon not Exist in my data");
    }

    if (CouponStore.coupons.FirstOrDefault(u => u.Name.ToLower() == coupondto.Name.ToLower()) != null)
    {
        return Results.BadRequest("This Name is Already Exist");
    }

   
    updatedcupon.Name = coupondto.Name;
    updatedcupon.Persent = coupondto.Persent;
    updatedcupon.isActive = coupondto.isActive;
    updatedcupon.updated = DateTime.Now;
    _loger.Log(LogLevel.Information, "Updating Coupon Done");
    //int index = CouponStore.coupons.FindIndex(c => c.Id == cp.Id);
    //if (index != -1)
    //{
    //    CouponStore.coupons[index] = updatescp;
    //}

  //  return Results.Content($"/api/coupon/{updatedcupon.Id}", updatedcupon.ToString());
    return Results.Ok(updatedcupon);
});

app.MapDelete("/api/Coupon/{id:int}", (ILogger<Program> _loger, int id) =>
{
    Coupon CouponfromStore = CouponStore.coupons.FirstOrDefault(f => f.Id == id);
    if (CouponfromStore == null)
    {
        return Results.BadRequest("this coupon not Exist in my data");
    }
    CouponStore.coupons.Remove(CouponfromStore);
    _loger.Log(LogLevel.Information, "Updating Coupon Done");
    return Results.Ok("the delete operation done " );

});
app.UseHttpsRedirection();

app.Run();

