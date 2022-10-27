using API_Gateway.Common.ExceptionHandler;
using API_Gateway.Common.Logics;
using API_Gateway.Common.Logics.Interface;
using API_Gateway.data.Entites;
using API_Gateway.Repository;
using API_Gateway.Repository.Interface;
using API_Gateway.Services;
using API_Gateway.Services.Interface;
using API_Gateway.Utilities;
using API_Gateway_FunctionApp.Service;
using API_Gateway_FunctionApp.Service.Interface;
using Azure.Storage.Blobs;
using LoyaltyOnlineWSUtilities.Service;
using LoyaltyOnlineWSUtilities.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using OktaUtilities.Service;
using OktaUtilities.Service.Interface;
using OnlineMessagesWSUtilities.Service;
using OnlineMessagesWSUtilities.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Loyalty_Robinsons_PreProdContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("AzureRedisConnection");
});

builder.Services.AddScoped(options =>
{
    return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
});

builder.Services.AddAzureClients(AzureClientFactoryBuilder => 
{
    AzureClientFactoryBuilder.AddSecretClient(
        builder.Configuration.GetSection("KeyVault"));
});

builder.Services.AddSingleton<IKeyVaultManager, KeyVaultManager>(); 
builder.Services.AddScoped<IFileManagerLogic, FileManagerLogic>();

builder.Services.AddScoped<ILoyaltyRobinsonsRepository, LoyaltyRobinsonsRepository>();
builder.Services.AddScoped<ILoyaltyRobinsonsService, LoyaltyRobinsonsService>();
builder.Services.AddScoped<ILOWSLoginService, LOWSLoginService>();
builder.Services.AddScoped<ILOWSMemberService, LOWSMemberService>();
builder.Services.AddScoped<IOnlineMWService, OnlineMWService>();
builder.Services.AddScoped<ITableStorageService, TableStorageService>();
builder.Services.AddHttpClient<IOktaClientService, OktaClientService>();
builder.Services.AddHttpClient<IFunctionAppService, FunctionAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();


app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
