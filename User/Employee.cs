namespace Console_Library_Management_Tool;

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
        throw new NotImplementedException();
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