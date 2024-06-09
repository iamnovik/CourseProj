using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using CourseProj.Converters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CourseProj.Data;
using CourseProj.Hubs;
using CourseProj.Models;
using CourseProj.Repositories.Implementations;
using CourseProj.Repositories.Interfaces;
using CourseProj.Services.Implementations;
using CourseProj.Services.Interfaces;
using CourseProj.ValidationAttributes;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<Program>();
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddIdentity<AppUser,IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Добавьте поддержку локализации и выбора культуры
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "ru-RU" };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
    options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
    options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
});
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    
}).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
.AddDataAnnotationsLocalization();;

builder.Services.AddSignalR();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IItemTagRepository, ItemTagRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICollectionItemAttributeRepository, CollectionItemAttributeRepository>();
builder.Services.AddScoped<IAttributeRepository, AttributeRepository>();
builder.Services.AddScoped<IItemAttributeRepository, ItemAttributeRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IItemTagService, ItemTagService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICollectionItemAttributeService, CollectionItemAttributeService>();
builder.Services.AddScoped<IAttributeService, AttributeService>();
builder.Services.AddScoped<IItemAttributeService, ItemAttributeService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<JiraService>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var userService = serviceProvider.GetRequiredService<IUserService>();
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    var user = userManager.GetUserAsync(httpContextAccessor.HttpContext.User).Result;

    return new JiraService(configuration, userService, user);
});
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    .Options;

using (var context = new AppDbContext(options))
{
    context.Database.EnsureCreated();
}


JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Converters = { new NpgsqlTsVectorConverter() }
};



builder.Services.Configure<IdentityOptions>(options =>
{
    // Отключаем базовые ограничения на пароль
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1; // Минимальная длина пароля
    options.Password.RequiredUniqueChars = 0; // Минимальное количество уникальных символов в пароле
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var supportedCultures = new[] { "en-US", "ru-RU" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en-US")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<CommentHub>("/commentHub");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();