using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//V#4 Layout, Razor, Listas
//Se importa la libreria correspondiente
using ASPNET_CursoMVC_HDeLeon.Models;
using ASPNET_CursoMVC_HDeLeon.Models.TableViewModel;
//V#5 IniciandoCrud Validaciones y agregar usuario con EF
using ASPNET_CursoMVC_HDeLeon.Models.AddUserViewModel;

namespace ASPNET_CursoMVC_HDeLeon.Controllers
{
    //V#4 Layout, Razor, Listas
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            //V#4 Layout, Razor, Listas
            //Se crea una lista que hace referencia al modelo con las propiedades que se van a mostrar
            List<UserTableViewModel> lst = null;

            //Mostrar la información(el contexto)
            //Se carga el contexto de la bd el DBEntities
            using(BD_CursoMVC_HDeLeonEntities bd = new BD_CursoMVC_HDeLeonEntities())
            {
                //Como llenar la lista "lst" (a través de LINQ)
                //La lista se llena a través de código linq
                lst = (from d in bd.Users
                       where d.idState != 3
                       orderby d.Id
                       select new UserTableViewModel //Se crea un nuevo usuario
                       {
                           Id = d.Id,
                           Nombre = d.Nombre,
                           Edad = d.Edad,
                           Email = d.Email,
                           IdState = d.idState
                       }).ToList();
                //Al final se convierte en una lista 
            }
            //Mandar el modelo para poder trabajar
            return View(lst);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Add_UserViewModel addModel)
        {
            //Validar los dataAnotation que va a retornar un objeto true o false
            if(!ModelState.IsValid)
            {
                return View(addModel);
            }
            
            //Crear un contexto y guardarlo en la BD
            using (BD_CursoMVC_HDeLeonEntities BD = new BD_CursoMVC_HDeLeonEntities())
            {
                //Se crea un objeto con la estructura del modelo(propiedades) donde se va a recibir
                //y asignar el valor a todos esos datos que se reciben dede Add(Vista)

                User oUser = new User();
                //oUser.idState = 1;//Cada nuevo usuario que se cree va a tener este valor = 1(Activo)
                oUser.Nombre = addModel.Nombre;
                oUser.Edad = addModel.Edad;
                oUser.Email = addModel.Email;
                oUser.Password = addModel.Password;
                oUser.idState = addModel.IdState;

                //Agregamos el nuevo objeto a la BD (Directamente a la tabla Users)
                BD.Users.Add(oUser);

                //Por cada insert exitoso que hagamos se deben guardar los cambios
                BD.SaveChanges();
            }
            return Redirect(Url.Content("~/User/Index"));
        }

        //V#6 Siguiendo crud Editar y Eliminar con Entity Framework
        public ActionResult Edit(int Id)
        {
            //Creamos una instancia del modelo Editar
            Edit_UserViewModel modelo_Edit = new Edit_UserViewModel();

            //Creamos el contexto de la Bd
            using (BD_CursoMVC_HDeLeonEntities DB = new BD_CursoMVC_HDeLeonEntities())
            {
                //Se va a crear un objeto y se va a mandar el ID para editar todos los datos correspondientes 
                //en base al id
                var oUser = DB.Users.Find(Id);
                modelo_Edit.Id = oUser.Id;
                modelo_Edit.Nombre = oUser.Nombre;
                modelo_Edit.Edad = oUser.Edad;
                modelo_Edit.Email = oUser.Email;
                modelo_Edit.Password = oUser.Password;
                modelo_Edit.IdState = oUser.idState;
                //modelo_Edit.ConfirmarPassword = oUser.ConfirmarPassword; (En esta en la BD solo sirve para validar)
            }
            //Se retorna el modelo para que se cargen todos los datos en la Vista(Edit)
            return View(modelo_Edit);
        }

        //V#6 Siguiendo crud Editar y Eliminar con Entity Framework
        //SE RECIBEN LOS DATOS
        //A través del método httppost(HyperText Transfer Protocol) Https(HyperText Transfer Protocol Security)
        //Método post para validar los datos de los campos actualizados o no
        //se valida el valor de las propiedades (Modelo Add_UserViewModel y método Edit_UserViewModel)
        [HttpPost]
        public ActionResult Edit(Edit_UserViewModel modeloEdit)
        {
            //Se ejecuta una condición negada es decir que si los datos o el modelo el distinto a válido se muestran los 
            //msj de error y se vuelve a solicitar el ingreso de datos
            if(!ModelState.IsValid) 
            {
                return View(modeloEdit);
            }

            //Si el modelo es válido se pasará a la siguiente instrucción
            //Que es la de crear un contexto para trabajar con las BD y las Query
            using(BD_CursoMVC_HDeLeonEntities DB = new BD_CursoMVC_HDeLeonEntities())
            {
                //Se crea un objeto donde se guardan los datos del elemento a editar esto según su id
                var oUser = DB.Users.Find(modeloEdit.Id);
                //los campos editados son asignados y sobreescritos y se almacenan directo a la bd
                oUser.Nombre = modeloEdit.Nombre;
                oUser.Edad = modeloEdit.Edad;
                oUser.Email = modeloEdit.Email;
                //oUser.Password = modeloEdit.Password;
                oUser.idState = modeloEdit.IdState;

                //Esto valida si se edita o se escribe algo más dentro de la caja del password sino asi se queda
                //con su primer valor registrado
                if(modeloEdit.Password!= null && modeloEdit.Password.Trim() != "")
                {
                    oUser.Password = modeloEdit.Password;
                }

                //De esta manera se le indica a EF que este registro fue editado
                //Sin esta linea puede que no se guarden los registros
                DB.Entry(oUser).State = System.Data.Entity.EntityState.Modified;

                //Se guardan los cambios en la BD
                DB.SaveChanges();
            }

            return Redirect(Url.Content("~/User/Index"));
        }

        //Para este caso no se hará ningún borrado de la bd, sino que se realizará un borrado de la interfaz del usuario 
        //donde ya no se mostrará el registro eliminado (BORRADO LÓGICO)
        public ActionResult Delete(int id)
        {
            //Creamos el contexto de la bd para que pueda tener comunicación con este mismo
            using(BD_CursoMVC_HDeLeonEntities bd = new BD_CursoMVC_HDeLeonEntities())
            {
                //Se crea un objeto que va a recibir el id
                var oUser = bd.Users.Find(id);

                //Luego como en la consulta del Action(Index) se especifico que solo se muestren solo los registros 
                //con el id mayor a 0.
                //Entonces el registro del id actual pasa a tener el valor idState como 0 y a no mostrarse.
                oUser.idState = 3;

                //Como se hizo un borrado lógico solo de interfaz y no de bd, se procede a indicar a la bd o EF que 
                //el registro solo se EDITO no se elimino.
                bd.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                //guardar los cambios
                bd.SaveChanges();
            }

            //Se envia un contenido o un valor para validar a través de ajax
            return Content("0");
            //return Redirect(Url.Content("~/User/Index"));//YA NO SE RETORNA UNA VISTA
        }
    }
}