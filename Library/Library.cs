﻿namespace Console_Library_Management_Tool;

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
        var page = 1;
        const int PAGE_SIZE = 10;
        string text;

        while (true)
        {
            // Only loops from the start of the page to the end of the page.
            for (int index = (page - 1) * PAGE_SIZE; index < page * PAGE_SIZE && index < Books.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {Books[index].ShortDescription()}");
            }
            
            text = 
                $"""
                
                What would you like to do {user.Name}?
                
                N - Next page
                P - Previous page
                "Input the number of the book you want to view."                
                Q - Quit


                """;
            Console.Write(text);
            
            string input = Console.ReadLine().ToUpper();
            switch (input)
            {
                case "N":
                    if (page * PAGE_SIZE < Books.Count)
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
                case var _ when int.TryParse(input, out int index):
                    Console.Clear();
                    user.CheckOutBook(this, index - 1);

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