namespace Console_Library_Management_Tool;

/// <summary>
/// Tool class to get a user logged in.
/// </summary>
public static class Login
{
    /// <summary>
    /// Gets a user logged in.
    /// </summary>
    /// <param name="userDatabase">The database of users.</param>
    /// <returns>The user that is logging in.</returns>
    public static IUser LoginUser(List<IUser> userDatabase)
    {
        string text;

        text =
            $"""
                Enter your username: 
                """;
        Console.Write(text);
        string username = Console.ReadLine();

        text =
            $"""
                
                Enter your password:
                """;
        Console.Write(text);
        string password = Console.ReadLine();

        foreach (var user in userDatabase)
        {
            if (user.Name.Equals(username) && user.Password.Equals(password))
            {
                return user;
            }
        }

        text =
            $"""

                Invalid username or password. Please try again.
                """;
        Console.Write(text);
        Console.ReadKey();
        return null;
    }

    /// <summary>
    /// Creates a user.
    /// </summary>
    /// <param name="userDatabase">The database of users.</param>
    /// <returns>The new user profile.</returns>
    /// <remarks>Creating a new user with this method will only output a Customer class. To create a new Employee, it'll need to either be manually added to the user database, or be created by an Employee in his profile menu.</remarks>
    public static IUser CreateUser(List<IUser> userDatabase)
    {
        string text;
        
        text = 
            $"""
            Input new username: 
            """;
        Console.Write(text);
        string username = Console.ReadLine();
        
        text = 
            $"""

            Input new password: 
            """;
        Console.Write(text);
        string password = Console.ReadLine();

        var newCustomer = new Customer(username, password);
        userDatabase.Add(newCustomer);
        return newCustomer;
    }
}