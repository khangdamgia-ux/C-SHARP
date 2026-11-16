using BookStore.Application;
using BookStore.Desktop.HaTang;
using BookStore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BookStore.Desktop;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        // ---- Cấu hình và DI (giống Program.cs của Web) ----
        var builder = Host.CreateApplicationBuilder(new HostApplicationBuilderSettings
        {
            ContentRootPath = AppContext.BaseDirectory
        });
        // Cấu hình riêng của từng máy (chuỗi kết nối...), KHÔNG đưa lên Git – xem README
        builder.Configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.Local.json"), optional: true);

        builder.Services.AddSerilog(cfg => cfg
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "desktop-.log"),
                rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14));

        builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("BookStoreDB")
            ?? throw new InvalidOperationException("Thiếu chuỗi kết nối 'BookStoreDB' trong appsettings.json"));

        // Service nghiệp vụ dùng chung với Web (BookStore.Application)
        builder.Services.AddApplication();

        builder.Services.AddSingleton<PhienDangNhap>();
        builder.Services.AddSingleton<IFormFactory, FormFactory>();


        using var host = builder.Build();
        var log = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("BookStore.Desktop");
        XuLyLoi.DangKy(log);

        // ---- Kết nối và khởi tạo CSDL ----
        try
        {
            host.Services.KhoiTaoCsdlAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            log.LogCritical(ex, "Không kết nối được CSDL");
            ThongBao.Loi("Không kết nối được CSDL BookStoreDB.\nHãy kiểm tra SQL Server và chuỗi kết nối trong appsettings.json.\n\n" + ex.Message);
            return;
        }

        // ---- Tạm thời: báo kết nối CSDL thành công (màn hình đăng nhập làm ở commit sau) ----
        ThongBao.ThanhCong("Kết nối CSDL BookStoreDB thành công.");
        Log.CloseAndFlush();
    }
}
