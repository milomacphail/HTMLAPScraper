using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HTMLAPScraper.Models;

namespace HTMLAPScraper.Controllers
{
    public class HAPStockTablesController : Controller
    {
        private HAPStockTableEntities db = new HAPStockTableEntities();

        // GET: HAPStockTables
        public async Task<ActionResult> Index()
        {
            return View(await db.HAPStockTables.ToListAsync());
        }

        // GET: HAPStockTables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HAPStockTable hAPStockTable = await db.HAPStockTables.FindAsync(id);
            if (hAPStockTable == null)
            {
                return HttpNotFound();
            }
            return View(hAPStockTable);
        }

        // GET: HAPStockTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HAPStockTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Time_Scraped,Stock_Symbol,Last_Price,Change,Change_Percent")] HAPStockTable hAPStockTable)
        {
            if (ModelState.IsValid)
            {
                db.HAPStockTables.Add(hAPStockTable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(hAPStockTable);
        }

        // GET: HAPStockTables/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HAPStockTable hAPStockTable = await db.HAPStockTables.FindAsync(id);
            if (hAPStockTable == null)
            {
                return HttpNotFound();
            }
            return View(hAPStockTable);
        }

        // POST: HAPStockTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Time_Scraped,Stock_Symbol,Last_Price,Change,Change_Percent")] HAPStockTable hAPStockTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hAPStockTable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hAPStockTable);
        }

        // GET: HAPStockTables/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HAPStockTable hAPStockTable = await db.HAPStockTables.FindAsync(id);
            if (hAPStockTable == null)
            {
                return HttpNotFound();
            }
            return View(hAPStockTable);
        }

        // POST: HAPStockTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HAPStockTable hAPStockTable = await db.HAPStockTables.FindAsync(id);
            db.HAPStockTables.Remove(hAPStockTable);
            await db.SaveChangesAsync();
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

        [Authorize]
        public ActionResult Scrape()
        {
            string nasdaq = "https://www.nasdaq.com/markets/unusual-volume.aspx";

            HAPscrape scrape = new HAPscrape();
            scrape.ScrapeStocks(nasdaq);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult DeleteTable()
        {
            string deleteQuery = "DELETE FROM dbo.HAPStockTable;" + "DBCC CHECKIDENT (HAPStockTable, RESEED, 0);";

            db.Database.ExecuteSqlCommand(deleteQuery);

            return RedirectToAction("Index");

        }
    }
}
