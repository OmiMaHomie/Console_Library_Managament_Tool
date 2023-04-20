namespace Console_Library_Management_Tool;

/// <summary>
/// Represents a normal user of the library.
/// </summary>
public class Customer : IUser
{
    public string Name { get; set; }
    
    public string Password { get; set; }

    public List<Book> CheckedOutBooks { get; init; }

    /// <summary>
    /// Instantiates a customer with a custom name and password, and an empty list of checked out books.
    /// </summary>
    /// <param name="name">Name of the user.</param>
    /// <param name="password">Password the user designated for this profile.</param>
    public Customer(string name, string password)
    {
        Name = name;
        Password = password;
        CheckedOutBooks = new();
    }

    public void CheckOutBook(Library library, Book book)
    {
        CheckedOutBooks.Add(book);

        int index = library.Books.FindIndex(b => b.Isbn13 == book.Isbn13);
        library.Books[index] = library.Books[index] with
        {
            IsAvailable = false
        };
    }

    public void ReturnBook(Library library, Book book)
    {
        CheckedOutBooks.Remove(book);
        
        int index = library.Books.FindIndex(b => b.Isbn13 == book.Isbn13);
        library.Books[index] = library.Books[index] with 
            { IsAvailable = true };
    }

    /// <summary>
    /// Displays the user's profile menu.
    /// </summary>
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
            Console.WriteLine(text);
            
            for (int i = 0; i < CheckedOutBooks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {CheckedOutBooks[i].ShortDescription()}");
            }

            text =
                $"""
                
                What would you like to do {Name}?
                Input the number of the book you'd like to return
                Q - Quit


                """;
            Console.Write(text);

            switch (Console.ReadLine().ToUpper())
            {
                case var input when int.TryParse(input, out int index):
                    Console.Clear();
                    
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
                        else
                        {
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

    /// <summary>
    /// Returns a string representation of the customer. This includes the name of the customer, the number of books checked out, and the password.
    /// </summary>
    /// <returns>A string representation of the customer object.</returns>
    public override string ToString() => 
        $"""
        Name: {Name}
        Password: {Password}
        {CheckedOutBooks.Count} book(s) checked out
        """;
}