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

    public class CurrencyTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICurrencyTypesService _currencytypesService;

        public CurrencyTypesController(ICurrencyTypesService currencytypesService )
        {
            _currencytypesService = currencytypesService;
        }

        // GET: CurrencyTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var currencytypes = _currencytypesService.Index();
            return View(currencytypes.ToList());
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
            var currencytypes = _currencytypesService.Index();
            return PartialView("IndexGrid", currencytypes.ToList());
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
                IGrid<CurrencyTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<CurrencyTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCurrencyTypes.xlsx");
            }
        }

        private IGrid<CurrencyTypes> CreateExportableGrid()
        {
            IGrid<CurrencyTypes> grid = new Grid<CurrencyTypes>(_currencytypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCurrencyType).Titled("Currency Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCurrencyTypeCode).Titled("Currency Type Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<CurrencyTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: CurrencyTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_currencytypesService.Get(id));
        }

        // GET: CurrencyTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: CurrencyTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCurrencyType,sCurrencyType,sCurrencyTypeCode")] CurrencyTypesPost currencytypes)
        {
            if (ModelState.IsValid)
            {
                currencytypes.UserName = User.Identity.Name;
                _currencytypesService.Create(currencytypes);
                return RedirectToAction("Index");
            }

            return View(currencytypes);
        }

        // GET: CurrencyTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CurrencyTypesPost currencytypes = _currencytypesService.GetPost(id);
            if (currencytypes == null)
            {
                return NotFound();
            }

            return View(currencytypes);
        }

        // POST: CurrencyTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCurrencyType,sCurrencyType,sCurrencyTypeCode")] CurrencyTypesPost currencytypes)
        {
            if (ModelState.IsValid)
            {
                currencytypes.UserName = User.Identity.Name;
                _currencytypesService.Edit(currencytypes);
                return RedirectToAction("Index");
            }

            return View(currencytypes);
        }


        // GET: CurrencyTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_currencytypesService.Get(id));
        }

        // POST: CurrencyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CurrencyTypesPost currencytypes = _currencytypesService.GetPost(id);
            currencytypes.UserName = User.Identity.Name;
            _currencytypesService.Delete(currencytypes);
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
            string sCurrencyType;

            CurrencyTypesPost currencytypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCurrencyType = _currencytypesService.Get(nID).sCurrencyType;
                            if (!_currencytypesService.VerifyCurrencyTypeDeleteOK(nID, sCurrencyType).Any())
                            {
                                currencytypes = _currencytypesService.GetPost(nID);
                                currencytypes.UserName = User.Identity.Name;
                                _currencytypesService.Delete(currencytypes);
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
        public IActionResult VerifyCurrencyType(long ixCurrencyType, string sCurrencyType)
        {
            string validationResponse = "";

            if (!_currencytypesService.VerifyCurrencyTypeUnique(ixCurrencyType, sCurrencyType))
            {
                validationResponse = $"CurrencyType {sCurrencyType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

