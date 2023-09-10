using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//V#2 Entity Framework y autentificación
//Importar las librerias correspondientes
using ASPNET_CursoMVC_HDeLeon.Models;

namespace ASPNET_CursoMVC_HDeLeon.Controllers
{
    public class AccesController : Controller
    {
        // GET: Acces
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Ingresar()
        //{
        //    return View();
        //}

        //V#1 Introducción a controladores y vistas HDL
        public ActionResult Ingresar(string user, string password)
        {
            try
            {
                //V#2 Entity Framework y autentificación
                //creamos la instancia de la bd dentro de un using para que nos permita tener acceso a distintos parametros
                using(BD_CursoMVC_HDeLeonEntities db = new BD_CursoMVC_HDeLeonEntities())
                {
                    //consulta con linq
                    //Linq es mejor utilizado para manejar las consultas sql sobre c#
                    //en este caso con EF ayuda a realizar los querys a la bd
                    //list va contener el resultado de la op, "d" va a servir para acceder a las propiedades del modelo(Tabla) Users
                    var list = from d in db.Users
                    //Se hará la consulta cuando los parametros(datos que ingresa el usuario en la vista) se encuentre en la bd
                               where d.Email == user && d.Password == password &&  d.idState==1
                    //Contiene el resultado de la consulta
                               select d;

                    //Valida si existe el usuario
                    if(list.Count()>0)
                    {
                        //Crea una instancia del modelo = Trae un tipo de dato llamado UIuser(IQueryable)
                        User oUser = list.First();
                        //Crea una sesión del usuario = Contiene el IQ
                        Session["User"] = oUser;
                        return Content("1");
                    }
                    else
                    {
                        return Content("Usuario Inválido");
                    }
                }
                ////V#1 Introducción a controladores y vistas HDL
                //return Content("1");
            }
            catch (Exception ex)
            {
                //V#1 Introducción a controladores y vistas HDL
                //Content es un String regresa una función en lugar de una vista
                return Content("Ocurrio un error :(" + ex.Message);
            }

            //ya no se utiliza porque se retorna un content
            //return View();
        }

        ////V#7 Cerrar Sesión 
        ////método para cerrar sesión
        //public ActionResult LogOff()
        //{
        //    //Establece la sesión del usuario como nula(se cierra la sesión)
        //    //Controller "User"
        //    Session["User"] = null;
            
        //    return applica
        //}
    }
}