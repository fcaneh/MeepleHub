using System;
using System.Collections.Generic;
using System.Text;

namespace MeepleHub.Domain.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Game>? Games { get; set; }
    }
}
