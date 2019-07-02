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

    public class CountrySubDivisionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICountrySubDivisionsService _countrysubdivisionsService;

        public CountrySubDivisionsController(ICountrySubDivisionsService countrysubdivisionsService )
        {
            _countrysubdivisionsService = countrysubdivisionsService;
        }

        // GET: CountrySubDivisions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var countrysubdivisions = _countrysubdivisionsService.Index();
            return View(countrysubdivisions.ToList());
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
            var countrysubdivisions = _countrysubdivisionsService.Index();
            return PartialView("IndexGrid", countrysubdivisions.ToList());
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
                IGrid<CountrySubDivisions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<CountrySubDivisions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCountrySubDivisions.xlsx");
            }
        }

        private IGrid<CountrySubDivisions> CreateExportableGrid()
        {
            IGrid<CountrySubDivisions> grid = new Grid<CountrySubDivisions>(_countrysubdivisionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCountrySubDivision).Titled("Country Sub Division").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Countries.sCountry).Titled("Country").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCountrySubDivisionCode).Titled("Country Sub Division Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<CountrySubDivisions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: CountrySubDivisions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_countrysubdivisionsService.Get(id));
        }

        // GET: CountrySubDivisions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCountry = new SelectList(_countrysubdivisionsService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry");

            return View();
        }

        // POST: CountrySubDivisions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCountrySubDivision,sCountrySubDivision,ixCountry,sCountrySubDivisionCode")] CountrySubDivisionsPost countrysubdivisions)
        {
            if (ModelState.IsValid)
            {
                countrysubdivisions.UserName = User.Identity.Name;
                _countrysubdivisionsService.Create(countrysubdivisions);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountry = new SelectList(_countrysubdivisionsService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry");

            return View(countrysubdivisions);
        }

        // GET: CountrySubDivisions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CountrySubDivisionsPost countrysubdivisions = _countrysubdivisionsService.GetPost(id);
            if (countrysubdivisions == null)
            {
                return NotFound();
            }
			ViewBag.ixCountry = new SelectList(_countrysubdivisionsService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry", countrysubdivisions.ixCountry);

            return View(countrysubdivisions);
        }

        // POST: CountrySubDivisions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCountrySubDivision,sCountrySubDivision,ixCountry,sCountrySubDivisionCode")] CountrySubDivisionsPost countrysubdivisions)
        {
            if (ModelState.IsValid)
            {
                countrysubdivisions.UserName = User.Identity.Name;
                _countrysubdivisionsService.Edit(countrysubdivisions);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountry = new SelectList(_countrysubdivisionsService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry", countrysubdivisions.ixCountry);

            return View(countrysubdivisions);
        }


        // GET: CountrySubDivisions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_countrysubdivisionsService.Get(id));
        }

        // POST: CountrySubDivisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CountrySubDivisionsPost countrysubdivisions = _countrysubdivisionsService.GetPost(id);
            countrysubdivisions.UserName = User.Identity.Name;
            _countrysubdivisionsService.Delete(countrysubdivisions);
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
            string sCountrySubDivision;

            CountrySubDivisionsPost countrysubdivisions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCountrySubDivision = _countrysubdivisionsService.Get(nID).sCountrySubDivision;
                            if (!_countrysubdivisionsService.VerifyCountrySubDivisionDeleteOK(nID, sCountrySubDivision).Any())
                            {
                                countrysubdivisions = _countrysubdivisionsService.GetPost(nID);
                                countrysubdivisions.UserName = User.Identity.Name;
                                _countrysubdivisionsService.Delete(countrysubdivisions);
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
        public IActionResult VerifyCountrySubDivision(long ixCountrySubDivision, string sCountrySubDivision)
        {
            string validationResponse = "";

            if (!_countrysubdivisionsService.VerifyCountrySubDivisionUnique(ixCountrySubDivision, sCountrySubDivision))
            {
                validationResponse = $"CountrySubDivision {sCountrySubDivision} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

