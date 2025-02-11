namespace BoxedApiApplication1.ViewModels;

/// <summary>
/// The options used to request a page.
/// </summary>
public class PageOptions
{
    /// <summary>
    /// Gets or sets the number of items to retrieve from the beginning.
    /// </summary>
    /// <example>10</example>
    public int? First { get; set; }

    /// <summary>
    /// Gets or sets the number of items to retrieve from the end.
    /// </summary>
    /// <example></example>
    public int? Last { get; set; }

    /// <summary>
    /// Gets or sets the cursor of the item after which items are requested.
    /// </summary>
    /// <example></example>
    public string? After { get; set; }

    /// <summary>
    /// Gets or sets the cursor of the item before which items are requested
    /// </summary>
    /// <example></example>
    public string? Before { get; set; }
}
