using PGMCLIP.DataAccess;
using PGMCLIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGMCLIP.Controllers
{
    public class ConfiguracionController : Controller
    {

        [HttpGet]
        public ActionResult ListadoParametros()
        {
            List<ParametroConfiguracion> listaParametros = ParametroDA.obtenerListaParametros();
            return View(listaParametros);

        }

        [HttpGet]
        public ActionResult ActualizarParametros(int codigo_parametro)
        {
            ParametroConfiguracion parametro = ParametroDA.obtenerParametro(codigo_parametro);
            ViewBag.nombreParametro = parametro.nombre;
            return View(parametro);
        }

        [HttpPost]
        public ActionResult ActualizarParametros(ParametroConfiguracion model)
        {
          
                bool resultado = ParametroDA.modificarParametro(model);
                if (resultado)
                {
                    return RedirectToAction("ListadoParametros", "Configuracion");
                }
                else
                {
                    return View(model);
                }
          
        }

    }
}
