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

    public class MessageResponseTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMessageResponseTypesService _messageresponsetypesService;

        public MessageResponseTypesController(IMessageResponseTypesService messageresponsetypesService )
        {
            _messageresponsetypesService = messageresponsetypesService;
        }

        // GET: MessageResponseTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var messageresponsetypes = _messageresponsetypesService.Index();
            return View(messageresponsetypes.ToList());
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
            var messageresponsetypes = _messageresponsetypesService.Index();
            return PartialView("IndexGrid", messageresponsetypes.ToList());
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
                IGrid<MessageResponseTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MessageResponseTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMessageResponseTypes.xlsx");
            }
        }

        private IGrid<MessageResponseTypes> CreateExportableGrid()
        {
            IGrid<MessageResponseTypes> grid = new Grid<MessageResponseTypes>(_messageresponsetypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMessageResponseType).Titled("Message Response Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sMessageResponseTypeCode).Titled("Message Response Type Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MessageResponseTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MessageResponseTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_messageresponsetypesService.Get(id));
        }

        // GET: MessageResponseTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MessageResponseTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMessageResponseType,sMessageResponseType,sMessageResponseTypeCode")] MessageResponseTypesPost messageresponsetypes)
        {
            if (ModelState.IsValid)
            {
                messageresponsetypes.UserName = User.Identity.Name;
                _messageresponsetypesService.Create(messageresponsetypes);
                return RedirectToAction("Index");
            }

            return View(messageresponsetypes);
        }

        // GET: MessageResponseTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MessageResponseTypesPost messageresponsetypes = _messageresponsetypesService.GetPost(id);
            if (messageresponsetypes == null)
            {
                return NotFound();
            }

            return View(messageresponsetypes);
        }

        // POST: MessageResponseTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMessageResponseType,sMessageResponseType,sMessageResponseTypeCode")] MessageResponseTypesPost messageresponsetypes)
        {
            if (ModelState.IsValid)
            {
                messageresponsetypes.UserName = User.Identity.Name;
                _messageresponsetypesService.Edit(messageresponsetypes);
                return RedirectToAction("Index");
            }

            return View(messageresponsetypes);
        }


        // GET: MessageResponseTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_messageresponsetypesService.Get(id));
        }

        // POST: MessageResponseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MessageResponseTypesPost messageresponsetypes = _messageresponsetypesService.GetPost(id);
            messageresponsetypes.UserName = User.Identity.Name;
            _messageresponsetypesService.Delete(messageresponsetypes);
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
            string sMessageResponseType;

            MessageResponseTypesPost messageresponsetypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMessageResponseType = _messageresponsetypesService.Get(nID).sMessageResponseType;
                            if (!_messageresponsetypesService.VerifyMessageResponseTypeDeleteOK(nID, sMessageResponseType).Any())
                            {
                                messageresponsetypes = _messageresponsetypesService.GetPost(nID);
                                messageresponsetypes.UserName = User.Identity.Name;
                                _messageresponsetypesService.Delete(messageresponsetypes);
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
        public IActionResult VerifyMessageResponseType(long ixMessageResponseType, string sMessageResponseType)
        {
            string validationResponse = "";

            if (!_messageresponsetypesService.VerifyMessageResponseTypeUnique(ixMessageResponseType, sMessageResponseType))
            {
                validationResponse = $"MessageResponseType {sMessageResponseType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

