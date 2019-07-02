using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BotSpiel.DataAccess.Models;
using BotSpiel.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using NonFactors.Mvc.Grid;


namespace BotSpiel
{

    public class GalaxiesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IGalaxiesService _galaxiesService;

        public GalaxiesController(IGalaxiesService galaxiesService )
        {
            _galaxiesService = galaxiesService;
        }

        // GET: Galaxies
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var galaxies = _galaxiesService.Index();
            return View(galaxies.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult IndexExport()
        {
            return View(CreateExportableGrid());
        }

        [Authorize]
        [HttpGet]
        public PartialViewResult IndexGrid()
        {
            var galaxies = _galaxiesService.Index();
            return PartialView("IndexGrid", galaxies.ToList());
        }

        [Authorize]
        [HttpGet]
        public FileContentResult ExportIndex()
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                Int32 row = 2;
                Int32 col = 1;

                package.Workbook.Worksheets.Add("Data");
                IGrid<Galaxies> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Galaxies> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportGalaxies.xlsx");
            }
        }

        private IGrid<Galaxies> CreateExportableGrid()
        {
            IGrid<Galaxies> grid = new Grid<Galaxies>(_galaxiesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sGalaxy).Titled("Galaxy").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Universes.sUniverse).Titled("Universe").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Galaxies>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Galaxies/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_galaxiesService.Get(id));
        }

        // GET: Galaxies/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixUniverse = new SelectList(_galaxiesService.selectUniverses().Select( x => new { x.ixUniverse, x.sUniverse }), "ixUniverse", "sUniverse");

            return View();
        }

        // POST: Galaxies/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixGalaxy,sGalaxy,ixUniverse")] GalaxiesPost galaxies)
        {
            if (ModelState.IsValid)
            {
                galaxies.UserName = User.Identity.Name;
                _galaxiesService.Create(galaxies);
                return RedirectToAction("Index");
            }
			ViewBag.ixUniverse = new SelectList(_galaxiesService.selectUniverses().Select( x => new { x.ixUniverse, x.sUniverse }), "ixUniverse", "sUniverse");

            return View(galaxies);
        }

        // GET: Galaxies/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            GalaxiesPost galaxies = _galaxiesService.GetPost(id);
            if (galaxies == null)
            {
                return NotFound();
            }
			ViewBag.ixUniverse = new SelectList(_galaxiesService.selectUniverses().Select( x => new { x.ixUniverse, x.sUniverse }), "ixUniverse", "sUniverse", galaxies.ixUniverse);

            return View(galaxies);
        }

        // POST: Galaxies/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixGalaxy,sGalaxy,ixUniverse")] GalaxiesPost galaxies)
        {
            if (ModelState.IsValid)
            {
                galaxies.UserName = User.Identity.Name;
                _galaxiesService.Edit(galaxies);
                return RedirectToAction("Index");
            }
			ViewBag.ixUniverse = new SelectList(_galaxiesService.selectUniverses().Select( x => new { x.ixUniverse, x.sUniverse }), "ixUniverse", "sUniverse", galaxies.ixUniverse);

            return View(galaxies);
        }


        // GET: Galaxies/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_galaxiesService.Get(id));
        }

        // POST: Galaxies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            GalaxiesPost galaxies = _galaxiesService.GetPost(id);
            galaxies.UserName = User.Identity.Name;
            _galaxiesService.Delete(galaxies);
            return RedirectToAction("Index");
        } 

        [AcceptVerbs("Get", "Post")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult MultiRowDelete(string Ids)
        {
            string validationResponse = "";
            string[] sIDs = Ids.Split("|");
            List<long> iDs = new List<long>();
            long nID;
            string sGalaxy;

            GalaxiesPost galaxies;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sGalaxy = _galaxiesService.Get(nID).sGalaxy;
                            if (!_galaxiesService.VerifyGalaxyDeleteOK(nID, sGalaxy).Any())
                            {
                                galaxies = _galaxiesService.GetPost(nID);
                                galaxies.UserName = User.Identity.Name;
                                _galaxiesService.Delete(galaxies);
                                iDs.Add(nID);
                            }
                        }
                    }
                );

            iDs.ForEach(n => validationResponse = validationResponse + ", " + n.ToString());

            return Json(validationResponse);
        }

        [AcceptVerbs("Get","Post")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult VerifyGalaxy(long ixGalaxy, string sGalaxy)
        {
            string validationResponse = "";

            if (!_galaxiesService.VerifyGalaxyUnique(ixGalaxy, sGalaxy))
            {
                validationResponse = $"Galaxy {sGalaxy} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

