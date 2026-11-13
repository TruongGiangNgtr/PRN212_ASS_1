# PRN212_ASS_1

Web quản lý sinh viên viết bằng **ASP.NET Core MVC (.NET 10)**, có đăng nhập và đăng xuất. Dữ liệu lưu trong bộ nhớ, **không dùng database**. Đây là project demo cho chủ đề "Làm web MVC trong C# thế nào?".

## 1. Web làm được gì

- **Đăng nhập, đăng xuất.** Cả web phải đăng nhập, chỉ trang đăng nhập là công khai. Chỉ có một tài khoản (admin), không có vai trò, không có đăng ký.
- **Danh sách sinh viên.** Sau khi đăng nhập là vào thẳng trang này, hiện bảng sinh viên kèm nút Thêm, Chi tiết, Sửa, Xóa.
- **Xem chi tiết một sinh viên.** Địa chỉ gọn dạng `/students/1`. Id không có thì báo "Không tìm thấy".
- **Thêm sinh viên** và **Sửa sinh viên.** Có kiểm tra dữ liệu nhập (xem mục 6). Lưu xong quay về danh sách và hiện dòng thông báo thành công.
- **Xóa sinh viên.** Có trang xác nhận, xóa bằng nút gửi form.

Dữ liệu mẫu: 5 sinh viên (Id 1 đến 5). Tắt app là mất những gì đã thêm, sửa, xóa. Không có bảng, cột hay index nào vì không có database.

## 2. Công nghệ và thư viện

| Thành phần | Dùng để làm gì |
|---|---|
| .NET 10 (`net10.0`), C# | Nền tảng và ngôn ngữ |
| ASP.NET Core MVC | Khung web theo mô hình Model, View, Controller |
| Razor (`.cshtml`) và Tag Helper | Viết giao diện, tạo form và đường dẫn tự động (`asp-action`, `asp-for`...) |
| Cookie Authentication (có sẵn trong ASP.NET Core) | Giữ phiên đăng nhập |
| Bootstrap, jQuery (file tĩnh trong `wwwroot/lib/`, đến từ template `dotnet new mvc`) | Giao diện |

## 3. Agent và công cụ

**AI và agent:**
- **Claude (chat trên claude.ai, project "PRN")**: lên kế hoạch, chia phase và việc cho hai người, soạn prompt, đọc repo trên GitHub để đối chiếu với đề, soạn tài liệu.
- **Claude Code**: agent thực hiện việc viết code trong repo, theo prompt do người làm soạn. Người làm kiểm tra kết quả (build, xem thay đổi, chạy thử) trước khi push.

**Tài liệu cho agent trong repo:**
- `CLAUDE.md`: Claude Code tự đọc file này, ghi quy tắc và hợp đồng chung của project.

**Quy trình:** mỗi việc là một nhánh `feature/...`, agent build rồi commit, người làm tự xem `git diff` để chắc agent không sửa ngoài phạm vi, rồi mới push và merge vào `main`.

## 4. Môi trường

- Visual Studio (repo có `.gitignore` cho Visual Studio và file solution `StudentDemo.slnx`) hoặc chỉ cần dòng lệnh.
- Git và GitHub: `https://github.com/TruongGiangNgtr/PRN212_ASS_1`.

## 5. Cài đặt và chạy web

### Bước 1: Cài công cụ (làm một lần)
1. **.NET 10 SDK:** tải ở `https://dotnet.microsoft.com/download`. Cài xong mở terminal **mới** và chạy `dotnet --version`, kết quả phải bắt đầu bằng `10.`
2. **Git:** tải ở `https://git-scm.com/download/win`. Kiểm tra bằng `git --version`.

### Bước 2: Lấy code
```
git clone https://github.com/TruongGiangNgtr/PRN212_ASS_1.git
cd PRN212_ASS_1
```
Nếu đã clone từ trước thì chạy `git pull` ở thư mục repo để lấy bản mới nhất.

### Bước 3: Build và chạy
```
cd StudentDemo
dotnet build
dotnet run --launch-profile https
```
- `dotnet build` phải xong mà không có lỗi.
- Địa chỉ web: `https://localhost:7014` (hoặc `http://localhost:5204`). Trình duyệt tự mở khi chạy.
- Dừng web: bấm `Ctrl + C` trong terminal.

**Nếu dùng Visual Studio:** mở file `StudentDemo.slnx`, chọn profile `https` rồi bấm chạy (F5). Nếu Visual Studio không mở được file `.slnx` thì bản của bạn chưa đủ mới, hãy cập nhật hoặc dùng dòng lệnh ở trên.

