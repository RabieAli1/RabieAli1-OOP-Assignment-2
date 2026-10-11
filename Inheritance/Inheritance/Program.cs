

DateTime today = new DateTime(2026, 10, 1);

HeadLibrarian head = new HeadLibrarian("S3", "Hoda Samir", "01000000003", new DateTime(2018, 1, 1), 9000);
Librarian librarian = new Librarian("S1", "Mona Adel", "01000000001", new DateTime(2020, 3, 1), 6000);
Shelver shelver = new Shelver("S2", "Omar Khaled", "01000000002", new DateTime(2022, 6, 1), 4000, "Fiction");

StudentMember student = new StudentMember("M1", "Ahmed Hassan", "01111111111");
PremiumMember premium = new PremiumMember("M2", "Sara Youssef", "01222222222", 10);

Book book1 = new Book("B1", "Clean Code", 2);
Book book2 = new Book("B2", "The Pragmatic Programmer", 2);
Book book3 = new Book("B3", "Refactoring", 2);
Book book4 = new Book("B4", "Design Patterns", 2);
Dvd dvd = new Dvd("D1", "Inception", 2);
Magazine magazine = new Magazine("G1", "National Geographic", 2);

// 1) Borrow a withdrawn item
Console.WriteLine("--- Borrow a withdrawn item");
try
{
    head.Withdraw(book4);
    student.Borrow("L-W", book4, today);
}
catch (Exception ex)
{
    Console.WriteLine("Rejected: " + ex.Message);
}
head.Restore(book4);

// 2) Borrow an item already on loan
Console.WriteLine("--- Borrow an item already on loan");
try
{
    student.Borrow("L-1", book1, today);
    premium.Borrow("L-2", book1, today);
}
catch (Exception ex)
{
    Console.WriteLine("Rejected: " + ex.Message);
}

// 3) Student borrows a 4th item
Console.WriteLine("--- Student borrows a 4th item");
try
{
    student.Borrow("L-3", book2, today);
    student.Borrow("L-4", book3, today);
    student.Borrow("L-5", book4, today);
}
catch (Exception ex)
{
    Console.WriteLine("Rejected: " + ex.Message);
}

// 4) Staff in one list: monthly pay
Console.WriteLine("--- Monthly pay");
List<Staff> staffList = new List<Staff> { librarian, head, shelver };
foreach (Staff staff in staffList)
{
    Console.WriteLine(staff.FullName + ": " + staff.GetMonthlyPay());
}

// 5) Items in one list: loan period and daily late fee
Console.WriteLine("--- Items");
List<LibraryItem> items = new List<LibraryItem> { book1, dvd, magazine };
foreach (LibraryItem item in items)
{
    Console.WriteLine(item.Title + ": loan period " + item.LoanPeriodDays +
                      " days, daily late fee " + item.GetDailyLateFee());
}

// 6) Premium member returns a DVD 5 days late
Console.WriteLine("--- Premium member returns a DVD 5 days late");
Loan dvdLoan = premium.Borrow("L-6", dvd, today);
librarian.ProcessReturn(dvdLoan, dvdLoan.DueDate.AddDays(5));
Console.WriteLine("Due date: " + dvdLoan.DueDate.ToString("yyyy-MM-dd"));
Console.WriteLine("Late fee: " + dvdLoan.LateFee);
Console.WriteLine("Reading points: " + premium.ReadingPoints);

// 7) Return the same loan twice
Console.WriteLine("--- Return the same loan twice");
try
{
    librarian.ProcessReturn(dvdLoan, dvdLoan.DueDate.AddDays(6));
}
catch (Exception ex)
{
    Console.WriteLine("Rejected: " + ex.Message);
}

// 8) Mark a returned loan as lost
Console.WriteLine("--- Mark a returned loan as lost");
try
{
    librarian.MarkAsLost(dvdLoan);
}
catch (Exception ex)
{
    Console.WriteLine("Rejected: " + ex.Message);
}
