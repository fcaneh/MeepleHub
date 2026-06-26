using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<UserGame>? UserGames { get; set; }
        public ICollection<Loan> BorrowedLoans { get; set; } = new List<Loan>();
    }
}
