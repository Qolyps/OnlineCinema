var builder = WebApplication.CreateBuilder(args);

// �������� CORS (���� ����������, ����� ������)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin() // ����� ��������� ����� �������
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllersWithViews(); // ���������� ������� ��� ������ � �������������

var app = builder.Build();

// ��������� CORS
app.UseCors();

// ��������� ����������� ����� (��������, CSS, JS)
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();
app.MapGet("/", () => Results.Redirect("/index.html"));

// �������� ����������� (���� ���������)
app.UseAuthorization();

// ��������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ��������� ����������
app.Run();
