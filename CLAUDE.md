# StudentDemo

Demo ASP.NET Core MVC (.NET 10), dữ liệu in-memory, KHÔNG database.
Web CRUD sinh viên, có đăng nhập/đăng xuất và quản lý người dùng. Chỉ một loại tài khoản (admin), không có vai trò.

## Lệnh
- Build: `dotnet build` (phải sạch error và warning)
- Chạy: `dotnet run`

## Hợp đồng chung (không tự đổi)
- `Student`: Id, FullName, Email, Age
- `IStudentService`: GetAll, GetById, Add, Update (bool), Delete (bool)
- `User`: Id, Username, FullName, PasswordHash
- `IUserService`: GetAll, GetById, GetByUsername, Add (bool), Update (bool), Delete (bool), ValidateCredentials
- Hai dịch vụ in-memory đăng ký Singleton trong Program.cs (không đổi lifetime)
- Tài khoản mẫu: admin / Admin@123

## Phân việc theo file
- Người A: AccountController, UsersController, Views/Account, Views/Users, Middleware/, đăng ký middleware trong Program.cs
- Người B: StudentsController, Views/Students, Models/Student.cs
- File ngoài phạm vi việc được giao thì không sửa. Chỉ thêm action/file mới, không sửa phần của người kia.

## Quy tắc
- Không thêm NuGet package. Không thêm database. Không thêm vai trò.
- Cả web phải đăng nhập, chỉ trang Login được công khai ([AllowAnonymous]).
- Không lưu mật khẩu chữ thường. Admin không được xóa chính tài khoản đang đăng nhập (kiểm tra ở UsersController).
- Xóa và đăng xuất dùng POST, không dùng link GET.
- Không giả định cấu trúc repo: đọc file trước khi sửa. Cần quyết định ngoài mô tả thì dừng và hỏi.
- Xong việc: báo danh sách file đã sửa và kết quả `dotnet build`.
