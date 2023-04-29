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
    /// A reference to the user database.
    /// </summary>
    private List<IUser> Users { get; init; }

    /// <summary>
    /// A reference to the user currently logged in. If null, then no user is logged in.
    /// </summary>
    private IUser? User { get; set; }

    /// <summary>
    /// Initialize a menu class with a reference to the library and user database, and with no user currently logged in.
    /// </summary>
    /// <param name="library">The library database.</param>
    /// <param name="users">The user database.</param>
    public Menu(Library library, List<IUser> users)
    {
        _library = library;
        User = null;
        Users = users;
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
                text = 
                    $"""
                    Unknown user, please login or create new profile.
                    L - login
                    C - create a new profile


                    """;
                Console.Write(text);
                
                switch (Console.ReadLine().ToUpper())
                {
                    case "L":
                        Console.Clear();
                        User = Login.LoginUser(Users);
                        break;
                    case "C":
                        Console.Clear();
                        User = Login.CreateUser(Users);
                        break;
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
            }
            
            Console.Clear();
        }
    }
}