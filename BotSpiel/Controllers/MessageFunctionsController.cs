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

    public class MessageFunctionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMessageFunctionsService _messagefunctionsService;

        public MessageFunctionsController(IMessageFunctionsService messagefunctionsService )
        {
            _messagefunctionsService = messagefunctionsService;
        }

        // GET: MessageFunctions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var messagefunctions = _messagefunctionsService.Index();
            return View(messagefunctions.ToList());
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
            var messagefunctions = _messagefunctionsService.Index();
            return PartialView("IndexGrid", messagefunctions.ToList());
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
                IGrid<MessageFunctions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MessageFunctions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMessageFunctions.xlsx");
            }
        }

        private IGrid<MessageFunctions> CreateExportableGrid()
        {
            IGrid<MessageFunctions> grid = new Grid<MessageFunctions>(_messagefunctionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMessageFunction).Titled("Message Function").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sMessageFunctionCode).Titled("Message Function Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MessageFunctions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MessageFunctions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_messagefunctionsService.Get(id));
        }

        // GET: MessageFunctions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MessageFunctions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMessageFunction,sMessageFunction,sMessageFunctionCode")] MessageFunctionsPost messagefunctions)
        {
            if (ModelState.IsValid)
            {
                messagefunctions.UserName = User.Identity.Name;
                _messagefunctionsService.Create(messagefunctions);
                return RedirectToAction("Index");
            }

            return View(messagefunctions);
        }

        // GET: MessageFunctions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MessageFunctionsPost messagefunctions = _messagefunctionsService.GetPost(id);
            if (messagefunctions == null)
            {
                return NotFound();
            }

            return View(messagefunctions);
        }

        // POST: MessageFunctions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMessageFunction,sMessageFunction,sMessageFunctionCode")] MessageFunctionsPost messagefunctions)
        {
            if (ModelState.IsValid)
            {
                messagefunctions.UserName = User.Identity.Name;
                _messagefunctionsService.Edit(messagefunctions);
                return RedirectToAction("Index");
            }

            return View(messagefunctions);
        }


        // GET: MessageFunctions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_messagefunctionsService.Get(id));
        }

        // POST: MessageFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MessageFunctionsPost messagefunctions = _messagefunctionsService.GetPost(id);
            messagefunctions.UserName = User.Identity.Name;
            _messagefunctionsService.Delete(messagefunctions);
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
            string sMessageFunction;

            MessageFunctionsPost messagefunctions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMessageFunction = _messagefunctionsService.Get(nID).sMessageFunction;
                            if (!_messagefunctionsService.VerifyMessageFunctionDeleteOK(nID, sMessageFunction).Any())
                            {
                                messagefunctions = _messagefunctionsService.GetPost(nID);
                                messagefunctions.UserName = User.Identity.Name;
                                _messagefunctionsService.Delete(messagefunctions);
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
        public IActionResult VerifyMessageFunction(long ixMessageFunction, string sMessageFunction)
        {
            string validationResponse = "";

            if (!_messagefunctionsService.VerifyMessageFunctionUnique(ixMessageFunction, sMessageFunction))
            {
                validationResponse = $"MessageFunction {sMessageFunction} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