### Bước 4: Đăng nhập
| Tên đăng nhập | Mật khẩu |
|---|---|
| `admin` | `Admin@123` |

### Lỗi thường gặp
| Hiện tượng | Cách xử lý |
|---|---|
| `dotnet` không nhận lệnh | Chưa cài SDK, hoặc chưa mở terminal mới sau khi cài |
| Báo thiếu SDK hoặc không build được | `dotnet --version` không bắt đầu bằng `10.`, cài lại SDK 10 |
| Trình duyệt cảnh báo chứng chỉ HTTPS | Chạy `dotnet dev-certs https --trust` rồi chạy lại |
| Cổng 5204 hoặc 7014 đang bị dùng | Tắt chương trình đang dùng cổng đó, hoặc sửa cổng trong `StudentDemo/Properties/launchSettings.json` |
| Dữ liệu đã thêm bị mất | Bình thường: dữ liệu chỉ nằm trong bộ nhớ, tắt app là mất |

## 6. Cách dùng web

1. Mở web, đăng nhập bằng `admin` / `Admin@123`. Sai thì báo "Sai tên đăng nhập hoặc mật khẩu".
2. Trang danh sách sinh viên hiện ra. Bấm **Thêm sinh viên** ở góc trên để mở form thêm.
3. Mỗi dòng có **Chi tiết**, **Sửa**, **Xóa**. Xóa phải bấm xác nhận ở trang kế tiếp.
4. Góc phải trên cùng có "Chào, admin" và nút **Đăng xuất**.

**Quy tắc nhập đúng (báo lỗi tiếng Việt):**
- Họ tên: bắt buộc.
- Email: bắt buộc và đúng dạng email.
- Tuổi: bắt buộc, từ 16 đến 60.

## 7. Các địa chỉ trong web

| Địa chỉ | Làm gì | Cách gửi |
|---|---|---|
| `/` | Chuyển vào danh sách sinh viên (chưa đăng nhập thì về trang đăng nhập) | GET |
| `/Account/Login` | Trang đăng nhập, và gửi form đăng nhập | GET, POST |
| `/Account/Logout` | Đăng xuất | POST |
| `/Students` | Danh sách sinh viên | GET |
| `/students/{id}` | Chi tiết một sinh viên | GET |
| `/Students/Create` | Form thêm, và lưu | GET, POST |
| `/Students/Edit/{id}` | Form sửa, và lưu | GET, POST |
| `/Students/Delete/{id}` | Trang xác nhận, và xóa | GET, POST |
| `/Home/Privacy` | Trang Privacy của template (cũng phải đăng nhập) | GET |

Trang chi tiết chỉ có đường dẫn gọn `/students/{id}`, đường dẫn kiểu `/Students/Details/1` không còn dùng được.

## 8. Cấu trúc thư mục và việc của từng file

```
StudentDemo.slnx                        File solution để mở bằng Visual Studio
CLAUDE.md, PHASE2.md                    Tài liệu cho agent (xem mục 3)
StudentDemo/
  Program.cs                            Khởi động web: đăng ký dịch vụ, đăng nhập bằng cookie, dựng pipeline, đặt đường dẫn mặc định
  Models/
    Student.cs                          Sinh viên (Id, họ tên, email, tuổi) và quy tắc nhập đúng
    User.cs                             Tài khoản (Id, tên đăng nhập, họ tên, mật khẩu đã mã hóa)
    ErrorViewModel.cs                   Dữ liệu cho trang lỗi (của template)
  Services/
    IStudentService.cs                  Hợp đồng: lấy tất cả, lấy một, thêm, sửa, xóa sinh viên
    InMemoryStudentService.cs           Lưu sinh viên trong List, có sẵn 5 sinh viên mẫu
    IUserService.cs                     Hợp đồng quản lý tài khoản và kiểm tra mật khẩu
    InMemoryUserService.cs              Lưu tài khoản trong List, có sẵn tài khoản admin, mã hóa mật khẩu bằng PasswordHasher
  Controllers/
    AccountController.cs                Đăng nhập, đăng xuất
    StudentsController.cs               Danh sách, chi tiết, thêm, sửa, xóa sinh viên
    HomeController.cs                   Trang Privacy và trang lỗi (của template)
  Views/
    Account/Login.cshtml                Form đăng nhập
    Students/                           Index (danh sách), Details, Create, Edit, Delete, NotFound, _StudentForm (phần form dùng chung cho thêm và sửa)
    Shared/_Layout.cshtml               Khung chung mọi trang: header (tên project bên trái, đăng xuất bên phải), footer
    Shared/_ValidationScriptsPartial    Nạp thư viện kiểm tra form phía trình duyệt (chưa trang nào dùng)
  Properties/launchSettings.json        Cổng chạy thử: 5204 (http), 7014 (https)
  wwwroot/                              File tĩnh: css, js, thư viện Bootstrap và jQuery
```

