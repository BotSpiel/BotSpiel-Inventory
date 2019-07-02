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

    public class CountryLocationsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICountryLocationsService _countrylocationsService;

        public CountryLocationsController(ICountryLocationsService countrylocationsService )
        {
            _countrylocationsService = countrylocationsService;
        }

        // GET: CountryLocations
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var countrylocations = _countrylocationsService.Index();
            return View(countrylocations.ToList());
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
            var countrylocations = _countrylocationsService.Index();
            return PartialView("IndexGrid", countrylocations.ToList());
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
                IGrid<CountryLocations> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<CountryLocations> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCountryLocations.xlsx");
            }
        }

        private IGrid<CountryLocations> CreateExportableGrid()
        {
            IGrid<CountryLocations> grid = new Grid<CountryLocations>(_countrylocationsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCountryLocation).Titled("Country Location").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.CountrySubDivisions.sCountrySubDivision).Titled("Country Sub Division").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sLocationCode).Titled("Location Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sNameWithoutDiacritics).Titled("Name Without Diacritics").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLatitude).Titled("Latitude").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLongitude).Titled("Longitude").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<CountryLocations>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: CountryLocations/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_countrylocationsService.Get(id));
        }

        // GET: CountryLocations/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCountrySubDivision = new SelectList(_countrylocationsService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision");

            return View();
        }

        // POST: CountryLocations/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCountryLocation,sCountryLocation,ixCountrySubDivision,sLocationCode,sNameWithoutDiacritics,sLatitude,sLongitude")] CountryLocationsPost countrylocations)
        {
            if (ModelState.IsValid)
            {
                countrylocations.UserName = User.Identity.Name;
                _countrylocationsService.Create(countrylocations);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountrySubDivision = new SelectList(_countrylocationsService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision");

            return View(countrylocations);
        }

        // GET: CountryLocations/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CountryLocationsPost countrylocations = _countrylocationsService.GetPost(id);
            if (countrylocations == null)
            {
                return NotFound();
            }
			ViewBag.ixCountrySubDivision = new SelectList(_countrylocationsService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision", countrylocations.ixCountrySubDivision);

            return View(countrylocations);
        }

        // POST: CountryLocations/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCountryLocation,sCountryLocation,ixCountrySubDivision,sLocationCode,sNameWithoutDiacritics,sLatitude,sLongitude")] CountryLocationsPost countrylocations)
        {
            if (ModelState.IsValid)
            {
                countrylocations.UserName = User.Identity.Name;
                _countrylocationsService.Edit(countrylocations);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountrySubDivision = new SelectList(_countrylocationsService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision", countrylocations.ixCountrySubDivision);

            return View(countrylocations);
        }


        // GET: CountryLocations/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_countrylocationsService.Get(id));
        }

        // POST: CountryLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CountryLocationsPost countrylocations = _countrylocationsService.GetPost(id);
            countrylocations.UserName = User.Identity.Name;
            _countrylocationsService.Delete(countrylocations);
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
            string sCountryLocation;

            CountryLocationsPost countrylocations;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCountryLocation = _countrylocationsService.Get(nID).sCountryLocation;
                            if (!_countrylocationsService.VerifyCountryLocationDeleteOK(nID, sCountryLocation).Any())
                            {
                                countrylocations = _countrylocationsService.GetPost(nID);
                                countrylocations.UserName = User.Identity.Name;
                                _countrylocationsService.Delete(countrylocations);
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
        public IActionResult VerifyCountryLocation(long ixCountryLocation, string sCountryLocation)
        {
            string validationResponse = "";

            if (!_countrylocationsService.VerifyCountryLocationUnique(ixCountryLocation, sCountryLocation))
            {
                validationResponse = $"CountryLocation {sCountryLocation} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

