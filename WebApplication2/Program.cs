var builder = WebApplication.CreateBuilder(args);

// Настроим CORS (если необходимо, можно убрать)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin() // Можно разрешить любые запросы
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllersWithViews(); // Подключаем сервисы для работы с контроллерами

var app = builder.Build();

// Применяем CORS
app.UseCors();

// Добавляем статические файлы (картинки, CSS, JS)
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();
app.MapGet("/", () => Results.Redirect("/index.html"));

// Включаем авторизацию (если требуется)
app.UseAuthorization();

// Маршруты
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Запускаем приложение
app.Run();
