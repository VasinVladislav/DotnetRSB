using Scalar.AspNetCore; // Добавляем импорт
// Эта строка создает строитель приложения
var builder = WebApplication.CreateBuilder(args);

// 1. РЕГИСТРАЦИЯ СЕРВИСОВ (Контейнер зависимостей)
builder.Services.AddServiceCollection(builder.Configuration);

// Этап 2: Сборка приложения
// Создает объект приложения
var app = builder.Build();

// 2. НАСТРОЙКА MIDDLEWARE (Конвейер обработки запросов)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Эндпоинт для генерации json
    app.MapScalarApiReference(); // Красивая документация Scalar
}
app.UseCors("BankLandingPolicy"); // Важно: эта строчка должна идти СТРОГО ДО UseHttpsRedirection и MapControllers!
app.UseHttpsRedirection();
app.UseAuthorization(); // Понадобится в будущем для админки

// 3. МАРШРУТИЗАЦИЯ
app.MapControllers(); // Говорим серверу искать эндпоинты в папке Controllers
await app.InitializeDatabase(); // Сидинг данных если БД пуста.
app.Run();