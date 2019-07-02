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

    public class PlanetSubRegionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPlanetSubRegionsService _planetsubregionsService;

        public PlanetSubRegionsController(IPlanetSubRegionsService planetsubregionsService )
        {
            _planetsubregionsService = planetsubregionsService;
        }

        // GET: PlanetSubRegions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var planetsubregions = _planetsubregionsService.Index();
            return View(planetsubregions.ToList());
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
            var planetsubregions = _planetsubregionsService.Index();
            return PartialView("IndexGrid", planetsubregions.ToList());
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
                IGrid<PlanetSubRegions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PlanetSubRegions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPlanetSubRegions.xlsx");
            }
        }

        private IGrid<PlanetSubRegions> CreateExportableGrid()
        {
            IGrid<PlanetSubRegions> grid = new Grid<PlanetSubRegions>(_planetsubregionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPlanetSubRegion).Titled("Planet Sub Region").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.PlanetRegions.sPlanetRegion).Titled("Planet Region").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PlanetSubRegions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PlanetSubRegions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_planetsubregionsService.Get(id));
        }

        // GET: PlanetSubRegions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPlanetRegion = new SelectList(_planetsubregionsService.selectPlanetRegions().Select( x => new { x.ixPlanetRegion, x.sPlanetRegion }), "ixPlanetRegion", "sPlanetRegion");

            return View();
        }

        // POST: PlanetSubRegions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPlanetSubRegion,sPlanetSubRegion,ixPlanetRegion")] PlanetSubRegionsPost planetsubregions)
        {
            if (ModelState.IsValid)
            {
                planetsubregions.UserName = User.Identity.Name;
                _planetsubregionsService.Create(planetsubregions);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanetRegion = new SelectList(_planetsubregionsService.selectPlanetRegions().Select( x => new { x.ixPlanetRegion, x.sPlanetRegion }), "ixPlanetRegion", "sPlanetRegion");

            return View(planetsubregions);
        }

        // GET: PlanetSubRegions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PlanetSubRegionsPost planetsubregions = _planetsubregionsService.GetPost(id);
            if (planetsubregions == null)
            {
                return NotFound();
            }
			ViewBag.ixPlanetRegion = new SelectList(_planetsubregionsService.selectPlanetRegions().Select( x => new { x.ixPlanetRegion, x.sPlanetRegion }), "ixPlanetRegion", "sPlanetRegion", planetsubregions.ixPlanetRegion);

            return View(planetsubregions);
        }

        // POST: PlanetSubRegions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPlanetSubRegion,sPlanetSubRegion,ixPlanetRegion")] PlanetSubRegionsPost planetsubregions)
        {
            if (ModelState.IsValid)
            {
                planetsubregions.UserName = User.Identity.Name;
                _planetsubregionsService.Edit(planetsubregions);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanetRegion = new SelectList(_planetsubregionsService.selectPlanetRegions().Select( x => new { x.ixPlanetRegion, x.sPlanetRegion }), "ixPlanetRegion", "sPlanetRegion", planetsubregions.ixPlanetRegion);

            return View(planetsubregions);
        }


        // GET: PlanetSubRegions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_planetsubregionsService.Get(id));
        }

        // POST: PlanetSubRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PlanetSubRegionsPost planetsubregions = _planetsubregionsService.GetPost(id);
            planetsubregions.UserName = User.Identity.Name;
            _planetsubregionsService.Delete(planetsubregions);
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
            string sPlanetSubRegion;

            PlanetSubRegionsPost planetsubregions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPlanetSubRegion = _planetsubregionsService.Get(nID).sPlanetSubRegion;
                            if (!_planetsubregionsService.VerifyPlanetSubRegionDeleteOK(nID, sPlanetSubRegion).Any())
                            {
                                planetsubregions = _planetsubregionsService.GetPost(nID);
                                planetsubregions.UserName = User.Identity.Name;
                                _planetsubregionsService.Delete(planetsubregions);
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
        public IActionResult VerifyPlanetSubRegion(long ixPlanetSubRegion, string sPlanetSubRegion)
        {
            string validationResponse = "";

            if (!_planetsubregionsService.VerifyPlanetSubRegionUnique(ixPlanetSubRegion, sPlanetSubRegion))
            {
                validationResponse = $"PlanetSubRegion {sPlanetSubRegion} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

