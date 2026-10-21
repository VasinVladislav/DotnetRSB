// extension класс для оптимизации 
using Microsoft.EntityFrameworkCore;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services, ConfigurationManager configuration)
    {
        // 1. РЕГИСТРАЦИЯ СЕРВИСОВ (Контейнер зависимостей)
        services.AddOpenApi();
        services.AddControllers(); // Включаем поддержку контроллеров вместо Minimal API

        //  Читаем массив разрешенных адресов из конфигурационного файла appsettings.json
        var allowedOrigins = configuration
            .GetSection("CorsSettings:AllowedOrigins")
            .Get<string[]>() ?? Array.Empty<string>();

        // 2. Динамически применяем политику CORS
        services.AddCors(options =>
        {
            options.AddPolicy("BankLandingPolicy", policy =>
            {
                policy.WithOrigins(allowedOrigins) // Передаем прочитанный из JSON массив строк                
                      .AllowAnyMethod()   // Разрешает любые HTTP-методы: GET, POST, PUT, DELETE
                      .AllowAnyHeader()  // Разрешает любые HTTP-заголовки (Content-Type, Authorization и т.д.)
                      .AllowCredentials(); // Понадобится в будущем, если работать с Cookie/Сессиями
            });
        });
        var connectionString = configuration.GetConnectionString("DefaultConnection"); // Получаем строку подключения к хранилищу в Supabase из appsettings.json

        // СНАЧАЛА регистрируем контекст базы данных PostgreSQL
        services.AddDbContext<BankDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<HeaderMenuStorage>(); // Регистрируем хранилище
        services.AddScoped<LocationMenuStorage>();
        services.AddScoped<UserStorage>();
        services.AddScoped<HeaderMenuInitializer>();
        services.AddScoped<LocationMenuInitializer>();
        return services;
    }
}