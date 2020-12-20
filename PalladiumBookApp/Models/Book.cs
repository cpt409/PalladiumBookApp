using System;
using System.Collections.Generic;

#nullable disable

namespace PalladiumBookApp.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public int Categoryid { get; set; }
        public string Name { get; set; }
        public bool? OwnedBool { get; set; }
        public int Gameid { get; set; }

        public virtual Category Category { get; set; }
        public virtual Game Game { get; set; }
    }
}
