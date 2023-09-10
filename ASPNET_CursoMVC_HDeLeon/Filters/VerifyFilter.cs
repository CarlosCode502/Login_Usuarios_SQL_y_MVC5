using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//V#3 Filtros y seguridad
using System.Web.Mvc;
using ASPNET_CursoMVC_HDeLeon.Controllers;
using ASPNET_CursoMVC_HDeLeon.Models;

namespace ASPNET_CursoMVC_HDeLeon.Filters
{
    //Hereda de una clase 
    public class VerifyFilter : ActionFilterAttribute
    {
        //Se sobrecarga un método que cuando se ejecute el filtro
        //Override es como decirle al programa que primero realice mis instrucciones y luego las de este.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //V#3 Filtros y seguridad
            //Se valida la sesión del usuario(Otra forma similar en AccesController)
            //oUser contiene las credenciales de login del usuario

            //V#7 Cerrar Sesión (Cuando se guarda un password o un dato en el navegador se almacena como Cookie(en algún lado))
            //Cuando se hace uso de el filtro ".Current.Session["User"];" se guarda el inicio de sesión en el navegador
            var oUser = (User)HttpContext.Current.Session["User"];

            //Verifica si esta vacio se ejecuta esta condición
            if(oUser == null)
            {
                //En donde se valida que si no existen datos en el logión los enlaces a los que se hagan click 
                //Siempre redigiran a la página de acceso(login)
                //Si el controlador al que se hace clic o se desea entrar no es Acces entonces lo va redireccionar hacia este mismo 
                if(filterContext.Controller is AccesController == false)
                {
                    //Enlace o ruta a la que se redirigira si se intentan ingresar sin un logeo
                    filterContext.HttpContext.Response.Redirect("~/Acces/Index");
                }
            }
            else
            {
                //V#7 Cerrar Sesión 
                //El método de cerrar sesión no puede estar junto con el de iniciar sesión, ya que debido a esta validación de abajo 
                //Ya que luego de iniciar sesión ya no sera posible acceder al controlador(Acces) para cerrar sesión y por lo tanto
                //no va a ser posible cerrar sesión. (Por eso se debe de crear otro controlador que contendra el cerrar sesión).

                //V#3 Filtros y seguridad
                //En caso de que el usuario ya haya validado sus datos se ejecutara este apartado
                //Donde si el usuario intenta ir a la url del login se redireccinara
                if (filterContext.Controller is AccesController == true)
                {
                    //Ruta a la que se le retornará el usuario 
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }

            //Instrucciones propias de la clase se ejecutan después
            //Si no se ejecuta el if o la condición anterior 
            //Pasa dirrectamente al index ya que el usuario se encuentra logueado correctamente
            base.OnActionExecuting(filterContext);
        }
    }
}