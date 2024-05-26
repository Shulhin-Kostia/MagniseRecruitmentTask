using MagniseRecruitmentTask.Data;
using MagniseRecruitmentTask.Jobs;
using MagniseRecruitmentTask.Models;
using MagniseRecruitmentTask.Services;
using MagniseRecruitmentTask.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("CoinsDB"));
});

builder.Services.AddScoped<CoinApiService>();
builder.Services.AddHostedService<CoinPriceUpdateService>();

builder.Services.AddExceptionHandler<DefaultExceptionHadler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/coins/supported", async (AppDbContext context) =>
{
    return Results.Ok(await context.Coins.Select(c => c.Code).ToListAsync());
});

//app.MapPost("/coins/supported", async (CoinApiService coinApiService, AppDbContext context, [FromBody, Required] string coinCode) =>
//{
//    if(!Regex.IsMatch(coinCode, @"^[A-Z]{3,5}$"))
//        return Results.BadRequest("Invalid format of coin code. You should use only from 3 to 5 uppercase letters.");
//    if (context.Coins.Any(c => c.Code.Equals(coinCode)))
//        return Results.BadRequest("Coin already added.");

//    var exchangeRate = await coinApiService.GetRateAsync(coinCode);
//    if (exchangeRate == default)
//        return Results.BadRequest("Such coin doesn't exist.");

//    context.Coins.Add(
//        new Coin()
//        {
//            Code = coinCode,
//            Price = exchangeRate!.rate,
//            PriceUpdated = exchangeRate!.time
//        });
//    await context.SaveChangesAsync();

//    return Results.Ok(await context.Coins.FirstAsync(c => c.Code.Equals(coinCode)));
//});

app.MapGet("/coins", async (AppDbContext context, [FromQuery] string ids) =>
{
    if(ids == default)
        return Results.Ok(await context.Coins.ToListAsync());

    var coinCodes = ids.QueryParams();
    var availableCoins = context.Coins.Select(c => c.Code).ToList();
    if (!coinCodes.All(cc => availableCoins.Contains(cc)))
    {
        var unsupportedCoins = coinCodes.Where(cc => !availableCoins.Contains(cc));

        return Results.BadRequest($"Coins {string.Join(',', unsupportedCoins)} not supported. Please choose only supported coins.");
    }

    return Results.Ok(await context.Coins.Where(c => coinCodes.Contains(c.Code)).ToListAsync());
});

DataSeeder.Seed(app);

app.Run();
