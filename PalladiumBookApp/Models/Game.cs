using System;
using System.Collections.Generic;

#nullable disable

namespace PalladiumBookApp.Models
{
    public partial class Game
    {
        public Game()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
