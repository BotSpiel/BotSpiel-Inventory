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

    public class PlanetsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPlanetsService _planetsService;

        public PlanetsController(IPlanetsService planetsService )
        {
            _planetsService = planetsService;
        }

        // GET: Planets
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var planets = _planetsService.Index();
            return View(planets.ToList());
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
            var planets = _planetsService.Index();
            return PartialView("IndexGrid", planets.ToList());
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
                IGrid<Planets> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Planets> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPlanets.xlsx");
            }
        }

        private IGrid<Planets> CreateExportableGrid()
        {
            IGrid<Planets> grid = new Grid<Planets>(_planetsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPlanet).Titled("Planet").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.PlanetarySystems.sPlanetarySystem).Titled("Planetary System").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Planets>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Planets/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_planetsService.Get(id));
        }

        // GET: Planets/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPlanetarySystem = new SelectList(_planetsService.selectPlanetarySystems().Select( x => new { x.ixPlanetarySystem, x.sPlanetarySystem }), "ixPlanetarySystem", "sPlanetarySystem");

            return View();
        }

        // POST: Planets/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPlanet,sPlanet,ixPlanetarySystem")] PlanetsPost planets)
        {
            if (ModelState.IsValid)
            {
                planets.UserName = User.Identity.Name;
                _planetsService.Create(planets);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanetarySystem = new SelectList(_planetsService.selectPlanetarySystems().Select( x => new { x.ixPlanetarySystem, x.sPlanetarySystem }), "ixPlanetarySystem", "sPlanetarySystem");

            return View(planets);
        }

        // GET: Planets/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PlanetsPost planets = _planetsService.GetPost(id);
            if (planets == null)
            {
                return NotFound();
            }
			ViewBag.ixPlanetarySystem = new SelectList(_planetsService.selectPlanetarySystems().Select( x => new { x.ixPlanetarySystem, x.sPlanetarySystem }), "ixPlanetarySystem", "sPlanetarySystem", planets.ixPlanetarySystem);

            return View(planets);
        }

        // POST: Planets/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPlanet,sPlanet,ixPlanetarySystem")] PlanetsPost planets)
        {
            if (ModelState.IsValid)
            {
                planets.UserName = User.Identity.Name;
                _planetsService.Edit(planets);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanetarySystem = new SelectList(_planetsService.selectPlanetarySystems().Select( x => new { x.ixPlanetarySystem, x.sPlanetarySystem }), "ixPlanetarySystem", "sPlanetarySystem", planets.ixPlanetarySystem);

            return View(planets);
        }


        // GET: Planets/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_planetsService.Get(id));
        }

        // POST: Planets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PlanetsPost planets = _planetsService.GetPost(id);
            planets.UserName = User.Identity.Name;
            _planetsService.Delete(planets);
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
            string sPlanet;

            PlanetsPost planets;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPlanet = _planetsService.Get(nID).sPlanet;
                            if (!_planetsService.VerifyPlanetDeleteOK(nID, sPlanet).Any())
                            {
                                planets = _planetsService.GetPost(nID);
                                planets.UserName = User.Identity.Name;
                                _planetsService.Delete(planets);
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
        public IActionResult VerifyPlanet(long ixPlanet, string sPlanet)
        {
            string validationResponse = "";

            if (!_planetsService.VerifyPlanetUnique(ixPlanet, sPlanet))
            {
                validationResponse = $"Planet {sPlanet} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

