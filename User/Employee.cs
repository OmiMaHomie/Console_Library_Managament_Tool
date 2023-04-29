namespace Console_Library_Management_Tool;

//TODO: Let an employee create a new employee profile inside of the profile menu.
/// <summary>
/// Represents a high-level employee of the library.
/// </summary>
public class Employee : IUser
{
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public List<Book> CheckedOutBooks { get; init; }
    
    /// <summary>
    /// Holds a list of books that are to be modified.
    /// </summary>
    public List<Book> ToBeModifiedBooks { get; init; }

    /// <summary>
    /// Initializes an employee with a custom name and password, and an empty list of checked out books and books to be modified.
    /// </summary>
    /// <param name="name">Desired name of the employee.</param>
    /// <param name="password">Password the user designated for this profile.</param>
    public Employee(string name, string password)
    {
        Name = name;
        Password = password;
        CheckedOutBooks = new();
        ToBeModifiedBooks = new();
    }

    public void CheckOutBook(Library library, int index)
    {
        string text;

        if (index >= 0 && index < library.Books.Count)
        {
            Console.WriteLine(library.Books[index]);

            text =
                $"""

                    What would you like to do?

                    {(library.Books[index].IsAvailable ? "C - Check out book (for reading)" : "This book is currently checked out")}
                    {(!ToBeModifiedBooks.Contains(library.Books[index]) ? "D - Check out book (for Debugging)" : "This book is already in an admin list")}
                    R - Return to previous menu


                    """;
            Console.Write(text);

            string checkOutInput = Console.ReadLine().ToUpper();
            switch (checkOutInput)
            {
                case "C" when library.Books[index].IsAvailable:
                    CheckedOutBooks.Add(library.Books[index]);
                    library.Books[index].IsAvailable = false;

                    text =
                        $"""

                            {library.Books[index].Title} checked out!
                            Press any key to continue...
                            """;
                    Console.Write(text);
                    Console.ReadKey();

                    break;
                case "C" when !library.Books[index].IsAvailable:
                    text =
                        $"""

                            This book is currently checked out.
                            Press any key to continue...
                            """;
                    Console.Write(text);
                    Console.ReadKey();

                    break;
                case "D" when !ToBeModifiedBooks.Contains(library.Books[index]):
                    ToBeModifiedBooks.Add(library.Books[index]);

                    text =
                        $"""

                            {library.Books[index].Title} added to your admin list!
                            Press any key to continue...
                            """;
                    Console.Write(text);
                    Console.ReadKey();

                    break;
                case "D" when ToBeModifiedBooks.Contains(library.Books[index]):
                    text =
                        $"""

                            This book is already in your admin list.
                            Press any key to continue...
                            """;
                    Console.Write(text);
                    Console.ReadKey();

                    break;
                case "R":
                    break;
            }
        }
        else
        {
            text =
                $"""

                Invalid book input!
                Press any key to continue...
                """;
            Console.Write(text);
            Console.ReadKey();
        }
    }

    public void ReturnBook(Library library, Book book)
    {
        CheckedOutBooks.Remove(book);

        int index = library.Books.FindIndex(b => b.Isbn13 == book.Isbn13);
        library.Books[index].IsAvailable = true;
    }
    
