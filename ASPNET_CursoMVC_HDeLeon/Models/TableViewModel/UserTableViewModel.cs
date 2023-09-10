using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNET_CursoMVC_HDeLeon.Models.TableViewModel
{
    //V#4 Layout, Razor, Listas
    //En este modelo es posible especificar que propiedades se van a mostrar y que otras no se van a mostrar¿
    public class UserTableViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int IdState { get; set; }
    }
}