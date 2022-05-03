using BLL;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class PermisoController : Controller
    {
        public IActionResult Index()
        {
            var permiso = new Permiso();
            var empleado = new Empleado();
            permiso.Empleados = empleado.List();
            return View(permiso);
        }
        public IActionResult PermisosEmpleado(int id)
        {
            var permiso = new Permiso();
            var empleado = new Empleado();
            var tipoPermiso = new TipoPermiso();
            empleado = empleado.Get(id);
            empleado.Permisos = permiso.List(id);
            ViewBag.TipoPermisos = tipoPermiso.List();
            return View(empleado);
        }
        public IActionResult Nuevo(int id)
        {   
            var empleado = new Empleado();
            var tipoPermiso = new TipoPermiso();
            empleado = empleado.Get(id);
            ViewBag.TipoPermisos = tipoPermiso.List();
            return View(empleado);
        }
        public JsonResult list()
        {
            var permiso = new Permiso();
            var lista = permiso.List();
            return Json(new { lista });
        }
        public JsonResult get([FromBody] Permiso permiso)
        {            
            permiso = permiso.Get(permiso.Id);
            return Json(new { permiso });
        }
        [HttpPost]
        public JsonResult save([FromBody] Permiso permiso)
        {
            var id = permiso.Save(permiso);
            return Json(new { id });
        }
        [HttpPut]
        public JsonResult update([FromBody] Permiso permiso)
        {
            var id = permiso.Update(permiso);
            return Json(new { id });
        }
        [HttpDelete]
        public JsonResult delete([FromBody] Permiso permiso)
        {
            var id = permiso.Delete(permiso);
            return Json(new { id });
        }
    }
}
