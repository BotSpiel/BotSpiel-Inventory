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

    public class DateTimePeriodFormatsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IDateTimePeriodFormatsService _datetimeperiodformatsService;

        public DateTimePeriodFormatsController(IDateTimePeriodFormatsService datetimeperiodformatsService )
        {
            _datetimeperiodformatsService = datetimeperiodformatsService;
        }

        // GET: DateTimePeriodFormats
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var datetimeperiodformats = _datetimeperiodformatsService.Index();
            return View(datetimeperiodformats.ToList());
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
            var datetimeperiodformats = _datetimeperiodformatsService.Index();
            return PartialView("IndexGrid", datetimeperiodformats.ToList());
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
                IGrid<DateTimePeriodFormats> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<DateTimePeriodFormats> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportDateTimePeriodFormats.xlsx");
            }
        }

        private IGrid<DateTimePeriodFormats> CreateExportableGrid()
        {
            IGrid<DateTimePeriodFormats> grid = new Grid<DateTimePeriodFormats>(_datetimeperiodformatsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sDateTimePeriodFormat).Titled("Date Time Period Format").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDateTimePeriodFormatCode).Titled("Date Time Period Format Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<DateTimePeriodFormats>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: DateTimePeriodFormats/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_datetimeperiodformatsService.Get(id));
        }

        // GET: DateTimePeriodFormats/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: DateTimePeriodFormats/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixDateTimePeriodFormat,sDateTimePeriodFormat,sDateTimePeriodFormatCode")] DateTimePeriodFormatsPost datetimeperiodformats)
        {
            if (ModelState.IsValid)
            {
                datetimeperiodformats.UserName = User.Identity.Name;
                _datetimeperiodformatsService.Create(datetimeperiodformats);
                return RedirectToAction("Index");
            }

            return View(datetimeperiodformats);
        }

        // GET: DateTimePeriodFormats/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            DateTimePeriodFormatsPost datetimeperiodformats = _datetimeperiodformatsService.GetPost(id);
            if (datetimeperiodformats == null)
            {
                return NotFound();
            }

            return View(datetimeperiodformats);
        }

        // POST: DateTimePeriodFormats/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixDateTimePeriodFormat,sDateTimePeriodFormat,sDateTimePeriodFormatCode")] DateTimePeriodFormatsPost datetimeperiodformats)
        {
            if (ModelState.IsValid)
            {
                datetimeperiodformats.UserName = User.Identity.Name;
                _datetimeperiodformatsService.Edit(datetimeperiodformats);
                return RedirectToAction("Index");
            }

            return View(datetimeperiodformats);
        }


        // GET: DateTimePeriodFormats/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_datetimeperiodformatsService.Get(id));
        }

        // POST: DateTimePeriodFormats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DateTimePeriodFormatsPost datetimeperiodformats = _datetimeperiodformatsService.GetPost(id);
            datetimeperiodformats.UserName = User.Identity.Name;
            _datetimeperiodformatsService.Delete(datetimeperiodformats);
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
            string sDateTimePeriodFormat;

            DateTimePeriodFormatsPost datetimeperiodformats;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sDateTimePeriodFormat = _datetimeperiodformatsService.Get(nID).sDateTimePeriodFormat;
                            if (!_datetimeperiodformatsService.VerifyDateTimePeriodFormatDeleteOK(nID, sDateTimePeriodFormat).Any())
                            {
                                datetimeperiodformats = _datetimeperiodformatsService.GetPost(nID);
                                datetimeperiodformats.UserName = User.Identity.Name;
                                _datetimeperiodformatsService.Delete(datetimeperiodformats);
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
        public IActionResult VerifyDateTimePeriodFormat(long ixDateTimePeriodFormat, string sDateTimePeriodFormat)
        {
            string validationResponse = "";

            if (!_datetimeperiodformatsService.VerifyDateTimePeriodFormatUnique(ixDateTimePeriodFormat, sDateTimePeriodFormat))
            {
                validationResponse = $"DateTimePeriodFormat {sDateTimePeriodFormat} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

