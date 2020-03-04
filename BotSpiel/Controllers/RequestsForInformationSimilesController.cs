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

    public class RequestsForInformationSimilesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IRequestsForInformationSimilesService _requestsforinformationsimilesService;

        public RequestsForInformationSimilesController(IRequestsForInformationSimilesService requestsforinformationsimilesService )
        {
            _requestsforinformationsimilesService = requestsforinformationsimilesService;
        }

        // GET: RequestsForInformationSimiles
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var requestsforinformationsimiles = _requestsforinformationsimilesService.Index();
            return View(requestsforinformationsimiles.ToList());
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
            var requestsforinformationsimiles = _requestsforinformationsimilesService.Index();
            return PartialView("IndexGrid", requestsforinformationsimiles.ToList());
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
                IGrid<RequestsForInformationSimiles> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<RequestsForInformationSimiles> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportRequestsForInformationSimiles.xlsx");
            }
        }

        private IGrid<RequestsForInformationSimiles> CreateExportableGrid()
        {
            IGrid<RequestsForInformationSimiles> grid = new Grid<RequestsForInformationSimiles>(_requestsforinformationsimilesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sRequestsForInformationSimile).Titled("Requests For Information Simile").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.RequestsForInformation.sRequestForInformation).Titled("Request For Information").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<RequestsForInformationSimiles>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: RequestsForInformationSimiles/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_requestsforinformationsimilesService.Get(id));
        }

        // GET: RequestsForInformationSimiles/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixRequestForInformation = new SelectList(_requestsforinformationsimilesService.selectRequestsForInformation().Select( x => new { x.ixRequestForInformation, x.sRequestForInformation }), "ixRequestForInformation", "sRequestForInformation");

            return View();
        }

        // POST: RequestsForInformationSimiles/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixRequestsForInformationSimile,sRequestsForInformationSimile,ixRequestForInformation,sRequestsForInformationSimileText")] RequestsForInformationSimilesPost requestsforinformationsimiles)
        {
            if (ModelState.IsValid)
            {
                requestsforinformationsimiles.UserName = User.Identity.Name;
                _requestsforinformationsimilesService.Create(requestsforinformationsimiles);
                return RedirectToAction("Index");
            }
			ViewBag.ixRequestForInformation = new SelectList(_requestsforinformationsimilesService.selectRequestsForInformation().Select( x => new { x.ixRequestForInformation, x.sRequestForInformation }), "ixRequestForInformation", "sRequestForInformation");

            return View(requestsforinformationsimiles);
        }

        // GET: RequestsForInformationSimiles/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            RequestsForInformationSimilesPost requestsforinformationsimiles = _requestsforinformationsimilesService.GetPost(id);
            if (requestsforinformationsimiles == null)
            {
                return NotFound();
            }
			ViewBag.ixRequestForInformation = new SelectList(_requestsforinformationsimilesService.selectRequestsForInformation().Select( x => new { x.ixRequestForInformation, x.sRequestForInformation }), "ixRequestForInformation", "sRequestForInformation", requestsforinformationsimiles.ixRequestForInformation);

            return View(requestsforinformationsimiles);
        }

        // POST: RequestsForInformationSimiles/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixRequestsForInformationSimile,sRequestsForInformationSimile,ixRequestForInformation,sRequestsForInformationSimileText")] RequestsForInformationSimilesPost requestsforinformationsimiles)
        {
            if (ModelState.IsValid)
            {
                requestsforinformationsimiles.UserName = User.Identity.Name;
                _requestsforinformationsimilesService.Edit(requestsforinformationsimiles);
                return RedirectToAction("Index");
            }
			ViewBag.ixRequestForInformation = new SelectList(_requestsforinformationsimilesService.selectRequestsForInformation().Select( x => new { x.ixRequestForInformation, x.sRequestForInformation }), "ixRequestForInformation", "sRequestForInformation", requestsforinformationsimiles.ixRequestForInformation);

            return View(requestsforinformationsimiles);
        }


        // GET: RequestsForInformationSimiles/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_requestsforinformationsimilesService.Get(id));
        }

        // POST: RequestsForInformationSimiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RequestsForInformationSimilesPost requestsforinformationsimiles = _requestsforinformationsimilesService.GetPost(id);
            requestsforinformationsimiles.UserName = User.Identity.Name;
            _requestsforinformationsimilesService.Delete(requestsforinformationsimiles);
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
            string sRequestsForInformationSimile;

            RequestsForInformationSimilesPost requestsforinformationsimiles;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sRequestsForInformationSimile = _requestsforinformationsimilesService.Get(nID).sRequestsForInformationSimile;
                            if (!_requestsforinformationsimilesService.VerifyRequestsForInformationSimileDeleteOK(nID, sRequestsForInformationSimile).Any())
                            {
                                requestsforinformationsimiles = _requestsforinformationsimilesService.GetPost(nID);
                                requestsforinformationsimiles.UserName = User.Identity.Name;
                                _requestsforinformationsimilesService.Delete(requestsforinformationsimiles);
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
        public IActionResult VerifyRequestsForInformationSimile(long ixRequestsForInformationSimile, string sRequestsForInformationSimile)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

