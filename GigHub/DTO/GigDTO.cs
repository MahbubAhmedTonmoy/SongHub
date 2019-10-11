using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.DTO
{
    public class GigDTO
    {
        public int Id { get; set; }

        public bool IsCanceled { get; set; }

        public UserDTO Artist { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public GenreDTO Genre { get; set; }
    }
}