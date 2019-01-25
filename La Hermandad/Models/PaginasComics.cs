using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace La_Hermandad.Models
{
    public class PaginasComics
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdPaginaC { get; set; }
        public byte[] Paginas {get; set;}
        public int Id_Comic { get; set; }
    }
}