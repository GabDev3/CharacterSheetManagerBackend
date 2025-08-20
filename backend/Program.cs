using Microsoft.EntityFrameworkCore;
using CharacterSheetManager.Data;
using CharacterSheetManager.Repositories.Interfaces;
using CharacterSheetManager.Repositories;
using CharacterSheetManager.Services.Interfaces;
using CharacterSheetManager.Services;
using CharacterSheetManager.Middleware;
using CharacterSheetManager.Mappings;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{   
    c.CustomSchemaIds(type => type.FullName);
    c.SwaggerDoc("v1", new() { 
        Title = "Character Sheet Manager API", 
        Version = "v1",
        Description = "A comprehensive API for managing D&D character sheets, items, spells, and templates"
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ISpellRepository, SpellRepository>();
builder.Services.AddScoped<IItemTemplateRepository, ItemTemplateRepository>();
builder.Services.AddScoped<ISpellTemplateRepository, SpellTemplateRepository>();

builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISpellService, SpellService>();
builder.Services.AddScoped<IItemTemplateService, ItemTemplateService>();
builder.Services.AddScoped<ISpellTemplateService, SpellTemplateService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Character Sheet Manager API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while creating the database");
    }
}

app.Run();

public partial class Program { }