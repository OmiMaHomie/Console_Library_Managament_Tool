namespace Console_Library_Management_Tool;

/// <summary>
/// Tool class to generate books.
/// </summary>
public static class BookGenerator
{
    /// <summary>
    /// Random class to help with book randomization.
    /// </summary>
    private static readonly Random _random = new();
    
    /// <summary>
    /// Generates a list of randomized books.
    /// </summary>
    /// <param name="amount">The desired amount of books to be generated.</param>
    /// <returns></returns>
    public static List<Book> GenerateBooks(int amount)
    {
        var books = new List<Book>();
        
        for (int index = 0; index < amount; index++)
        {
            string title = $"Book {index + 1}";

            string[] authors = new string[_random.Next(1, 4)];
            for (int authorIndex = 0; authorIndex < authors.Length; authorIndex++)
            {
                authors[authorIndex] = $"Authors {authorIndex + 1}";
            }

            ulong isbn13 = (ulong)_random.NextInt64(0000000000000, 10000000000000);
            
            string publisher = $"Publisher {index + 1}";
            
            var publicationDate = new DateOnly(_random.Next(1900, 2023)
                , _random.Next(1, 12)
                , _random.Next(1, 29));
            
            string language = (_random.Next(1, 6)) switch
            {
                1 => "English",
                2 => "French",
                3 => "German",
                4 => "Spanish",
                _ => "Italian",
            };
            
            string[] genres = new string[_random.Next(1, 4)];
            for (int genreIndex = 0; genreIndex < genres.Length; genreIndex++)
            {
                genres[genreIndex] = $"Genre {genreIndex + 1}";
            }
            
            ushort pages = (ushort)_random.Next(1, 1001);
            
            books.Add(new(title, authors, isbn13, publisher, publicationDate, language, genres, pages));
        }

        return books;
    }
}