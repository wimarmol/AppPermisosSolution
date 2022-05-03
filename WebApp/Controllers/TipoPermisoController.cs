using BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class TipoPermisoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Index(TipoPermiso tipoPermiso)
        //{
        //    var id = tipoPermiso.save(tipoPermiso);
        //    return View();
        //}
        [HttpPost]
        public JsonResult save([FromBody] TipoPermiso tipoPermiso) {
            var id = tipoPermiso.Save(tipoPermiso);
            return Json(new { id });
        }
        public JsonResult list() {
            var tipoPermiso = new TipoPermiso();
            var lista = tipoPermiso.List();
            return Json(new { lista });
        }
        [HttpPut]
        public JsonResult update([FromBody] TipoPermiso tipoPermiso) {
            var id = tipoPermiso.Update(tipoPermiso);
            return Json(new { id });
        }
        [HttpDelete]
        public JsonResult delete([FromBody] TipoPermiso tipoPermiso)
        {
            var id = tipoPermiso.Delete(tipoPermiso);
            return Json(new { id });
        }
    }
}
