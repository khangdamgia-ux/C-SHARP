using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application;

public static class DependencyInjection
{
    /// <summary>Đăng ký toàn bộ service nghiệp vụ – Web và Desktop gọi cùng một hàm nên luôn dùng chung quy tắc.</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
