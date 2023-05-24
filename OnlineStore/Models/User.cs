using System;
using System.Collections.Generic;

namespace OnlineStore.Models;

public partial class User
{
    public int UserId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public byte[] Password { get; set; } = null!;
    public byte[] SaltPassword { get; set; } = null!;

    public string Source { get; set; } 

    public string? FirstName { get; set; } = null!;

    public string? MiddleName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public int RoleId { get; set; } = 1;

    public int PubId { get; set; } = 1;

    public DateTime? HireDate { get; set; }

    public virtual Publisher Pub { get; set; } 

    public virtual ICollection<Refreshtoken> Refreshtokens { get; set; }

    public virtual Role Role { get; set; } 

    public User() {
        Refreshtokens = new List<Refreshtoken>();
    }
}
