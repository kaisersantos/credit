using Credit.Infra.Adapter.EfCore.Config;
using Credit.Infra.Adapter.Dapper.Config;
using Credit.Presentation.BackEnd.Filters;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(new GlobalExceptionFilterAttribute(builder.Environment)))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEfCoreAdapter(builder.Configuration
    .GetSection(nameof(EfCoreAdapterOptions))
    .Get<EfCoreAdapterOptions>()
);

builder.Services.AddDapperAdapter(builder.Configuration
    .GetSection(nameof(DapperAdapterOptions))
    .Get<DapperAdapterOptions>()
);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
