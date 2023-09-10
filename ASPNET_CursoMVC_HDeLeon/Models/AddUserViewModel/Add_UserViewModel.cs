using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//V#5 IniciandoCrud Validaciones y agregar usuario con EF
//Agregar las librerias correspondientes 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CursoMVC_HDeLeon.Models.AddUserViewModel
{
    public class Add_UserViewModel
    {
        [Required]
        //[DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required]
        //[DataType(DataType)] NO Va ya que EF automáticamente lo identifica y asigna
        public int Edad { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(20,MinimumLength = 4,ErrorMessage = "El campo {0} debe tener una longitud mínima de {2} y una máxima de {1}")]
        public string Password { get; set; }

        //[NotMapped]No lo voy a utilizar 
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coiciden")]
        public string ConfirmarPassword { get; set; }

        [Required]
        public int IdState { get; set; }
    }

    //V#6 Siguiendo crud Editar y Eliminar con Entity Framework
    //Este modelo donde se incluyen las propiedades del usuario para poder agregarlos
    //se puede utilizar para editar el usuario ya que los campos son similares
    public class Edit_UserViewModel
    {
        //Por último se agrego el ID ya que en este caso se realiza una búsqueda por id
        public int Id { get; set; }

        [Required]
        //[DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required]
        //[DataType(DataType)] NO Va ya que EF automáticamente lo identifica y asigna
        public int Edad { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener una longitud mínima de {2} y una máxima de {1}")]
        public string Password { get; set; }

        //[NotMapped]No lo voy a utilizar 
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coiciden")]
        public string ConfirmarPassword { get; set; }

        [Required]
        public int IdState { get; set; }
    }
}