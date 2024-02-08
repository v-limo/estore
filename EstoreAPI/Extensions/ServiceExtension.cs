namespace EStoreAPI.Extensions;

public static class ServiceExtension
{
    public static void AddCustomRouting(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRouting(options => options.LowercaseUrls = true);
    }

    public static void AddCustomSwaggerGens(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("V1", new OpenApiInfo
            {
                Version = "V1",
                Title = "API App",
                Description = "API App",
                TermsOfService = new Uri("https://fakeurl.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Vincent",
                    Url = new Uri("https://fakeurl.com/dsfsdsa")
                },
                License = new OpenApiLicense
                {
                    Name = "License",
                    Url = new Uri("https://example.com/fdfd")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Description = "Bearer Token",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Description = "Authentication",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
    }

    public static void AddAppServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IProductService, ProductService>();
        serviceCollection.AddScoped<ICategoryService, CategoryService>();
        serviceCollection.AddScoped<IOrderService, OrderService>();
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
        serviceCollection.AddScoped<IReviewService, ReviewService>();
        serviceCollection.AddScoped<IAuthService, AuthService>();
    }

    public static void AddAppDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SqliteConnection"));
        });
    }


    public static void AddJwtAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var validIssuer = configuration.GetSection("BearerAuthentication:ValidIssuer").Value!;
        var validAudiences =
            configuration.GetSection("BearerAuthentication:ValidAudiences").Get<List<string>>();
        var secret = configuration.GetSection("BearerAuthentication:Secret").Value!;

        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = true,
                ValidateAudience = false,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = validIssuer,
                ValidAudiences = validAudiences,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
            });
    }

    public static void AddCustomAuthorization(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAuthorizationBuilder()
            .AddPolicy("adminPolicy", policyBuilder => policyBuilder.RequireRole(Role.Admin.ToString()))
            .AddPolicy("usersPolicy", policyBuilder => policyBuilder.RequireRole(Role.User.ToString()))
            .AddPolicy("profile", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.RequireClaim(ClaimTypes.NameIdentifier);
            });
    }


    public static void AddCustomCors(this IServiceCollection serviceCollection, IWebHostEnvironment environment,
        IConfiguration configuration)
    {
        serviceCollection.AddCors(options =>
        {
            var frontends = configuration.GetSection("Frontends:production").Get<string[]>();

            if (environment.IsDevelopment())
            {
                options.AddPolicy("dev-free", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });

                options.AddPolicy("devReact", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3000/");
                });
            }
            else if (environment.IsProduction() && !frontends.IsNullOrEmpty())
            {
                options.AddPolicy("production", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithOrigins(frontends!);
                });
            }
            else
            {
                options.AddPolicy("denyAll", builder => { builder.WithMethods([]); });
            }
        });
    }

    public static void AddCustomIdentity(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddIdentityCore<ApplicationUser>(
                options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = false; //Todo: true
                    options.Password.RequireUppercase = false; //Todo: true
                    options.Password.RequireNonAlphanumeric = false; //Todo: true
                    options.Password.RequiredLength = 3; //Todo: 8
                    options.User.RequireUniqueEmail = true;
                })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}