    public void ProfileMenu(Library library)
    {
        string text;
        
        while (true)
        {
            text = 
                $"""
                Username : {Name}

                Books checked out:

                """;
            Console.Write(text);
            for (int i = 0; i < CheckedOutBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {CheckedOutBooks[i].ShortDescription()}");
            }

            text = 
                $"""
                
                Books to be modified:

                """;
            Console.Write(text);
            for (int i = 0; i < ToBeModifiedBooks.Count; i++)
            {
                Console.WriteLine($"D{i + 1}. {ToBeModifiedBooks[i].ShortDescription()}");
            }

            text =
                $"""
                
                What would you like to do {Name}?
                Input the number of the book you'd like to return
                Input "D" + the number of the book you'd like to modify
                Q - Quit


                """;
            Console.Write(text);

            string input = Console.ReadLine().ToUpper();
            switch (input)
            {
                case var _ when int.TryParse(input, out int index):
                    if (index > 0 && index <= CheckedOutBooks.Count)
                    {
                        text =
                            $"""

                            {CheckedOutBooks[index - 1]}
                                
                            Confirm return this book? (Y/N) 
                            """;
                        Console.Write(text);
                        
                        if (Console.ReadLine().ToUpper() == "Y")
                        {
                            text =
                                $"""

                                {CheckedOutBooks[index - 1].Title} has been returned to the library.
                                Press any key to continue...
                                """;
                            Console.Write(text);
                            
                            ReturnBook(library, CheckedOutBooks[index - 1]);
                            
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        text =
                            $"""

                                Invalid book input!
                                Press any key to continue...
                                """;
                        Console.Write(text);
                        Console.ReadKey();
                    }

                    break;
                case var _ when input.StartsWith("D") && int.TryParse(input[1..], out int debugBookIndex):
                    if (debugBookIndex > 0 && debugBookIndex <= ToBeModifiedBooks.Count)
                    {
                        Console.Clear();
                        ModifyBook(ToBeModifiedBooks[debugBookIndex - 1]);
                    }
                    else
                    {
                        text =
                            $"""

                                Invalid debug book input!
                                Press any key to continue...
                                """;
                        Console.Write(text);
                        Console.ReadKey();
                    }

                    break;
                case "Q":
                    
                    return;
                default:
                    text =
                        $"""

                            Invalid input!
                            Press any key to continue...
                            """;
                    Console.Write(text);
                    Console.ReadKey();
                    break;
            }

            Console.Clear();
        }
    }

    private void ModifyBook(Book book)
    {
        string text;

        while (true)
        {
            Console.WriteLine(book);
            text = 
                $"""

                What would you like to modify?
                TTL - Title
                AUT - Author(s)
                ISBN - ISBN13
                PUB - Publisher
                PUB_D - Publish Date
                LANG - Language
                GEN - Genre(s)
                PGS - Page Count
                AVLB - Availability (WARNING)
                R_B - Return book to library
                R - Return to profile menu

            
                """;
            // TODO: If a book is toggled available, remove it from any customer's personal library and notify them upon login.
            // TODO: IF a book is toggle unavailable and no users have it in their personal library, notify employees.
            Console.Write(text);
            
            string input = Console.ReadLine().ToUpper();
            switch (input)
            {
                case "TTL":
                    text = 
                        $"""
                        
                        New title: 
                        """;
                    Console.Write(text);
                    
                    book.Title = Console.ReadLine();
                    
                    break;
                case "AUT":
                    text = 
                        $"""
                        
                        New author(s) (separate with commas, no spaces between commas):  
                        """;
                    Console.Write(text);

                    book.Authors = Console.ReadLine().Split(',');
                    
                    break;
                case "ISBN":
                    text = 
                        $"""
                        
                        New ISBN13: 
                        """;
                    Console.Write(text);

                    if (ulong.TryParse(Console.ReadLine(), out ulong isbn13))
                    {
                        book.Isbn13 = isbn13;
                    }
                    else
                    {
                        text =
                            $"""

                            Invalid ISBN13 input!
                            Press any key to continue...
                            """;
                        Console.Write(text);
                        Console.ReadKey();
                    }
                    
                    break;
                case "PUB":
                    text = 
                        $"""
                        
                        New publisher: 
                        """;
                    Console.Write(text);

                    book.Publisher = Console.ReadLine();
                    
                    break;
                case "PUB_D":
                    text = 
                        $"""
                        
                        New publish date (MM/DD/YYYY): 
                        """;
                    Console.Write(text);
                    
                    if (DateOnly.TryParse(Console.ReadLine(), out var publishDate))
                    {
                        book.PublicationDate = publishDate;
                    }
                    else
                    {
                        text =
                            $"""

                            Invalid publish date input!
                            Press any key to continue...
                            """;
                        Console.Write(text);
                        Console.ReadKey();
                    }

                    break;
                case "LANG":
                    text = 
                        $"""
                        
                        New language: 
                        """;
                    Console.Write(text);
                    
                    book.Language = Console.ReadLine();
                    
                    break;
                case "GEN":
                    text = 
                        $"""
                        
                        New genre(s) (separate with commas, no spaces between commas): 
                        """;
                    Console.Write(text);
                    
                    book.Genres = Console.ReadLine().Split(',');
                    
                    break;
                case "PGS":
                    text = 
                        $"""
                        
                        New page count: 
                        """;
                    Console.Write(text);
                    
                    if (ushort.TryParse(Console.ReadLine(), out ushort pageCount))
                    {
                        book.Pages = pageCount;
                    }
                    else
                    {
                        text =
                            $"""

                            Invalid page count input!
                            Press any key to continue...
                            """;
                        Console.Write(text);
                        Console.ReadKey();
                    }

                    break;
                case "AVLB":
                    text = 
                        $"""
                        
                        WARNING: Be sure to toggle this book back to its original state, as this function can cause duplicate books in database or books that can't be checked out.
                        Toggle availability? (Y/N) 
                        """;
                    Console.Write(text);
                    
                    if (Console.ReadLine().ToUpper() == "Y")
                    {
                        book.IsAvailable = !book.IsAvailable;
                    }
                    
                    break;
                case "R_B":
                    ToBeModifiedBooks.Remove(book);

                    text = 
                        $"""
                        
                        {book.Title} has been returned to the library!
                        Press any key to continue...
                        """;
                    Console.Write(text);
                    Console.ReadKey();

                    return;
                case "R":
                    return;
                default:
                    text =
                        $"""

                        Invalid input!
                        Press any key to continue...
                        """;
                    Console.Write(text);
                    Console.ReadKey();

                    break;
            }
            
            Console.Clear();
        }
    }

    /// <summary>
    /// Returns a string representation of the employee. This includes the name of the employee, the number of books checked out, and their password.
    /// </summary>
    /// <returns>A string representation of the employee object.</returns>
    public override string ToString() =>
        $"""
        Name: {Name}
        Password: {Password}
        {CheckedOutBooks.Count} book(s) checked out
        """;
}