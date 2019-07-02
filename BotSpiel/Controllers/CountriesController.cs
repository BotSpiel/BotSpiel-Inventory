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

    public class CountriesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService )
        {
            _countriesService = countriesService;
        }

        // GET: Countries
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var countries = _countriesService.Index();
            return View(countries.ToList());
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
            var countries = _countriesService.Index();
            return PartialView("IndexGrid", countries.ToList());
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
                IGrid<Countries> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Countries> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCountries.xlsx");
            }
        }

        private IGrid<Countries> CreateExportableGrid()
        {
            IGrid<Countries> grid = new Grid<Countries>(_countriesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCountry).Titled("Country").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.PlanetSubRegions.sPlanetSubRegion).Titled("Planet Sub Region").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCountryCode).Titled("Country Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Countries>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Countries/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_countriesService.Get(id));
        }

        // GET: Countries/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPlanetSubRegion = new SelectList(_countriesService.selectPlanetSubRegions().Select( x => new { x.ixPlanetSubRegion, x.sPlanetSubRegion }), "ixPlanetSubRegion", "sPlanetSubRegion");

            return View();
        }

        // POST: Countries/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCountry,sCountry,ixPlanetSubRegion,sCountryCode")] CountriesPost countries)
        {
            if (ModelState.IsValid)
            {
                countries.UserName = User.Identity.Name;
                _countriesService.Create(countries);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanetSubRegion = new SelectList(_countriesService.selectPlanetSubRegions().Select( x => new { x.ixPlanetSubRegion, x.sPlanetSubRegion }), "ixPlanetSubRegion", "sPlanetSubRegion");

            return View(countries);
        }

        // GET: Countries/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CountriesPost countries = _countriesService.GetPost(id);
            if (countries == null)
            {
                return NotFound();
            }
			ViewBag.ixPlanetSubRegion = new SelectList(_countriesService.selectPlanetSubRegions().Select( x => new { x.ixPlanetSubRegion, x.sPlanetSubRegion }), "ixPlanetSubRegion", "sPlanetSubRegion", countries.ixPlanetSubRegion);

            return View(countries);
        }

        // POST: Countries/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCountry,sCountry,ixPlanetSubRegion,sCountryCode")] CountriesPost countries)
        {
            if (ModelState.IsValid)
            {
                countries.UserName = User.Identity.Name;
                _countriesService.Edit(countries);
                return RedirectToAction("Index");
            }
			ViewBag.ixPlanetSubRegion = new SelectList(_countriesService.selectPlanetSubRegions().Select( x => new { x.ixPlanetSubRegion, x.sPlanetSubRegion }), "ixPlanetSubRegion", "sPlanetSubRegion", countries.ixPlanetSubRegion);

            return View(countries);
        }


        // GET: Countries/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_countriesService.Get(id));
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CountriesPost countries = _countriesService.GetPost(id);
            countries.UserName = User.Identity.Name;
            _countriesService.Delete(countries);
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
            string sCountry;

            CountriesPost countries;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCountry = _countriesService.Get(nID).sCountry;
                            if (!_countriesService.VerifyCountryDeleteOK(nID, sCountry).Any())
                            {
                                countries = _countriesService.GetPost(nID);
                                countries.UserName = User.Identity.Name;
                                _countriesService.Delete(countries);
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
        public IActionResult VerifyCountry(long ixCountry, string sCountry)
        {
            string validationResponse = "";

            if (!_countriesService.VerifyCountryUnique(ixCountry, sCountry))
            {
                validationResponse = $"Country {sCountry} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

