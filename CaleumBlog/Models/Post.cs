using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaelumBlog.Models
{
    public class Post
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Campo Obrigatório")] [StringLength(50)]
        [Display(Name ="Título da obra:")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage ="Campo Obrigatório")]
        public string Resumo { get; set; }

        public string Categoria { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public bool Publicado { get; set; }
    }
}
