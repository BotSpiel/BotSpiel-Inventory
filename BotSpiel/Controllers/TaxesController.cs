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

    public class TaxesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ITaxesService _taxesService;

        public TaxesController(ITaxesService taxesService )
        {
            _taxesService = taxesService;
        }

        // GET: Taxes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var taxes = _taxesService.Index();
            return View(taxes.ToList());
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
            var taxes = _taxesService.Index();
            return PartialView("IndexGrid", taxes.ToList());
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
                IGrid<Taxes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Taxes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportTaxes.xlsx");
            }
        }

        private IGrid<Taxes> CreateExportableGrid()
        {
            IGrid<Taxes> grid = new Grid<Taxes>(_taxesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sTax).Titled("Tax").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Countries.sCountry).Titled("Country").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.CountrySubDivisions.sCountrySubDivision).Titled("Country Sub Division").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nRate).Titled("Rate").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Taxes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Taxes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_taxesService.Get(id));
        }

        // GET: Taxes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixCountry = new SelectList(_taxesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry");
			ViewBag.ixCountrySubDivision = new SelectList(_taxesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision");

            return View();
        }

        // POST: Taxes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixTax,sTax,ixCountry,ixCountrySubDivision,nRate")] TaxesPost taxes)
        {
            if (ModelState.IsValid)
            {
                taxes.UserName = User.Identity.Name;
                _taxesService.Create(taxes);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountry = new SelectList(_taxesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry");
			ViewBag.ixCountrySubDivision = new SelectList(_taxesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision");

            return View(taxes);
        }

        // GET: Taxes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            TaxesPost taxes = _taxesService.GetPost(id);
            if (taxes == null)
            {
                return NotFound();
            }
			ViewBag.ixCountry = new SelectList(_taxesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry", taxes.ixCountry);
			ViewBag.ixCountrySubDivision = new SelectList(_taxesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision", taxes.ixCountrySubDivision);

            return View(taxes);
        }

        // POST: Taxes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixTax,sTax,ixCountry,ixCountrySubDivision,nRate")] TaxesPost taxes)
        {
            if (ModelState.IsValid)
            {
                taxes.UserName = User.Identity.Name;
                _taxesService.Edit(taxes);
                return RedirectToAction("Index");
            }
			ViewBag.ixCountry = new SelectList(_taxesService.selectCountries().Select( x => new { x.ixCountry, x.sCountry }), "ixCountry", "sCountry", taxes.ixCountry);
			ViewBag.ixCountrySubDivision = new SelectList(_taxesService.selectCountrySubDivisions().Select( x => new { x.ixCountrySubDivision, x.sCountrySubDivision }), "ixCountrySubDivision", "sCountrySubDivision", taxes.ixCountrySubDivision);

            return View(taxes);
        }


        // GET: Taxes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_taxesService.Get(id));
        }

        // POST: Taxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TaxesPost taxes = _taxesService.GetPost(id);
            taxes.UserName = User.Identity.Name;
            _taxesService.Delete(taxes);
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
            string sTax;

            TaxesPost taxes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sTax = _taxesService.Get(nID).sTax;
                            if (!_taxesService.VerifyTaxDeleteOK(nID, sTax).Any())
                            {
                                taxes = _taxesService.GetPost(nID);
                                taxes.UserName = User.Identity.Name;
                                _taxesService.Delete(taxes);
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
        public IActionResult VerifyTax(long ixTax, string sTax)
        {
            string validationResponse = "";

            if (!_taxesService.VerifyTaxUnique(ixTax, sTax))
            {
                validationResponse = $"Tax {sTax} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

