using System;
using System.Collections.Generic;

namespace OnlineStore.Models;

public partial class Refreshtoken
{
    public int TokenId { get; set; }

    public int UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public bool isTokenVerified { get; set; } = false;
    public virtual User User { get; set; } = null!;
}
