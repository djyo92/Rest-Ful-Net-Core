using MiPrimerWebApiM3.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Entities
{
    public class Autor:IValidatableObject
    {
        public int Id { get; set; }
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
        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(this.Nombre))
            {
                var primeraLetra = this.Nombre[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper()) 
                {
                    yield return new ValidationResult("La priemra letra debe ser mayusucla");
                }
            }
        }
    }
}