## 9. Một request đi qua web như thế nào (đối chiếu với đề)

1. **Pipeline (middleware)** trong `Program.cs`, theo thứ tự: chuyển sang HTTPS, **routing** (chọn controller và action từ địa chỉ), **nhận diện người dùng** (đọc cookie), **kiểm tra quyền** (chưa đăng nhập thì chuyển về trang đăng nhập), rồi file tĩnh và controller.
2. **Controller** nhận request. Controller không tạo dịch vụ, mà nhận `IStudentService` qua hàm khởi tạo (**dependency injection**). Hai dịch vụ đăng ký kiểu Singleton nên danh sách trong bộ nhớ dùng chung cho mọi request.
3. **GET** hiện trang (danh sách, form). **POST** gửi dữ liệu (đăng nhập, thêm, sửa, xóa), có `[ValidateAntiForgeryToken]` chống giả mạo form.
4. Với form thêm và sửa, **Model binding** gán dữ liệu vào `Student`, **ModelState.IsValid** cho biết có vi phạm quy tắc không. Sai thì hiện lại form kèm lỗi, đúng thì lưu rồi **chuyển hướng về danh sách** (không trả lại trang form, để bấm F5 không gửi lại).
5. Controller trả **View** (Razor) kèm dữ liệu. Thông báo thành công đi qua **TempData** (chỉ sống qua một lần chuyển hướng). Tiêu đề trang chi tiết đi qua **ViewData**.

| Mục trong đề | Nằm ở đâu |
|---|---|
| MVC, vai trò Model/View/Controller, luồng request | Models/, Views/, Controllers/ và mục 9 này |
| Routing | Route mặc định trong Program.cs (về Students/Index) và route `students/{id}` trên action chi tiết |
| GET và POST | Các action cặp đôi trong StudentsController và AccountController |
| Middleware | Pipeline trong Program.cs. **Không có middleware tự viết** (đã bỏ khỏi phạm vi) |
| Dependency Injection | `AddSingleton` trong Program.cs, hàm khởi tạo các controller |
| Truyền dữ liệu giữa Controller và View | Model (danh sách, một sinh viên), ViewData (tiêu đề), TempData (thông báo) |
| Form và Validation | Student.cs (Data Annotations), `_StudentForm.cshtml`, StudentsController (kiểm tra ModelState) |
| Đăng nhập, đăng xuất, phân quyền | AccountController, `[Authorize]` trên các controller, `[AllowAnonymous]` chỉ ở Login |

## 10. Quá trình làm

1. **Dựng khung:** tạo project `dotnet new mvc` (.NET 10), thư mục, `Student`, dịch vụ lưu trong bộ nhớ, 5 sinh viên mẫu.
2. **Bổ sung khung đăng nhập:** tài khoản, dịch vụ tài khoản có mã hóa mật khẩu, đăng nhập bằng cookie, `[Authorize]`, thêm Sửa và Xóa vào dịch vụ sinh viên.
3. **Đăng nhập và đăng xuất.**
4. **Quản lý người dùng** (danh sách, thêm, sửa, xóa tài khoản): đã làm xong nhưng sau đó **bỏ khỏi phạm vi** và xóa code. Vẫn còn trong lịch sử commit.
5. **Dọn dẹp:** header đơn giản, đăng nhập xong vào thẳng danh sách sinh viên, đổi đường dẫn mặc định.
6. **Chức năng sinh viên:** danh sách, rồi chi tiết, thêm, sửa, xóa (người B), gộp bằng pull request.

## 11. Giới hạn đã biết

- Dữ liệu chỉ nằm trong bộ nhớ, mất khi tắt app. Không có database.
- Chỉ một tài khoản `admin`, không đổi được mật khẩu, không có quản lý người dùng.
- Kiểm tra form chỉ chạy ở phía server (thư viện phía trình duyệt có sẵn nhưng chưa nạp).
- Không có middleware tự viết, không có test tự động.
- Mật khẩu mẫu `Admin@123` nằm trong code, chỉ phù hợp cho demo.
- `CLAUDE.md` và `PHASE2.md` còn nội dung cũ về quản lý người dùng và middleware.
- Còn vài file `.gitkeep` thừa trong các thư mục đã có file thật.
