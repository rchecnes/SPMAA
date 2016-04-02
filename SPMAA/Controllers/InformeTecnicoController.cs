using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPMAA.Controllers
{
    public class InformeTecnicoController : Controller
    {
        //
        // GET: /InformeTecnico/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /InformeTecnico/nuevo
        public ActionResult Nuevo()
        {
            return View();
        }
        //
        // GET: /InformeTecnico/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /InformeTecnico/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /InformeTecnico/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /InformeTecnico/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /InformeTecnico/Edit/5
        [HttpPost]
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

        //
        // GET: /InformeTecnico/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /InformeTecnico/Delete/5
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
    }
}
