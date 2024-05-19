var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGens();

builder.Services.AddCustomCors(builder.Environment, builder.Configuration);

// builder.Services.AddAppDbContextSqLite(builder.Configuration);
// builder.Services.AddAppDbContextPostgresDocker(builder.Configuration);
builder.Services.AddAppDbContextPostgresLocal(builder.Configuration);

builder.Services.AddScoped<GlobalErrorMiddleWare>();
builder.Services.AddAutoMapper(assembly);

builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddCustomAuthorization();

builder.Services.AddAppServices();
builder.Services.AddCustomIdentity();

builder.Services.AddCustomRouting();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/V1/swagger.json", "App V1"));
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalErrorMiddleWare>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();