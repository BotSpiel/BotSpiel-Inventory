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

    public class RequestsForActionController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IRequestsForActionService _requestsforactionService;

        public RequestsForActionController(IRequestsForActionService requestsforactionService )
        {
            _requestsforactionService = requestsforactionService;
        }

        // GET: RequestsForAction
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var requestsforaction = _requestsforactionService.Index();
            return View(requestsforaction.ToList());
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
            var requestsforaction = _requestsforactionService.Index();
            return PartialView("IndexGrid", requestsforaction.ToList());
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
                IGrid<RequestsForAction> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<RequestsForAction> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportRequestsForAction.xlsx");
            }
        }

        private IGrid<RequestsForAction> CreateExportableGrid()
        {
            IGrid<RequestsForAction> grid = new Grid<RequestsForAction>(_requestsforactionService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sRequestForAction).Titled("Request For Action").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Languages.sLanguage).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LanguageStyles.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sModule).Titled("Module").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sEntity).Titled("Entity").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sEntityIntent).Titled("Entity Intent").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<RequestsForAction>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: RequestsForAction/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_requestsforactionService.Get(id));
        }

        // GET: RequestsForAction/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_requestsforactionService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_requestsforactionService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");

            return View();
        }

        // POST: RequestsForAction/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixRequestForAction,sRequestForAction,ixLanguage,ixLanguageStyle,sActionRequest,sModule,sEntity,sEntityIntent")] RequestsForActionPost requestsforaction)
        {
            if (ModelState.IsValid)
            {
                requestsforaction.UserName = User.Identity.Name;
                _requestsforactionService.Create(requestsforaction);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_requestsforactionService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_requestsforactionService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");

            return View(requestsforaction);
        }

        // GET: RequestsForAction/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            RequestsForActionPost requestsforaction = _requestsforactionService.GetPost(id);
            if (requestsforaction == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_requestsforactionService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", requestsforaction.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_requestsforactionService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", requestsforaction.ixLanguageStyle);

            return View(requestsforaction);
        }

        // POST: RequestsForAction/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixRequestForAction,sRequestForAction,ixLanguage,ixLanguageStyle,sActionRequest,sModule,sEntity,sEntityIntent")] RequestsForActionPost requestsforaction)
        {
            if (ModelState.IsValid)
            {
                requestsforaction.UserName = User.Identity.Name;
                _requestsforactionService.Edit(requestsforaction);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_requestsforactionService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", requestsforaction.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_requestsforactionService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", requestsforaction.ixLanguageStyle);

            return View(requestsforaction);
        }


        // GET: RequestsForAction/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_requestsforactionService.Get(id));
        }

        // POST: RequestsForAction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RequestsForActionPost requestsforaction = _requestsforactionService.GetPost(id);
            requestsforaction.UserName = User.Identity.Name;
            _requestsforactionService.Delete(requestsforaction);
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
            string sRequestForAction;

            RequestsForActionPost requestsforaction;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sRequestForAction = _requestsforactionService.Get(nID).sRequestForAction;
                            if (!_requestsforactionService.VerifyRequestForActionDeleteOK(nID, sRequestForAction).Any())
                            {
                                requestsforaction = _requestsforactionService.GetPost(nID);
                                requestsforaction.UserName = User.Identity.Name;
                                _requestsforactionService.Delete(requestsforaction);
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
        public IActionResult VerifyRequestForAction(long ixRequestForAction, string sRequestForAction)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

