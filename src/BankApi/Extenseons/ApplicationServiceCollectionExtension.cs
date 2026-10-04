// extension класс для оптимизации 
using Microsoft.EntityFrameworkCore;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services, ConfigurationManager configuration)
    {
        // 1. РЕГИСТРАЦИЯ СЕРВИСОВ (Контейнер зависимостей)
        services.AddOpenApi();
        services.AddControllers(); // Включаем поддержку контроллеров вместо Minimal API
                                   // Добавляем CORS-политику в контейнер зависимостей
        services.AddCors(options =>
        {
            options.AddPolicy("BankLandingPolicy", policy =>
            {
                policy.WithOrigins(
                        "http://localhost:5173",  // Локальный адрес React (Vite)
                        "http://localhost:3000",  // На случай, запустить через Create React App
                        "https://react-rsb.vercel.app" // Реальный URL сайта с Vercel
                      )
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