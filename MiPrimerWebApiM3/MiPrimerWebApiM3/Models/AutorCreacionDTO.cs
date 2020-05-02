using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Models
{
    public class AutorCreacionDTO
    {
        [Required]
        [StringLength(10, ErrorMessage = "El campo debe tener {1} caracteres o menos")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [Range(18, 100)]
        public int Edad { get; set; }
        [CreditCard]
        public string CreditCard { get; set; }
        [Url]
        public string Url { get; set; }
    }
}
