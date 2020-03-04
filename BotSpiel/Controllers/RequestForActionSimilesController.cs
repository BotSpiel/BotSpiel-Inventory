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

    public class RequestForActionSimilesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IRequestForActionSimilesService _requestforactionsimilesService;

        public RequestForActionSimilesController(IRequestForActionSimilesService requestforactionsimilesService )
        {
            _requestforactionsimilesService = requestforactionsimilesService;
        }

        // GET: RequestForActionSimiles
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var requestforactionsimiles = _requestforactionsimilesService.Index();
            return View(requestforactionsimiles.ToList());
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
            var requestforactionsimiles = _requestforactionsimilesService.Index();
            return PartialView("IndexGrid", requestforactionsimiles.ToList());
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
                IGrid<RequestForActionSimiles> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<RequestForActionSimiles> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportRequestForActionSimiles.xlsx");
            }
        }

        private IGrid<RequestForActionSimiles> CreateExportableGrid()
        {
            IGrid<RequestForActionSimiles> grid = new Grid<RequestForActionSimiles>(_requestforactionsimilesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sRequestForActionSimile).Titled("Request For Action Simile").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.RequestsForAction.sRequestForAction).Titled("Request For Action").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<RequestForActionSimiles>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: RequestForActionSimiles/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_requestforactionsimilesService.Get(id));
        }

        // GET: RequestForActionSimiles/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixRequestForAction = new SelectList(_requestforactionsimilesService.selectRequestsForAction().Select( x => new { x.ixRequestForAction, x.sRequestForAction }), "ixRequestForAction", "sRequestForAction");

            return View();
        }

        // POST: RequestForActionSimiles/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixRequestForActionSimile,sRequestForActionSimile,ixRequestForAction,sRequestForActionSimileText")] RequestForActionSimilesPost requestforactionsimiles)
        {
            if (ModelState.IsValid)
            {
                requestforactionsimiles.UserName = User.Identity.Name;
                _requestforactionsimilesService.Create(requestforactionsimiles);
                return RedirectToAction("Index");
            }
			ViewBag.ixRequestForAction = new SelectList(_requestforactionsimilesService.selectRequestsForAction().Select( x => new { x.ixRequestForAction, x.sRequestForAction }), "ixRequestForAction", "sRequestForAction");

            return View(requestforactionsimiles);
        }

        // GET: RequestForActionSimiles/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            RequestForActionSimilesPost requestforactionsimiles = _requestforactionsimilesService.GetPost(id);
            if (requestforactionsimiles == null)
            {
                return NotFound();
            }
			ViewBag.ixRequestForAction = new SelectList(_requestforactionsimilesService.selectRequestsForAction().Select( x => new { x.ixRequestForAction, x.sRequestForAction }), "ixRequestForAction", "sRequestForAction", requestforactionsimiles.ixRequestForAction);

            return View(requestforactionsimiles);
        }

        // POST: RequestForActionSimiles/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixRequestForActionSimile,sRequestForActionSimile,ixRequestForAction,sRequestForActionSimileText")] RequestForActionSimilesPost requestforactionsimiles)
        {
            if (ModelState.IsValid)
            {
                requestforactionsimiles.UserName = User.Identity.Name;
                _requestforactionsimilesService.Edit(requestforactionsimiles);
                return RedirectToAction("Index");
            }
			ViewBag.ixRequestForAction = new SelectList(_requestforactionsimilesService.selectRequestsForAction().Select( x => new { x.ixRequestForAction, x.sRequestForAction }), "ixRequestForAction", "sRequestForAction", requestforactionsimiles.ixRequestForAction);

            return View(requestforactionsimiles);
        }


        // GET: RequestForActionSimiles/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_requestforactionsimilesService.Get(id));
        }

        // POST: RequestForActionSimiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RequestForActionSimilesPost requestforactionsimiles = _requestforactionsimilesService.GetPost(id);
            requestforactionsimiles.UserName = User.Identity.Name;
            _requestforactionsimilesService.Delete(requestforactionsimiles);
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
            string sRequestForActionSimile;

            RequestForActionSimilesPost requestforactionsimiles;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sRequestForActionSimile = _requestforactionsimilesService.Get(nID).sRequestForActionSimile;
                            if (!_requestforactionsimilesService.VerifyRequestForActionSimileDeleteOK(nID, sRequestForActionSimile).Any())
                            {
                                requestforactionsimiles = _requestforactionsimilesService.GetPost(nID);
                                requestforactionsimiles.UserName = User.Identity.Name;
                                _requestforactionsimilesService.Delete(requestforactionsimiles);
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
        public IActionResult VerifyRequestForActionSimile(long ixRequestForActionSimile, string sRequestForActionSimile)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

