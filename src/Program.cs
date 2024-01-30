var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGens();

builder.Services.AddCustomCors(builder.Environment, builder.Configuration);
builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();

builder.Services.AddAppServices();

builder.Services.AddCustomRouting();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "App V1"));
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();