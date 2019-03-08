using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace La_Hermandad.Models
{
    public class Comics
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdComics { get; set; }
        public string Titulo { get; set; }
        public byte[] Portada { get; set; }
        public DateTime FechadeEstreno { get; set; }

        public virtual ICollection<PaginasComics> Pages { get; set; }

    }
}