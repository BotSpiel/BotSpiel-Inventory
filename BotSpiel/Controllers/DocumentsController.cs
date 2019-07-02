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

    public class DocumentsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IDocumentsService _documentsService;

        public DocumentsController(IDocumentsService documentsService )
        {
            _documentsService = documentsService;
        }

        // GET: Documents
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var documents = _documentsService.Index();
            return View(documents.ToList());
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
            var documents = _documentsService.Index();
            return PartialView("IndexGrid", documents.ToList());
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
                IGrid<Documents> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Documents> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportDocuments.xlsx");
            }
        }

        private IGrid<Documents> CreateExportableGrid()
        {
            IGrid<Documents> grid = new Grid<Documents>(_documentsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sDocument).Titled("Document").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.DocumentMessageTypes.sDocumentMessageType).Titled("Document Message Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sVersion).Titled("Version").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sRevision).Titled("Revision").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Documents>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Documents/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_documentsService.Get(id));
        }

        // GET: Documents/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixDocumentMessageType = new SelectList(_documentsService.selectDocumentMessageTypes().Select( x => new { x.ixDocumentMessageType, x.sDocumentMessageType }), "ixDocumentMessageType", "sDocumentMessageType");
			ViewBag.ixStatus = new SelectList(_documentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: Documents/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixDocument,sDocument,ixDocumentMessageType,sVersion,sRevision,ixStatus")] DocumentsPost documents)
        {
            if (ModelState.IsValid)
            {
                documents.UserName = User.Identity.Name;
                _documentsService.Create(documents);
                return RedirectToAction("Index");
            }
			ViewBag.ixDocumentMessageType = new SelectList(_documentsService.selectDocumentMessageTypes().Select( x => new { x.ixDocumentMessageType, x.sDocumentMessageType }), "ixDocumentMessageType", "sDocumentMessageType");
			ViewBag.ixStatus = new SelectList(_documentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(documents);
        }

        // GET: Documents/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            DocumentsPost documents = _documentsService.GetPost(id);
            if (documents == null)
            {
                return NotFound();
            }
			ViewBag.ixDocumentMessageType = new SelectList(_documentsService.selectDocumentMessageTypes().Select( x => new { x.ixDocumentMessageType, x.sDocumentMessageType }), "ixDocumentMessageType", "sDocumentMessageType", documents.ixDocumentMessageType);
			ViewBag.ixStatus = new SelectList(_documentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", documents.ixStatus);

            return View(documents);
        }

        // POST: Documents/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixDocument,sDocument,ixDocumentMessageType,sVersion,sRevision,ixStatus")] DocumentsPost documents)
        {
            if (ModelState.IsValid)
            {
                documents.UserName = User.Identity.Name;
                _documentsService.Edit(documents);
                return RedirectToAction("Index");
            }
			ViewBag.ixDocumentMessageType = new SelectList(_documentsService.selectDocumentMessageTypes().Select( x => new { x.ixDocumentMessageType, x.sDocumentMessageType }), "ixDocumentMessageType", "sDocumentMessageType", documents.ixDocumentMessageType);
			ViewBag.ixStatus = new SelectList(_documentsService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", documents.ixStatus);

            return View(documents);
        }


        // GET: Documents/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_documentsService.Get(id));
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DocumentsPost documents = _documentsService.GetPost(id);
            documents.UserName = User.Identity.Name;
            _documentsService.Delete(documents);
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
            string sDocument;

            DocumentsPost documents;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sDocument = _documentsService.Get(nID).sDocument;
                            if (!_documentsService.VerifyDocumentDeleteOK(nID, sDocument).Any())
                            {
                                documents = _documentsService.GetPost(nID);
                                documents.UserName = User.Identity.Name;
                                _documentsService.Delete(documents);
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
        public IActionResult VerifyDocument(long ixDocument, string sDocument)
        {
            string validationResponse = "";

            if (!_documentsService.VerifyDocumentUnique(ixDocument, sDocument))
            {
                validationResponse = $"Document {sDocument} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

