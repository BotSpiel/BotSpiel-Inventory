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

    public class StatusesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IStatusesService _statusesService;

        public StatusesController(IStatusesService statusesService )
        {
            _statusesService = statusesService;
        }

        // GET: Statuses
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var statuses = _statusesService.Index();
            return View(statuses.ToList());
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
            var statuses = _statusesService.Index();
            return PartialView("IndexGrid", statuses.ToList());
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
                IGrid<Statuses> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Statuses> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportStatuses.xlsx");
            }
        }

        private IGrid<Statuses> CreateExportableGrid()
        {
            IGrid<Statuses> grid = new Grid<Statuses>(_statusesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sStatusCode).Titled("Status Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Statuses>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Statuses/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_statusesService.Get(id));
        }

        // GET: Statuses/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Statuses/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixStatus,sStatus,sStatusCode")] StatusesPost statuses)
        {
            if (ModelState.IsValid)
            {
                statuses.UserName = User.Identity.Name;
                _statusesService.Create(statuses);
                return RedirectToAction("Index");
            }

            return View(statuses);
        }

        // GET: Statuses/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            StatusesPost statuses = _statusesService.GetPost(id);
            if (statuses == null)
            {
                return NotFound();
            }

            return View(statuses);
        }

        // POST: Statuses/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixStatus,sStatus,sStatusCode")] StatusesPost statuses)
        {
            if (ModelState.IsValid)
            {
                statuses.UserName = User.Identity.Name;
                _statusesService.Edit(statuses);
                return RedirectToAction("Index");
            }

            return View(statuses);
        }


        // GET: Statuses/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_statusesService.Get(id));
        }

        // POST: Statuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            StatusesPost statuses = _statusesService.GetPost(id);
            statuses.UserName = User.Identity.Name;
            _statusesService.Delete(statuses);
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
            string sStatus;

            StatusesPost statuses;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sStatus = _statusesService.Get(nID).sStatus;
                            if (!_statusesService.VerifyStatusDeleteOK(nID, sStatus).Any())
                            {
                                statuses = _statusesService.GetPost(nID);
                                statuses.UserName = User.Identity.Name;
                                _statusesService.Delete(statuses);
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
        public IActionResult VerifyStatus(long ixStatus, string sStatus)
        {
            string validationResponse = "";

            if (!_statusesService.VerifyStatusUnique(ixStatus, sStatus))
            {
                validationResponse = $"Status {sStatus} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

