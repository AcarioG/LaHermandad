using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
//using System.Web.UI.WebControls;
using System.Net;
using System.Web;
using System.Web.Mvc;
using La_Hermandad.Models;
using System.IO;


namespace La_Hermandad.Controllers
{
    public class ComicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comics
        public ActionResult Index()
        {
            return View(db.Comics.ToList());
        }

        public ActionResult convertirImagen(string IdComics)
        {
            var imagenPortada = db.Comics.Where(x => x.IdComics.ToString() == IdComics).FirstOrDefault();
            return File(imagenPortada.Portada, "imagen/jpeg");

        }

        public ActionResult PaginasComics(string IdPagina)
        {
            var PaginasC = db.Paginas.Where(x => x.IdPaginaC.ToString() == IdPagina).FirstOrDefault();
            return File(PaginasC.Paginas, "imagen/jpeg");

        }

        // GET: Comics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comics comics = db.Comics.Find(id);
            if (comics == null)
            {
                return HttpNotFound();
            }
            return View(comics);
        }

        // GET: Comics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titulo,FechadeEstreno")] Comics comics,PaginasComics paginas, HttpPostedFileBase imagenPortada, IEnumerable<HttpPostedFileBase> Paginas)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    if (imagenPortada != null && imagenPortada.ContentLength > 0)
                    {
                        byte[] imagenData = null;
                        using (var binaryReader = new BinaryReader(imagenPortada.InputStream))
                        {
                            imagenData = binaryReader.ReadBytes(imagenPortada.ContentLength);
                        }

                        comics.Portada = imagenData;
                    }
              
                 
                        if (Paginas != null)
                        {
                            var list = new List<PaginasComics>();
                            foreach (var pagina in Paginas)
                            {
                                using (var binaryReader = new BinaryReader(pagina.InputStream))
                                {
                                var data = binaryReader.ReadBytes(pagina.ContentLength);
                                var img = new PaginasComics { Id_Comic = comics.IdComics};
                                img.Paginas = data;
                                list.Add(img);
                                }

                            }
                        comics.Pages = list;
                        }
                  
                    //db.Paginas.AddRange(Paginas);
                    //db.SaveChanges();
                }
                catch
                {


                }

                db.Comics.Add(comics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(comics);
        }

        // GET: Comics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comics comics = db.Comics.Find(id);
            if (comics == null)
            {
                return HttpNotFound();
            }
            return View(comics);
        }

        // POST: Comics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdComics,Titulo,FechadeEstreno")] Comics comics, HttpPostedFileBase imagenPortada)
        {
            if (imagenPortada != null && imagenPortada.ContentLength > 0)
            {
                byte[] imagenData = null;
                using (var binaryReader = new BinaryReader(imagenPortada.InputStream))
                {
                    imagenData = binaryReader.ReadBytes(imagenPortada.ContentLength);
                }

                comics.Portada = imagenData;
            }

            if (ModelState.IsValid)
            {
                db.Entry(comics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comics);
        }

        // GET: Comics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comics comics = db.Comics.Find(id);
            if (comics == null)
            {
                return HttpNotFound();
            }
            return View(comics);
        }

        // POST: Comics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comics comics = db.Comics.Find(id);
            db.Comics.Remove(comics);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
