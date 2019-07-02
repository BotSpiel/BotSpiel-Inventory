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

    public class PlanetRegionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPlanetRegionsService _planetregionsService;

        public PlanetRegionsController(IPlanetRegionsService planetregionsService )
        {
            _planetregionsService = planetregionsService;
        }

        // GET: PlanetRegions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var planetregions = _planetregionsService.Index();
            return View(planetregions.ToList());
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
            var planetregions = _planetregionsService.Index();
            return PartialView("IndexGrid", planetregions.ToList());
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
                IGrid<PlanetRegions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PlanetRegions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPlanetRegions.xlsx");
            }
        }

        private IGrid<PlanetRegions> CreateExportableGrid()
        {
            IGrid<PlanetRegions> grid = new Grid<PlanetRegions>(_planetregionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPlanetRegion).Titled("Planet Region").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Planets.sPlanet).Titled("Planet").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PlanetRegions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PlanetRegions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_planetregionsService.Get(id));
        }

        // GET: PlanetRegions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPlanet = new SelectList(_planetregionsService.selectPlanets().Select( x => new { x.ixPlanet, x.sPlanet }), "ixPlanet", "sPlanet");

            return View();
        }

        // POST: PlanetRegions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPlanetRegion,sPlanetRegion,ixPlanet")] PlanetRegionsPost planetregions)
        {
            if (ModelState.IsValid)
            {
                planetregions.UserName = User.Identity.Name;
                _planetregionsService.Create(planetregions);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanet = new SelectList(_planetregionsService.selectPlanets().Select( x => new { x.ixPlanet, x.sPlanet }), "ixPlanet", "sPlanet");

            return View(planetregions);
        }

        // GET: PlanetRegions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PlanetRegionsPost planetregions = _planetregionsService.GetPost(id);
            if (planetregions == null)
            {
                return NotFound();
            }
			ViewBag.ixPlanet = new SelectList(_planetregionsService.selectPlanets().Select( x => new { x.ixPlanet, x.sPlanet }), "ixPlanet", "sPlanet", planetregions.ixPlanet);

            return View(planetregions);
        }

        // POST: PlanetRegions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPlanetRegion,sPlanetRegion,ixPlanet")] PlanetRegionsPost planetregions)
        {
            if (ModelState.IsValid)
            {
                planetregions.UserName = User.Identity.Name;
                _planetregionsService.Edit(planetregions);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanet = new SelectList(_planetregionsService.selectPlanets().Select( x => new { x.ixPlanet, x.sPlanet }), "ixPlanet", "sPlanet", planetregions.ixPlanet);

            return View(planetregions);
        }


        // GET: PlanetRegions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_planetregionsService.Get(id));
        }

        // POST: PlanetRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PlanetRegionsPost planetregions = _planetregionsService.GetPost(id);
            planetregions.UserName = User.Identity.Name;
            _planetregionsService.Delete(planetregions);
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
            string sPlanetRegion;

            PlanetRegionsPost planetregions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPlanetRegion = _planetregionsService.Get(nID).sPlanetRegion;
                            if (!_planetregionsService.VerifyPlanetRegionDeleteOK(nID, sPlanetRegion).Any())
                            {
                                planetregions = _planetregionsService.GetPost(nID);
                                planetregions.UserName = User.Identity.Name;
                                _planetregionsService.Delete(planetregions);
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
        public IActionResult VerifyPlanetRegion(long ixPlanetRegion, string sPlanetRegion)
        {
            string validationResponse = "";

            if (!_planetregionsService.VerifyPlanetRegionUnique(ixPlanetRegion, sPlanetRegion))
            {
                validationResponse = $"PlanetRegion {sPlanetRegion} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

