using Microsoft.AspNetCore.Mvc;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult list()
        {
            var empleado = new Empleado();
            var lista = empleado.List();
            return Json(new { lista });
        }        
        [HttpPost]
        public JsonResult save([FromBody] Empleado empleado)
        {
            var id = empleado.Save(empleado);
            return Json(new { id });
        }
        [HttpPut]
        public JsonResult update([FromBody] Empleado empleado)
        {
            var id = empleado.Update(empleado);
            return Json(new { id });
        }
        [HttpDelete]
        public JsonResult delete([FromBody] Empleado empleado)
        {
            var id = empleado.Delete(empleado);
            return Json(new { id });
        }
    }
}
