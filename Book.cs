namespace Console_Library_Management_Tool;

/// <summary>
/// Holds all the information about a particular book.
/// </summary>
public struct Book
{
    /// <summary>
    /// Name of the book.
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// Writer(s) of the book.
    /// </summary>
    public string[] Authors { get; init; }
    
    /// <summary>
    /// A 13 digit number that uniquely identifies a book.
    /// </summary>
    public ulong Isbn13 { get; init; }
    
    /// <summary>
    /// The publisher of the book.
    /// </summary>
    public string Publisher { get; init; }
    
    /// <summary>
    /// The date the book was published.
    /// </summary>
    public DateOnly PublicationDate { get; init; }
    
    /// <summary>
    /// The language the book is written in.
    /// </summary>
    public string Language { get; init; }
    
    /// <summary>
    /// The genre(s) associated with the book.
    /// </summary>
    public string[] Genres { get; init; }
    
    /// <summary>
    /// The number of pages in the book.
    /// </summary>
    public ushort Pages { get; init; }
    
    /// <summary>
    /// Holds the availability of the book.
    /// </summary>
    public bool IsAvailable { get; init; }

    /// <summary>
    /// Instantiates a book with custom values for each property, EXCEPT for IsAvailable. That is set to true by default.
    /// </summary>
    /// <param name="title">Title of the book.</param>
    /// <param name="authors">Author(s) of the book.</param>
    /// <param name="isbn13">Unique 13-digit code unique to the book.</param>
    /// <param name="publisher">Publisher of the book.</param>
    /// <param name="publicationDate">Book's date of publication.</param>
    /// <param name="language">Language the book is written in.</param>
    /// <param name="genres">The associated genre(s) of the book.</param>
    /// <param name="pages">The # of pages in the book.</param>
    public Book(string title, string[] authors, ulong isbn13, string publisher, DateOnly publicationDate, string language, string[] genres, ushort pages)
    {
        Title = title;
        Authors = authors;
        Isbn13 = isbn13;
        Publisher = publisher;
        PublicationDate = publicationDate;
        Language = language;
        Genres = genres;
        Pages = pages;
        IsAvailable = true;
    }

    /// <summary>
    /// Returns a shorter description of the book object, that only includes the book's title and author(s).
    /// </summary>
    /// <returns>A shorter representation of the book object.</returns>
    public string ShortDescription() => $"{Title} by {string.Join(", ", Authors)}";

    /// <summary>
    /// Returns a string representation of the book object, that include all of the book's properties.
    /// </summary>
    /// <returns>A string representation of the book object.</returns>
    public override string ToString() => 
        $"""
        Title: {Title}
        Authors: {string.Join(", ", Authors)}
        ISBN: {Isbn13.ToString().PadLeft(13, '0')}
        Publisher: {Publisher}
        Publication Date: {PublicationDate}
        Language: {Language}
        Genres: {string.Join(", ", Genres)}
        Pages: {Pages}
        {(IsAvailable ? "Available" : "Not Available")}
        """;
}