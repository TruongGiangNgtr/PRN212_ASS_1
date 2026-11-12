# StudentDemo: Kế hoạch Phase 2 (implement)

Tài liệu này giúp người và agent hiểu bối cảnh của Phase 2. **Đọc để hiểu, nhưng chỉ làm đúng việc được giao trong prompt.** Các việc của người kia, và việc nào chưa được giao, không làm.

## 1. Bối cảnh

Web CRUD đơn giản bằng ASP.NET Core MVC (.NET 10), dữ liệu lưu trong bộ nhớ, có đăng nhập, đăng xuất, phân quyền và quản lý người dùng. Demo này phục vụ chủ đề "Làm web MVC trong C# thế nào?", gồm: MVC (Model, View, Controller, luồng request), routing, GET/POST, middleware, dependency injection, truyền dữ liệu giữa các thành phần, xử lý form và validation.

Phase 1 (scaffold) đã xong. Repo hiện có sẵn: hợp đồng dịch vụ, tài khoản mẫu, đăng nhập bằng cookie, các controller còn rỗng, menu đăng nhập/đăng xuất.

## 2. Các quyết định đã chốt (không bàn lại)

- **Không database.** Mọi dữ liệu nằm trong bộ nhớ, tắt app là mất. Không có bảng, cột, index.
- **Chỉ một loại tài khoản (admin), không có vai trò.** Phân quyền = chưa đăng nhập thì bị chặn.
- **Cả web phải đăng nhập.** Chỉ trang đăng nhập là công khai.
- **Không có tự đăng ký.** Tài khoản chỉ do admin tạo trong phần quản lý người dùng.
- **Admin không được xóa chính tài khoản đang đăng nhập.**
- **Tài khoản mẫu:** `admin` / `Admin@123`. 5 sinh viên mẫu (Id 1 đến 5).
- **Không thêm NuGet package.** Không thêm chức năng ngoài danh sách việc bên dưới.
- **Không làm middleware ghi nhật ký riêng** (đã bỏ khỏi phạm vi). Không thêm file nào vào `Middleware/`.
- **Chưa chốt (không làm):** báo lỗi form ngay trên trình duyệt (bằng thư viện jQuery validation có sẵn).

## 3. Hợp đồng chung (không tự đổi)

- `Student`: `Id`, `FullName`, `Email`, `Age`.
- `IStudentService`: `GetAll()`, `GetById(id)`, `Add(student)`, `Update(student)` (trả bool), `Delete(id)` (trả bool).
- `User`: `Id`, `Username`, `FullName`, `PasswordHash`. Không có trường vai trò.
- `IUserService`: `GetAll()`, `GetById(id)`, `GetByUsername(username)`, `Add(user, password)` (bool, false nếu trùng tên đăng nhập), `Update(user, newPassword)` (bool; mật khẩu rỗng thì giữ cũ), `Delete(id)` (bool), `ValidateCredentials(username, password)`.
- Hai dịch vụ in-memory đăng ký Singleton trong `Program.cs`. Không đổi lifetime.

## 4. Ai sở hữu file nào

| Người | Được sửa | Không sửa |
|---|---|---|
| **A** | `AccountController`, `UsersController`, `Views/Account/`, `Views/Users/`, và file mới cho form người dùng trong `Models/` | `StudentsController`, `Views/Students/`, `Models/Student.cs`, `Models/User.cs`, `Program.cs`, `_Layout.cshtml` |
| **B** | `StudentsController`, `Views/Students/`, `Models/Student.cs` | Mọi file của A, `Program.cs`, `_Layout.cshtml`, `Models/User.cs` |

Cả hai chỉ **thêm action/file mới**, không sửa phần của người kia. `_Layout.cshtml` đã có đủ menu và `Program.cs` đã có đủ cấu hình đăng nhập, nên **không ai sửa hai file này trong Phase 2**.

## 5. Việc của A: tài khoản và người dùng

**Làm đăng nhập**
- Trang nhập tên đăng nhập và mật khẩu, gọi `ValidateCredentials`.
- Sai thì báo "Sai tên đăng nhập hoặc mật khẩu" (không nói rõ sai cái nào). Đúng thì tạo phiên đăng nhập bằng cookie, trong đó có tên đăng nhập (để menu hiện "Chào, ...") và Id người dùng (để kiểm tra "không xóa chính mình"), rồi chuyển về trang đang định vào.
- Chỉ chuyển về địa chỉ nội bộ của web (chặn chuyển hướng ra ngoài).
- Hai action đăng nhập (xem form và gửi form) phải cho phép người chưa đăng nhập; đây là chỗ duy nhất dùng `[AllowAnonymous]`.
- Nghiệm thu: vào `/` lúc chưa đăng nhập bị đẩy về trang đăng nhập; đăng nhập đúng vào được trang chủ; menu hiện tên.

**Làm đăng xuất**
- Action nhận POST (nút trong menu đã có sẵn), xóa phiên đăng nhập, về trang đăng nhập.
- Nghiệm thu: bấm "Đăng xuất" thì vào lại `/Students` bị đẩy về trang đăng nhập.

**Danh sách người dùng**
- Bảng các tài khoản (Id, tên đăng nhập, họ tên), có nút "Thêm người dùng", và nút "Sửa", "Xóa" trên mỗi dòng.
- Không hiển thị mật khẩu hay chuỗi mã hóa.

