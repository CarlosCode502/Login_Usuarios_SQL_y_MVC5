using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNET_CursoMVC_HDeLeon.Controllers
{
    public class LogOffController : Controller
    {
        //V#7 Cerrar sesión
        //Método para cerrar la sesión del usuario(se almacena en Cookies)
        public ActionResult Cerrar()
        {
            //Se establece la sesión actualmente abierta del usuario en null.
            Session["User"] = null;

            //El return es importante
            //Luego de cerrar la sesión se redirige al usuario a la página de inicio(login) 
            return RedirectToAction("Index", "Acces");
        }
    }
}