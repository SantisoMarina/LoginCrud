using PGMCLIP.DataAccess;
using PGMCLIP.Models;
using PGMCLIP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PGMCLIP.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult RegistroUsuario()
        {
            if (ModelState.IsValid)
            {
                List<ProvinciaItemVM> listaProvincias = UsuarioDA.obtenerListaProvincias();

                //viewbag -> manera de llevar los valores del controlador a la vista si el modelo no tiene esos datos
                ViewBag.items = listaProvincias;
                return View();
            }
            else
            {
                return View();
            }
        }

        // Post: Usuario
        [HttpPost]
        public ActionResult RegistroUsuario(Usuario user)
        {
            if (ModelState.IsValid)
            {
                bool resultado = UsuarioDA.nuevoUsuario(user);
                if (resultado)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(user);
                }

            }
            else
            {
                return View(user);
            }
        }


        // GET: InicioSesion
        [HttpGet]
        public ActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InicioSesion(string usuario, string password)
        {


            bool resultado = UsuarioDA.verificacionUsuario(usuario, password);
            if (resultado)
            {
                HttpCookie cookie = new HttpCookie("usuario", usuario);
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);


                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Response.Cookies["usuario"].Expires = DateTime.Now.AddDays(-1);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult ListadoPersonas(string orden, string ordenamiento, string searchString)
        {

            ordenamiento = ordenamiento == null ? "" : ordenamiento;
            List<PersonaVM> listaPersonas = UsuarioDA.obtenerListaPersonas();

            if (!String.IsNullOrEmpty(searchString))
            {
                listaPersonas = listaPersonas.Where(s => s.apellido.Contains(searchString)
                                       || s.nombre.Contains(searchString)).ToList();
            }

            switch (orden)
            {
                case "id":
                    if (String.IsNullOrEmpty(ordenamiento) || ordenamiento.Equals("idPersonaDes"))
                    {
                        ViewBag.ordenamiento = "idPersonaAsc";
                        listaPersonas = listaPersonas.OrderBy(s => s.id_persona).ToList();
                    }
                    else if (ordenamiento.Equals("idPersonaAsc"))
                    {
                        ViewBag.ordenamiento = "idPersonaDes";
                        listaPersonas = listaPersonas.OrderByDescending(s => s.id_persona).ToList();
                    }
                    break;
                case "nombre":
                    if (String.IsNullOrEmpty(ordenamiento) || ordenamiento.Equals("nombreDes")) {
                        ViewBag.ordenamiento = "nombreAsc";
                        listaPersonas = listaPersonas.OrderBy(s => s.nombre).ToList();
                    } else if (ordenamiento.Equals("nombreAsc")) {
                        ViewBag.ordenamiento = "nombreDes";
                        listaPersonas = listaPersonas.OrderByDescending(s => s.nombre).ToList();
                    } 
                    break;
                case "apellido":
                    if (String.IsNullOrEmpty(ordenamiento) || ordenamiento.Equals("apellidoDes"))
                    {
                        ViewBag.ordenamiento = "apellidoAsc";
                        listaPersonas = listaPersonas.OrderBy(s => s.apellido).ToList();
                    }
                    else if (ordenamiento.Equals("nombreAsc"))
                    {
                        ViewBag.ordenamiento = "apellidoDes";
                        listaPersonas = listaPersonas.OrderByDescending(s => s.apellido).ToList();
                    }
                    break;
                case "usuario":
                    if (String.IsNullOrEmpty(ordenamiento) || ordenamiento.Equals("usuarioDes"))
                    {
                        ViewBag.ordenamiento = "usuarioAsc";
                        listaPersonas = listaPersonas.OrderBy(s => s.usuario).ToList();
                    }
                    else if (ordenamiento.Equals("usuarioAsc"))
                    {
                        ViewBag.ordenamiento = "usuarioDes";
                        listaPersonas = listaPersonas.OrderByDescending(s => s.usuario).ToList();
                    }
                    break;
                case "email":
                    if (String.IsNullOrEmpty(ordenamiento) || ordenamiento.Equals("emailDes"))
                    {
                        ViewBag.ordenamiento = "emailAsc";
                        listaPersonas = listaPersonas.OrderBy(s => s.email).ToList();
                    }
                    else if (ordenamiento.Equals("emailAsc"))
                    {
                        ViewBag.ordenamiento = "emailDes";
                        listaPersonas = listaPersonas.OrderByDescending(s => s.email).ToList();
                    }
                    break;
                default:
                    listaPersonas = listaPersonas.OrderBy(s => s.id_persona).ToList();
                    break;
            }
            return View(listaPersonas);

        }

        [HttpGet]
        public ActionResult ActualizarDatos(int id_persona)
        {

            PersonaVM personaVM = UsuarioDA.obtenerPersona(id_persona);
            ViewBag.nombrePersonaVM = personaVM.nombre;
            return View(personaVM);
        }

        [HttpPost]
        public ActionResult ActualizarDatos(PersonaVM model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = UsuarioDA.modificarDatos(model);
                if (resultado)
                {
                    return RedirectToAction("ListadoPersonas", "Usuario");
                }
                else
                {
                    return View(model);
                }
            }
            return View();
        }


        [HttpGet]
        public ActionResult eliminarUsuario(PersonaVM persona)
        {
            bool resultado = UsuarioDA.eliminarUsuario(persona);
            if (resultado)
            {
                return RedirectToAction("ListadoPersonas", "Usuario");
            }
            else
            {
                return RedirectToAction("ListadoPersonas", "Usuario");
            }
        }


    }
}