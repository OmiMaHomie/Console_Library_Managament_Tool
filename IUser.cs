namespace Console_Library_Management_Tool;

/// <summary>
/// Defines a contract that all users must follow.
/// </summary>
public interface IUser
{
    /// <summary>
    /// Name of the user.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// User's password.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// List of books that the user has checked out.
    /// </summary>
    public List<Book> CheckedOutBooks { get; init; }

    /// <summary>
    /// Checks out a book from the library.
    /// </summary>
    /// <param name="library">Reference to the library database.</param>
    /// <param name="book">The book to be checked out.</param>
    public void CheckOutBook(Library library, Book book);

    /// <summary>
    /// Returns a book to the library.
    /// </summary>
    /// <param name="library">Reference to the library database.</param>
    /// <param name="book">The book to be returned.</param>
    public void ReturnBook(Library library, Book book);

    /// <summary>
    /// Displays the user's profile menu.
    /// </summary>
    /// <param name="library">Reference to the library database.</param>
    public void ProfileMenu(Library library);
}