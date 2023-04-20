namespace Console_Library_Management_Tool;

/// <summary>
/// Hold the logic for the library.
/// </summary>
public class Library
{
    /// <summary>
    /// Name of the library.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The database of books.
    /// </summary>
    public List<Book> Books { get; init; }

    /// <summary>
    /// Initializes a library with a name and a pre-existing database.
    /// </summary>
    /// <param name="name">Name of the library.</param>
    /// <param name="books">The database of books.</param>
    public Library(string name, List<Book> books)
    {
        Name = name;
        Books = books;
    }

    /// <summary>
    /// Will output a menu into the console, that'll allow the user to browse through the books in the database.
    /// </summary>
    /// <param name="user">The user currently logged in.</param>
    public void BrowseBooks(IUser user)
    {
        int page = 1;
        int pageSize = 10;
        string text;

        while (true)
        {
            // Only loops from the start of the page to the end of the page.
            for (int index = (page - 1) * pageSize; index < page * pageSize && index < Books.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {Books[index].ShortDescription()}");
            }
            
            text = 
                $"""
                
                What would you like to do {user.Name}?
                
                N - Next page
                P - Previous page
                Input the number of the book you'd like to check out.
                Q - Quit

                """;
            Console.WriteLine(text);
            
            string input = Console.ReadLine().ToUpper();
            switch (input)
            {
                case "N":
                    if (page * pageSize < Books.Count)
                    {
                        page++;
                    }
                    else
                    {
                        text = 
                            $"""
                            
                            You're already on the last page!
                            Press any key to continue...
                            """;
                        Console.Write(text);
                        Console.ReadKey();
                    }

                    break;
                case "P":
                    if (page > 1)
                    {
                        page--;
                    }
                    else
                    {
                        text = 
                            $"""
        
                            You're already on the first page!
                            Press any key to continue...
                            """;
                        Console.Write(text);
                        Console.ReadKey();
                    }

                    break;
                case var indexValue when int.TryParse(indexValue, out int index):
                    if (index > 0 && index <= Books.Count)
                    {
                        Console.Clear();
                        Console.WriteLine(Books[index - 1]);

                        if (Books[index - 1].IsAvailable)
                        {
                            text = 
                                $"""

                                Would you like to check out this book? (Y/N) 
                                """;
                            Console.Write(text);

                            string checkOutInput = Console.ReadLine().ToUpper();
                            if (checkOutInput == "Y")
                            {
                                user.CheckOutBook(this, Books[index - 1]);
                                text = 
                                    $"""
                                    {Books[index - 1].Title} checked out!
                                    Press any key to continue...
                                    """;
                                Console.Write(text);
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

                                Sorry, this book is currently checked out.
                                Press any key to continue...
                                """;
                            Console.Write(text);
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
                case "Q":
                    Console.Clear();
                    
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
    /// Returns a string representation of the library. This includes the name of the library and the number of books in the database.
    /// </summary>
    /// <returns>A string representation of the library object.</returns>
    public override string ToString() =>
        $"""
        {Name}
        {Books.Count} book(s)
        """;
}