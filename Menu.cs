namespace Console_Library_Management_Tool;

/// <summary>
/// Acts as the main menu for the library management tool.
/// </summary>
public class Menu
{
    /// <summary>
    /// A reference to the library database.
    /// </summary>
    private readonly Library _library;

    /// <summary>
    /// A reference to the user currently logged in. If null, then no user is logged in.
    /// </summary>
    private IUser User { get; set; }

    /// <summary>
    /// Initialize a menu class with a reference to the library database and the user currently logged in.
    /// </summary>
    /// <param name="library">The library database.</param>
    /// <param name="user">The user initially logged in.</param>
    public Menu(Library library, IUser user)
    {
        _library = library;
        User = user;
    }

    /// <summary>
    /// Displays the main menu for the library management tool.
    /// </summary>
    public void MainMenu()
    {
        string text;
        
        while (true)
        {
            if (User == null)
            {
                // TODO: Implement a login menu.
            }
            else
            {
                text = 
                    $"""
                    Welcome to {_library.Name}, {User.Name}!
                    
                    What would you like to do? 
                    B - Browse books
                    P - Check profile
                    Q - Quit

                    """;
                Console.WriteLine(text);

                switch (Console.ReadLine().ToUpper())
                {
                    case "B":
                        Console.Clear();
                        _library.BrowseBooks(User);
                        break;
                    case "P":
                        Console.Clear();
                        User.ProfileMenu(_library);
                        break;
                    case "Q": 
                        Console.WriteLine("\nHave a nice day!");
                        return;
                }

                Console.Clear();
            }
        }
    }
}