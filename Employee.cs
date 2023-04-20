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
    /// Initializes an employee with a custom name and password, and an empty list of checked out books.
    /// </summary>
    /// <param name="name">Desired name of the employee.</param>
    /// <param name="password">Password the user designated for this profile.</param>
    public Employee(string name, string password)
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
}