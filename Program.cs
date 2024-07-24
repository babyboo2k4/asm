// See https://aka.ms/new-console-template for more information
namespace SpringHeroBank
{
    class Program
    {
        static List<User> users = new List<User>();
        static List<Transaction> transactions = new List<Transaction>();
        static User currentUser = null;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Spring Hero Bank");
                Console.WriteLine("1.Register");
                Console.WriteLine("2.Log in.");
                Console.WriteLine("3.Exit");
                Console.Write("Choose your options (1,2,3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        static void Register()
        {
            Console.Clear();
            Console.WriteLine("Register");

            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Account number ");
            string accountNumber = Console.ReadLine();
            Console.Write("Phone: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            User newUser = new User(name, accountNumber, phoneNumber, password);
            users.Add(newUser);

            Console.WriteLine("Register success");
            Console.ReadKey();
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("Log in");

            Console.Write("Your account number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Password ");
            string password = Console.ReadLine();

            foreach (var user in users)
            {
                if (user.AccountNumber == accountNumber && user.Password == password)
                {
                    currentUser = user;
                    if (currentUser.IsAdmin)
                    {
                        AdminMenu();
                    }
                    else
                    {
                        UserMenu();
                    }
                    return;
                }
            }

            Console.WriteLine("Log in failed");
            Console.ReadKey();
        }

        static void UserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Spring Hero Bank ——");
                Console.WriteLine($"Welcome back {currentUser.Name}. Please choose your choice");
                Console.WriteLine("1. Gửi tiền.");
                Console.WriteLine("2. Rút tiền.");
                Console.WriteLine("3. Chuyển khoản.");
                Console.WriteLine("4. Truy vấn số dư.");
                Console.WriteLine("5. Thay đổi thông tin cá nhân.");
                Console.WriteLine("6. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("7. Truy vấn lịch sử giao dịch.");
                Console.WriteLine("8. Thoát.");
                Console.Write("Nhập lựa chọn của bạn (Từ 1 đến 8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Deposit();
                        break;
                    case "2":
                        Withdraw();
                        break;
                    case "3":
                        Transfer();
                        break;
                    case "4":
                        CheckBalance();
                        break;
                    case "5":
                        UpdatePersonalInfo();
                        break;
                    case "6":
                        ChangePassword();
                        break;
                    case "7":
                        ViewTransactionHistory();
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Ngân hàng Spring Hero Bank ——");
                Console.WriteLine($"Chào mừng Admin {currentUser.Name} quay trở lại. Vui lòng chọn thao tác.");
                Console.WriteLine("1. Danh sách người dùng.");
                Console.WriteLine("2. Danh sách lịch sử giao dịch.");
                Console.WriteLine("3. Tìm kiếm người dùng theo tên.");
                Console.WriteLine("4. Tìm kiếm người dùng theo số tài khoản.");
                Console.WriteLine("5. Tìm kiếm người dùng theo số điện thoại.");
                Console.WriteLine("6. Thêm người dùng mới.");
                Console.WriteLine("7. Khoá và mở tài khoản người dùng.");
                Console.WriteLine("8. Tìm kiếm lịch sử giao dịch theo số tài khoản.");
                Console.WriteLine("9. Thay đổi thông tin tài khoản.");
                Console.WriteLine("10. Thay đổi thông tin mật khẩu.");
                Console.WriteLine("11. Thoát.");
                Console.Write("Nhập lựa chọn của bạn (Từ 1 đến 11): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    // Thêm các case cho các lựa chọn của admin ở đây.
                    case "11":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        static void Deposit()
        {
            Console.Clear();
            Console.WriteLine("—— Gửi tiền ——");
            Console.Write("Nhập số tiền: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            currentUser.Balance += amount;
            transactions.Add(new Transaction(currentUser.AccountNumber, "Deposit", amount));

            Console.WriteLine("Gửi tiền thành công!");
            Console.ReadKey();
        }

        static void Withdraw()
        {
            Console.Clear();
            Console.WriteLine("—— Rút tiền ——");
            Console.Write("Nhập số tiền: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (currentUser.Balance >= amount)
            {
                currentUser.Balance -= amount;
                transactions.Add(new Transaction(currentUser.AccountNumber, "Withdraw", amount));

                Console.WriteLine("Rút tiền thành công!");
            }
            else
            {
                Console.WriteLine("Số dư không đủ!");
            }
            Console.ReadKey();
        }

        static void Transfer()
        {
            Console.Clear();
            Console.WriteLine("—— Chuyển khoản ——");
            Console.Write("Nhập số tài khoản nhận: ");
            string receiverAccount = Console.ReadLine();
            Console.Write("Nhập số tiền: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            User receiver = users.Find(u => u.AccountNumber == receiverAccount);

            if (receiver != null && currentUser.Balance >= amount)
            {
                currentUser.Balance -= amount;
                receiver.Balance += amount;
                transactions.Add(new Transaction(currentUser.AccountNumber, "Transfer", amount, receiverAccount));

                Console.WriteLine("Chuyển khoản thành công!");
            }
            else
            {
                Console.WriteLine("Chuyển khoản thất bại!");
            }
            Console.ReadKey();
        }

        static void CheckBalance()
        {
            Console.Clear();
            Console.WriteLine("—— Truy vấn số dư ——");
            Console.WriteLine($"Số dư hiện tại: {currentUser.Balance:C}");
            Console.ReadKey();
        }

        static void UpdatePersonalInfo()
        {
            Console.Clear();
            Console.WriteLine("—— Thay đổi thông tin cá nhân ——");

            Console.Write("Nhập tên mới: ");
            string newName = Console.ReadLine();
            Console.Write("Nhập số điện thoại mới: ");
            string newPhone = Console.ReadLine();

            currentUser.Name = newName;
            currentUser.PhoneNumber = newPhone;

            Console.WriteLine("Thay đổi thông tin cá nhân thành công!");
            Console.ReadKey();
        }

        static void ChangePassword()
        {
            Console.Clear();
            Console.WriteLine("—— Thay đổi thông tin mật khẩu ——");

            Console.Write("Nhập mật khẩu mới: ");
            string newPassword = Console.ReadLine();

            currentUser.Password = newPassword;

            Console.WriteLine("Thay đổi mật khẩu thành công!");
            Console.ReadKey();
        }

        static void ViewTransactionHistory()
        {
            Console.Clear();
            Console.WriteLine("—— Lịch sử giao dịch ——");

            List<Transaction> userTransactions = transactions.FindAll(t => t.AccountNumber == currentUser.AccountNumber);

            const int pageSize = 5;
            int pageNumber = 0;
            int totalRecords = userTransactions.Count;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("—— Lịch sử giao dịch ——");
                for (int i = pageNumber * pageSize; i < (pageNumber + 1) * pageSize && i < totalRecords; i++)
                {
                    Console.WriteLine(userTransactions[i]);
                }
                Console.WriteLine($"Trang {pageNumber + 1}/{(totalRecords + pageSize - 1) / pageSize}");

                Console.WriteLine("Nhấn '>' để sang trang tiếp, '<' để về trang trước, ESC để thoát.");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.RightArrow)
                {
                    pageNumber = (pageNumber + 1) % ((totalRecords + pageSize - 1) / pageSize);
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    pageNumber = (pageNumber == 0) ? ((totalRecords + pageSize - 1) / pageSize) - 1 : pageNumber - 1;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}