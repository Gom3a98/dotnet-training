using System;
using System.Collections.Generic;

namespace OnlineStore.Models;

public partial class Bookauthor
{
    public int AuthorId { get; set; }

    public int BookId { get; set; }

    public sbyte? AuthorOrder { get; set; }

    public int? RoyalityPercentage { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
