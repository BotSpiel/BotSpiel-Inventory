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

    public class CurrenciesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICurrenciesService _currenciesService;

        public CurrenciesController(ICurrenciesService currenciesService )
        {
            _currenciesService = currenciesService;
        }

        // GET: Currencies
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var currencies = _currenciesService.Index();
            return View(currencies.ToList());
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
            var currencies = _currenciesService.Index();
            return PartialView("IndexGrid", currencies.ToList());
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
                IGrid<Currencies> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Currencies> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCurrencies.xlsx");
            }
        }

        private IGrid<Currencies> CreateExportableGrid()
        {
            IGrid<Currencies> grid = new Grid<Currencies>(_currenciesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCurrency).Titled("Currency").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Currencies>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Currencies/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_currenciesService.Get(id));
        }

        // GET: Currencies/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Currencies/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCurrency,sCurrency")] CurrenciesPost currencies)
        {
            if (ModelState.IsValid)
            {
                currencies.UserName = User.Identity.Name;
                _currenciesService.Create(currencies);
                return RedirectToAction("Index");
            }

            return View(currencies);
        }

        // GET: Currencies/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CurrenciesPost currencies = _currenciesService.GetPost(id);
            if (currencies == null)
            {
                return NotFound();
            }

            return View(currencies);
        }

        // POST: Currencies/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCurrency,sCurrency")] CurrenciesPost currencies)
        {
            if (ModelState.IsValid)
            {
                currencies.UserName = User.Identity.Name;
                _currenciesService.Edit(currencies);
                return RedirectToAction("Index");
            }

            return View(currencies);
        }


        // GET: Currencies/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_currenciesService.Get(id));
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CurrenciesPost currencies = _currenciesService.GetPost(id);
            currencies.UserName = User.Identity.Name;
            _currenciesService.Delete(currencies);
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
            string sCurrency;

            CurrenciesPost currencies;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCurrency = _currenciesService.Get(nID).sCurrency;
                            if (!_currenciesService.VerifyCurrencyDeleteOK(nID, sCurrency).Any())
                            {
                                currencies = _currenciesService.GetPost(nID);
                                currencies.UserName = User.Identity.Name;
                                _currenciesService.Delete(currencies);
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
        public IActionResult VerifyCurrency(long ixCurrency, string sCurrency)
        {
            string validationResponse = "";

            if (!_currenciesService.VerifyCurrencyUnique(ixCurrency, sCurrency))
            {
                validationResponse = $"Currency {sCurrency} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

