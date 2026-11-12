# StudentDemo

Demo ASP.NET Core MVC (.NET 10), dữ liệu in-memory, database nhỏ (5 students).

## Lệnh
- Build: `dotnet build` (phải sạch error và warning)
- Chạy: `dotnet run`

## Contract chung (không tự đổi)
- `Student`: Id, FullName, Email, Age
- `IStudentService`: GetAll(), GetById(int id), Add(Student s)
- `InMemoryStudentService` đăng ký Singleton trong Program.cs (không đổi lifetime)

## Phân quyền file
- Người B: Program.cs, Services/, Middleware/, action Index/Details
- Người A: Models/Student.cs (validation), action Create, Views/Students/Create.cshtml
- File ngoài phạm vi task được giao thì không sửa.

## Quy tắc
- Không thêm NuGet package.
- Không thêm chức năng ngoài task (không tự thêm Edit/Delete).
- Không giả định cấu trúc repo: đọc file trước khi sửa.
- Nếu cần quyết định ngoài mô tả task, dừng lại và hỏi.
- Xong task: báo danh sách file đã sửa và kết quả `dotnet build`.