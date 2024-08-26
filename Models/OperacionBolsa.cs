using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace appdemo.Models
{
    public class OperacionBolsa 
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string NombreApellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La fecha de operación es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaOperacion { get; set; }

        // Propiedades booleanas para los checkboxes
        [CheckboxChecked]
        public bool SP500 { get; set; }
        public bool DowJones { get; set; }
        public bool BonosUS { get; set; } 
        

        [Required(ErrorMessage = "El monto a abonar es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El monto debe ser un número positivo")]
        public decimal MontoAbonar { get; set; }

    }

    // Método para validación para eleguir al menos un valor del checkbox
        public class CheckboxChecked : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var model = (OperacionBolsa)validationContext.ObjectInstance;

                if (!model.SP500 && !model.DowJones && !model.BonosUS)
                {
                    return new ValidationResult("Debes seleccionar al menos una opción.");
                }

                return ValidationResult.Success;
            }
        }

    
}