using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soaps.Model
{
    public class UserRegister
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Campo Primer Nombre es necesario llenarlo")]
        [DisplayName("Primer Nombre")]
        public string FirstName { get; set; }

        [DisplayName("Segundo Nombre")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "El Campo Apellidos es necesario llenarlo")]
        [DisplayName("Apellidos")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Imagen de Perfil")]
        public byte[] ProfileImage { get; set; }

        [Required]
        public bool Active { get; set; }

        [NotMapped]
        [DisplayName("Imagen de Perfil")]
        public IFormFile ImageFile { get; set; }

        public string UserRegisterOfApplicationUser { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
