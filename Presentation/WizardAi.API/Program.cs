using System.Text.Json.Serialization;
using System.Text.Json;
using WizardAi.Core.External;
using WizardAi.Service.Configurations;
using WizardAi.Service.External;
using WizardAi.Service.CQRS.Completion.Queries.FullTextCompletion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(FullTextCompletionQuery).Assembly);
});

// Options Pattern for AiConfigurations
builder.Services.Configure<AiConfigurations>(builder.Configuration.GetSection(nameof(AiConfigurations)));

builder.Services.AddScoped<IOpenAiService, OpenAiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
