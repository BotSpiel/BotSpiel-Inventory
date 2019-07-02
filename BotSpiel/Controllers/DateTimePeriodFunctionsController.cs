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

    public class DateTimePeriodFunctionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IDateTimePeriodFunctionsService _datetimeperiodfunctionsService;

        public DateTimePeriodFunctionsController(IDateTimePeriodFunctionsService datetimeperiodfunctionsService )
        {
            _datetimeperiodfunctionsService = datetimeperiodfunctionsService;
        }

        // GET: DateTimePeriodFunctions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var datetimeperiodfunctions = _datetimeperiodfunctionsService.Index();
            return View(datetimeperiodfunctions.ToList());
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
            var datetimeperiodfunctions = _datetimeperiodfunctionsService.Index();
            return PartialView("IndexGrid", datetimeperiodfunctions.ToList());
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
                IGrid<DateTimePeriodFunctions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<DateTimePeriodFunctions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportDateTimePeriodFunctions.xlsx");
            }
        }

        private IGrid<DateTimePeriodFunctions> CreateExportableGrid()
        {
            IGrid<DateTimePeriodFunctions> grid = new Grid<DateTimePeriodFunctions>(_datetimeperiodfunctionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sDateTimePeriodFunction).Titled("Date Time Period Function").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDateTimePeriodFunctionCode).Titled("Date Time Period Function Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<DateTimePeriodFunctions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: DateTimePeriodFunctions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_datetimeperiodfunctionsService.Get(id));
        }

        // GET: DateTimePeriodFunctions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: DateTimePeriodFunctions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixDateTimePeriodFunction,sDateTimePeriodFunction,sDateTimePeriodFunctionCode")] DateTimePeriodFunctionsPost datetimeperiodfunctions)
        {
            if (ModelState.IsValid)
            {
                datetimeperiodfunctions.UserName = User.Identity.Name;
                _datetimeperiodfunctionsService.Create(datetimeperiodfunctions);
                return RedirectToAction("Index");
            }

            return View(datetimeperiodfunctions);
        }

        // GET: DateTimePeriodFunctions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            DateTimePeriodFunctionsPost datetimeperiodfunctions = _datetimeperiodfunctionsService.GetPost(id);
            if (datetimeperiodfunctions == null)
            {
                return NotFound();
            }

            return View(datetimeperiodfunctions);
        }

        // POST: DateTimePeriodFunctions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixDateTimePeriodFunction,sDateTimePeriodFunction,sDateTimePeriodFunctionCode")] DateTimePeriodFunctionsPost datetimeperiodfunctions)
        {
            if (ModelState.IsValid)
            {
                datetimeperiodfunctions.UserName = User.Identity.Name;
                _datetimeperiodfunctionsService.Edit(datetimeperiodfunctions);
                return RedirectToAction("Index");
            }

            return View(datetimeperiodfunctions);
        }


        // GET: DateTimePeriodFunctions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_datetimeperiodfunctionsService.Get(id));
        }

        // POST: DateTimePeriodFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DateTimePeriodFunctionsPost datetimeperiodfunctions = _datetimeperiodfunctionsService.GetPost(id);
            datetimeperiodfunctions.UserName = User.Identity.Name;
            _datetimeperiodfunctionsService.Delete(datetimeperiodfunctions);
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
            string sDateTimePeriodFunction;

            DateTimePeriodFunctionsPost datetimeperiodfunctions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sDateTimePeriodFunction = _datetimeperiodfunctionsService.Get(nID).sDateTimePeriodFunction;
                            if (!_datetimeperiodfunctionsService.VerifyDateTimePeriodFunctionDeleteOK(nID, sDateTimePeriodFunction).Any())
                            {
                                datetimeperiodfunctions = _datetimeperiodfunctionsService.GetPost(nID);
                                datetimeperiodfunctions.UserName = User.Identity.Name;
                                _datetimeperiodfunctionsService.Delete(datetimeperiodfunctions);
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
        public IActionResult VerifyDateTimePeriodFunction(long ixDateTimePeriodFunction, string sDateTimePeriodFunction)
        {
            string validationResponse = "";

            if (!_datetimeperiodfunctionsService.VerifyDateTimePeriodFunctionUnique(ixDateTimePeriodFunction, sDateTimePeriodFunction))
            {
                validationResponse = $"DateTimePeriodFunction {sDateTimePeriodFunction} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

