var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Khung ban đầu – các tầng sẽ được nối vào trong các commit tiếp theo
app.MapGet("/", () => "BookStore – website bán sách đang được xây dựng");

app.Run();
