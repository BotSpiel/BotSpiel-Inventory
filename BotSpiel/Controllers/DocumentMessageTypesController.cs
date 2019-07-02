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

    public class DocumentMessageTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IDocumentMessageTypesService _documentmessagetypesService;

        public DocumentMessageTypesController(IDocumentMessageTypesService documentmessagetypesService )
        {
            _documentmessagetypesService = documentmessagetypesService;
        }

        // GET: DocumentMessageTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var documentmessagetypes = _documentmessagetypesService.Index();
            return View(documentmessagetypes.ToList());
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
            var documentmessagetypes = _documentmessagetypesService.Index();
            return PartialView("IndexGrid", documentmessagetypes.ToList());
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
                IGrid<DocumentMessageTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<DocumentMessageTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportDocumentMessageTypes.xlsx");
            }
        }

        private IGrid<DocumentMessageTypes> CreateExportableGrid()
        {
            IGrid<DocumentMessageTypes> grid = new Grid<DocumentMessageTypes>(_documentmessagetypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sDocumentMessageType).Titled("Document Message Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sDocumentMessageTypeCode).Titled("Document Message Type Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<DocumentMessageTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: DocumentMessageTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_documentmessagetypesService.Get(id));
        }

        // GET: DocumentMessageTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: DocumentMessageTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixDocumentMessageType,sDocumentMessageType,sDocumentMessageTypeCode")] DocumentMessageTypesPost documentmessagetypes)
        {
            if (ModelState.IsValid)
            {
                documentmessagetypes.UserName = User.Identity.Name;
                _documentmessagetypesService.Create(documentmessagetypes);
                return RedirectToAction("Index");
            }

            return View(documentmessagetypes);
        }

        // GET: DocumentMessageTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            DocumentMessageTypesPost documentmessagetypes = _documentmessagetypesService.GetPost(id);
            if (documentmessagetypes == null)
            {
                return NotFound();
            }

            return View(documentmessagetypes);
        }

        // POST: DocumentMessageTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixDocumentMessageType,sDocumentMessageType,sDocumentMessageTypeCode")] DocumentMessageTypesPost documentmessagetypes)
        {
            if (ModelState.IsValid)
            {
                documentmessagetypes.UserName = User.Identity.Name;
                _documentmessagetypesService.Edit(documentmessagetypes);
                return RedirectToAction("Index");
            }

            return View(documentmessagetypes);
        }


        // GET: DocumentMessageTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_documentmessagetypesService.Get(id));
        }

        // POST: DocumentMessageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DocumentMessageTypesPost documentmessagetypes = _documentmessagetypesService.GetPost(id);
            documentmessagetypes.UserName = User.Identity.Name;
            _documentmessagetypesService.Delete(documentmessagetypes);
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
            string sDocumentMessageType;

            DocumentMessageTypesPost documentmessagetypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sDocumentMessageType = _documentmessagetypesService.Get(nID).sDocumentMessageType;
                            if (!_documentmessagetypesService.VerifyDocumentMessageTypeDeleteOK(nID, sDocumentMessageType).Any())
                            {
                                documentmessagetypes = _documentmessagetypesService.GetPost(nID);
                                documentmessagetypes.UserName = User.Identity.Name;
                                _documentmessagetypesService.Delete(documentmessagetypes);
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
        public IActionResult VerifyDocumentMessageType(long ixDocumentMessageType, string sDocumentMessageType)
        {
            string validationResponse = "";

            if (!_documentmessagetypesService.VerifyDocumentMessageTypeUnique(ixDocumentMessageType, sDocumentMessageType))
            {
                validationResponse = $"DocumentMessageType {sDocumentMessageType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

