using BookStore.Application;
using BookStore.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình riêng của từng máy (chuỗi kết nối...), KHÔNG đưa lên Git – xem README
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

// Ghi log ra console và file logs/web-<ngày>.log (mục 4.5 – Thiết kế thích ứng)
builder.Host.UseSerilog((ctx, cfg) => cfg
    .ReadFrom.Configuration(ctx.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/web-.log", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14));

// Tầng hạ tầng (EF Core + SQL Server) và tầng nghiệp vụ
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("BookStoreDB")
    ?? throw new InvalidOperationException("Thiếu chuỗi kết nối 'BookStoreDB' trong appsettings.json"));
builder.Services.AddApplication();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Tạo/cập nhật CSDL theo migration và nạp dữ liệu mẫu khi chạy lần đầu
await app.Services.KhoiTaoCsdlAsync();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
