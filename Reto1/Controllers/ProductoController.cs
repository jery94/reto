using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reto1.Negocio;

namespace Reto1.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            ViewBag.productos = new Producto().ReadAll();
            return View();
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create([Bind(incluce ="Nombre, Descripcion, Precio")] Producto produco)
        {
            try
            {
                // TODO: Add insert logic here
                produco.Save();
                TempData["mensaje"] = Guardado correctamente
                return RedirectToAction("Index");
            }
            catch
            {
                return View(productos);
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Producto p = new Producto().Find(id);

                if(p == null) {
                    TempData["mensaje"] = "No producto no existe";
                    return RedirectionAction("Index");
                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            if(new Producto().Find(id) == null)
            {
                TempData["mensaje"] = "No existe el producto";
                return RedirectionAction("Index");
            }

            if (new Producto().Delete(id)) {
                TempData["mensaje"] = "Eliminado correctamente";
                return RedirectionAction("Index");
            }

            TempData["mensaje"] = "No se ha podido eliminar";
            return RedirectionToAction("Index");

        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
