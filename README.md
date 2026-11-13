# Hệ thống quản lý và bán sách trực tuyến – Nhóm 01

Đồ án môn **Lập trình C#** – GV: TS. Đỗ Như Tài.

> Đang xây dựng. README đầy đủ (cách chạy, tài khoản thử, chức năng) sẽ được cập nhật khi hoàn thành.

## Thành viên

| Thành viên | Phụ trách chính |
|---|---|
| Nguyễn Đạt Mạnh (nhóm trưởng) | Kiến trúc, CSDL/EF Core, đặt hàng – giao tác, kho, báo cáo, form chính & phân quyền Desktop |
| Phạm Quốc Trung | Web: giao diện, trang chủ, danh mục, tìm kiếm, chi tiết sách; Desktop: quản lý sách, danh mục, NXB |
| Lương Diệu Quang | Tài khoản (đăng ký/đăng nhập), giỏ hàng, thanh toán; Desktop: đăng nhập, tài khoản, tiện ích giao diện |
| Đàm Gia Khang | Domain, dữ liệu mẫu, đơn hàng của tôi; Desktop: tác giả, khách hàng, tồn kho, báo cáo, Excel |

## Cấu trúc solution (kiến trúc phân lớp)

```
src/BookStore.Domain          Thực thể và enum
src/BookStore.Application     Nghiệp vụ (service, DTO) dùng chung cho Web và Desktop
src/BookStore.Infrastructure  EF Core + SQL Server
src/BookStore.Web             ASP.NET Core MVC – trang người dùng
src/BookStore.Desktop         WinForms – phần mềm quản trị
```
