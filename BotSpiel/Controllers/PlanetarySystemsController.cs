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

    public class PlanetarySystemsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPlanetarySystemsService _planetarysystemsService;

        public PlanetarySystemsController(IPlanetarySystemsService planetarysystemsService )
        {
            _planetarysystemsService = planetarysystemsService;
        }

        // GET: PlanetarySystems
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var planetarysystems = _planetarysystemsService.Index();
            return View(planetarysystems.ToList());
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
            var planetarysystems = _planetarysystemsService.Index();
            return PartialView("IndexGrid", planetarysystems.ToList());
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
                IGrid<PlanetarySystems> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PlanetarySystems> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPlanetarySystems.xlsx");
            }
        }

        private IGrid<PlanetarySystems> CreateExportableGrid()
        {
            IGrid<PlanetarySystems> grid = new Grid<PlanetarySystems>(_planetarysystemsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPlanetarySystem).Titled("Planetary System").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Galaxies.sGalaxy).Titled("Galaxy").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PlanetarySystems>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PlanetarySystems/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_planetarysystemsService.Get(id));
        }

        // GET: PlanetarySystems/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixGalaxy = new SelectList(_planetarysystemsService.selectGalaxies().Select( x => new { x.ixGalaxy, x.sGalaxy }), "ixGalaxy", "sGalaxy");

            return View();
        }

        // POST: PlanetarySystems/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPlanetarySystem,sPlanetarySystem,ixGalaxy")] PlanetarySystemsPost planetarysystems)
        {
            if (ModelState.IsValid)
            {
                planetarysystems.UserName = User.Identity.Name;
                _planetarysystemsService.Create(planetarysystems);
                return RedirectToAction("Index");
            }
			ViewBag.ixGalaxy = new SelectList(_planetarysystemsService.selectGalaxies().Select( x => new { x.ixGalaxy, x.sGalaxy }), "ixGalaxy", "sGalaxy");

            return View(planetarysystems);
        }

        // GET: PlanetarySystems/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PlanetarySystemsPost planetarysystems = _planetarysystemsService.GetPost(id);
            if (planetarysystems == null)
            {
                return NotFound();
            }
			ViewBag.ixGalaxy = new SelectList(_planetarysystemsService.selectGalaxies().Select( x => new { x.ixGalaxy, x.sGalaxy }), "ixGalaxy", "sGalaxy", planetarysystems.ixGalaxy);

            return View(planetarysystems);
        }

        // POST: PlanetarySystems/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPlanetarySystem,sPlanetarySystem,ixGalaxy")] PlanetarySystemsPost planetarysystems)
        {
            if (ModelState.IsValid)
            {
                planetarysystems.UserName = User.Identity.Name;
                _planetarysystemsService.Edit(planetarysystems);
                return RedirectToAction("Index");
            }
			ViewBag.ixGalaxy = new SelectList(_planetarysystemsService.selectGalaxies().Select( x => new { x.ixGalaxy, x.sGalaxy }), "ixGalaxy", "sGalaxy", planetarysystems.ixGalaxy);

            return View(planetarysystems);
        }


        // GET: PlanetarySystems/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_planetarysystemsService.Get(id));
        }

        // POST: PlanetarySystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PlanetarySystemsPost planetarysystems = _planetarysystemsService.GetPost(id);
            planetarysystems.UserName = User.Identity.Name;
            _planetarysystemsService.Delete(planetarysystems);
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
            string sPlanetarySystem;

            PlanetarySystemsPost planetarysystems;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPlanetarySystem = _planetarysystemsService.Get(nID).sPlanetarySystem;
                            if (!_planetarysystemsService.VerifyPlanetarySystemDeleteOK(nID, sPlanetarySystem).Any())
                            {
                                planetarysystems = _planetarysystemsService.GetPost(nID);
                                planetarysystems.UserName = User.Identity.Name;
                                _planetarysystemsService.Delete(planetarysystems);
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
        public IActionResult VerifyPlanetarySystem(long ixPlanetarySystem, string sPlanetarySystem)
        {
            string validationResponse = "";

            if (!_planetarysystemsService.VerifyPlanetarySystemUnique(ixPlanetarySystem, sPlanetarySystem))
            {
                validationResponse = $"PlanetarySystem {sPlanetarySystem} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