**Thêm người dùng**
- Form: tên đăng nhập, họ tên, mật khẩu, nhập lại mật khẩu. Quy tắc: các ô bắt buộc, mật khẩu tối thiểu 6 ký tự, hai mật khẩu phải khớp.
- Dùng một lớp dữ liệu riêng cho form (file mới trong `Models/`) để không sửa lớp `User`.
- Trùng tên đăng nhập (không phân biệt hoa thường) thì báo lỗi ngay ô tên đăng nhập.
- Thêm xong quay về danh sách và hiện thông báo thành công.

**Sửa người dùng**
- Form điền sẵn tên đăng nhập, họ tên; có ô mật khẩu mới, **để trống thì giữ mật khẩu cũ**.
- Không cho đổi sang tên đăng nhập đang thuộc người khác. Id không tồn tại thì báo không tìm thấy.

**Xóa người dùng**
- Có trang xác nhận trước khi xóa; việc xóa gửi bằng form (POST), không dùng link.
- **Không cho xóa tài khoản đang đăng nhập**: so Id trong phiên đăng nhập với Id cần xóa, nếu trùng thì từ chối và báo lỗi.

## 6. Việc của B: sinh viên

**Danh sách sinh viên**
- Bảng Id, họ tên, email, tuổi; nút "Thêm sinh viên" ở đầu bảng; mỗi dòng có "Chi tiết", "Sửa", "Xóa" (các nút trỏ tới trang làm ở các việc sau).
- Hiện dòng thông báo nếu có (ví dụ "Thêm thành công").
- Nghiệm thu: `/Students` hiện đủ 5 sinh viên mẫu.

**Xem chi tiết một sinh viên**
- Hiện đủ các trường. Id không tồn tại thì báo không tìm thấy.
- Nghiệm thu: mở chi tiết Id 1 đúng người; Id 999 báo không tìm thấy.

**Thêm sinh viên**
- Quy tắc nhập đúng trên `Student`: họ tên bắt buộc, email bắt buộc và đúng dạng, tuổi từ 16 đến 60; thông báo lỗi bằng tiếng Việt. **Không dùng `[MaxLength]`/`[StringLength]` nếu không cần.**
- Form xem (GET) và form gửi (POST, có `[ValidateAntiForgeryToken]`, kiểm tra `ModelState.IsValid`). Nhập sai thì hiện lại form kèm lỗi và giữ dữ liệu đã nhập.
- Thành công thì quay về danh sách (không trả lại trang form) kèm thông báo "Thêm thành công" (dùng `TempData`).

**Sửa sinh viên**
- Form giống trang thêm nhưng điền sẵn dữ liệu, cùng quy tắc nhập đúng. Id không tồn tại thì báo không tìm thấy.

**Xóa sinh viên**
- Trang xác nhận có thông tin sinh viên; việc xóa gửi bằng form (POST), không dùng link.
- Xóa xong về danh sách kèm thông báo.

**Đặt đường dẫn gọn cho trang chi tiết**
- Ví dụ `/students/1` thay vì `/Students/Details/1`, dùng attribute route trên action chi tiết. Các action khác giữ route mặc định.
- Lưu ý: sau khi đặt, đường dẫn cũ không còn vào được. Các nút "Chi tiết" phải dùng Tag Helper (`asp-action`, `asp-route-id`) để tự sinh đúng đường dẫn, không viết tay.

**Hiện tiêu đề trang theo tên sinh viên**
- Ở trang chi tiết, gán tiêu đề bằng `ViewData` hoặc `ViewBag` (chọn một kiểu), ví dụ "Chi tiết: Nguyễn Văn An". `_Layout` đã dùng `ViewData["Title"]` cho tiêu đề tab.

## 7. Thứ tự làm và phụ thuộc

**Phải làm đăng nhập trước.** Cả web đã bị khóa, nên B chưa chạy thử được trang sinh viên khi chưa đăng nhập được.

- **Đợt 1 (song song):** A làm đăng nhập, đăng xuất; B làm danh sách và chi tiết sinh viên (B cứ viết code, chạy thử sau khi A merge xong đăng nhập).
- **Đợt 2:** A làm danh sách và thêm người dùng; B làm thêm và sửa sinh viên.
- **Đợt 3:** A làm sửa và xóa người dùng; B làm xóa sinh viên, đường dẫn gọn, tiêu đề trang.

Trong mỗi người, nút "Thêm/Sửa/Xóa" nằm trong trang danh sách nên làm danh sách trước.

## 8. Cách làm việc chung

1. Mỗi người một nhánh riêng, ví dụ `feature/a-login`, `feature/b-students-list`.
2. Mỗi việc nhỏ xong: `dotnet build` sạch error và warning, commit, `git pull --rebase origin main`, merge vào `main`.
3. Sau mỗi việc, xem `git diff` xem có file ngoài phạm vi được giao không; có thì hoàn tác.
4. Không tự thêm package, không đổi hợp đồng, không đổi lifetime dịch vụ, không sửa file của người kia.

## 9. Mẫu prompt giao việc cho agent

```
Đọc PHASE2.md và CLAUDE.md để hiểu bối cảnh.
Bạn đang làm cho người <A hoặc B>.
CHỈ làm việc: "<tên việc, ví dụ: Làm đăng nhập>".
Không làm các việc khác trong PHASE2.md, kể cả việc của người kia.
Không sửa file ngoài phạm vi được giao ở mục 4.
Đọc file liên quan trước khi sửa, không giả định cấu trúc.
Xong thì chạy dotnet build, báo danh sách file đã sửa và kết quả build.
```
