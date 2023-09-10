using System.Web;
using System.Web.Mvc;
//V#3 Filtros y seguridad
//using ASPNET_CursoMVC_HDeLeon.Filters;

namespace ASPNET_CursoMVC_HDeLeon
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //V#3 Filtros y seguridad
            //Existen filtros propios para la seguridad que se crean automáticamente
            filters.Add(new Filters.VerifyFilter());
        }
    }
}
