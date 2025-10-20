using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using minmalAPI.AppDB;
using minmalAPI.DTOs;
using minmalAPI.Entities;
using minmalAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MinimalAppConection"));
});

builder.Services.AddScoped<ITodoService, TodoService>();

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

app.MapPost("/AddTodo", async Task<Results<Ok, BadRequest<String>>>
    (MinimalDTO modelDTO,
    ITodoService service,
    ILogger<Program> logger) =>
{
    if (modelDTO == null) return TypedResults.BadRequest("Todo item cannot be null.");

    await service.CreateAsync(modelDTO);
    return TypedResults.Ok();

});
app.MapGet("/GtAll", async (ITodoService service) =>
{
    var result = await service.GetAllAsync();
    if (result == null) return Results.BadRequest(result);
    return Results.Ok(result);
});

app.MapPut("/Update", async Task<Results<Ok, BadRequest<String>>>
    (int id,
    UpdateMinimalDTO modelDTO,
    ITodoService service,
    ILogger<Program> logger ) =>
    {
        if (modelDTO == null)
            return TypedResults.BadRequest("Error while Creating a new Todo Item:{Message}");


        await service.UpdateAsync(modelDTO);

        return TypedResults.Ok();
    });

app.MapDelete("/Delete", async Task<Results<Ok, BadRequest<string>>>
    ([FromQuery]int id,
     [FromBody] DeleteMinimalDTO modelDTO,
     ITodoService service,
     ILogger<Program> logger) =>
    {
        if (modelDTO == null) return TypedResults.BadRequest("Todo item cannot be null.");
        await service.DeleteByIdAsync(id);
        return TypedResults.Ok();
    });

app.Run();
