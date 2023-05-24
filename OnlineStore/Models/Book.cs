

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int PubId { get; set; }

    public decimal? Price { get; set; }

    public decimal? Advance { get; set; }

    public int? Royalty { get; set; }

    public int? YtdSales { get; set; }

    public string? Notes { get; set; }

    public string FrontCover { get; set; }
    public string BackCover { get; set; }

    public DateTime PublishedDate { get; set; }

    public virtual ICollection<Bookauthor> Bookauthors { get; set; } = new List<Bookauthor>();

    public virtual Publisher Pub { get; set; } = null!;
    
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

}