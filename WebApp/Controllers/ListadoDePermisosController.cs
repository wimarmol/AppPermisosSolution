using BLL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApp.Controllers
{
    public class ListadoDePermisosController : Controller
    {
        public IActionResult Index()
        {
            var tipoPermiso = new TipoPermiso();
            ViewBag.TipoPermisos = tipoPermiso.List();
            return View();
        }
        [HttpPost]
        public JsonResult listPermisoEmpleadoCantidad([FromBody] Permiso permiso)
        {            
            var lista = permiso.ListPermisoEmpleadoCantidad(permiso.FechaHoraInicioPermiso, permiso.FechaHoraFinPermiso);
            return Json(new { lista });
        }
        [HttpPost]
        public JsonResult listEmpleadoTodosPermisos([FromBody] Permiso permiso)
        {
            var empleado = new Empleado();
            var lista = empleado.ListTodosPermisos(permiso.FechaHoraInicioPermiso, permiso.FechaHoraFinPermiso);
            return Json(new { lista });
        }

    }
}
