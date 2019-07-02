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

    public class OutboundOrderLinesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundOrderLinesService _outboundorderlinesService;

        public OutboundOrderLinesController(IOutboundOrderLinesService outboundorderlinesService )
        {
            _outboundorderlinesService = outboundorderlinesService;
        }

        // GET: OutboundOrderLines
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundorderlines = _outboundorderlinesService.Index();
            return View(outboundorderlines.ToList());
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
            var outboundorderlines = _outboundorderlinesService.Index();
            return PartialView("IndexGrid", outboundorderlines.ToList());
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
                IGrid<OutboundOrderLines> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundOrderLines> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundOrderLines.xlsx");
            }
        }

        private IGrid<OutboundOrderLines> CreateExportableGrid()
        {
            IGrid<OutboundOrderLines> grid = new Grid<OutboundOrderLines>(_outboundorderlinesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundOrderLine).Titled("Outbound Order Line").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sOrderLineReference).Titled("Order Line Reference").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Materials.sMaterial).Titled("Material").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sBatchNumber).Titled("Batch Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sSerialNumber).Titled("Serial Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityOrdered).Titled("Base Unit Quantity Ordered").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityShipped).Titled("Base Unit Quantity Shipped").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundOrderLines>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundOrderLines/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundorderlinesService.Get(id));
        }

        // GET: OutboundOrderLines/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixMaterial = new SelectList(_outboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixStatus = new SelectList(_outboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundOrderLines/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundOrderLine,sOutboundOrderLine,sOrderLineReference,ixMaterial,sBatchNumber,sSerialNumber,nBaseUnitQuantityOrdered,nBaseUnitQuantityShipped,ixStatus")] OutboundOrderLinesPost outboundorderlines)
        {
            if (ModelState.IsValid)
            {
                outboundorderlines.UserName = User.Identity.Name;
                _outboundorderlinesService.Create(outboundorderlines);
                return RedirectToAction("Index");
            }
			ViewBag.ixMaterial = new SelectList(_outboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixStatus = new SelectList(_outboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundorderlines);
        }

        // GET: OutboundOrderLines/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundOrderLinesPost outboundorderlines = _outboundorderlinesService.GetPost(id);
            if (outboundorderlines == null)
            {
                return NotFound();
            }
			ViewBag.ixMaterial = new SelectList(_outboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", outboundorderlines.ixMaterial);
			ViewBag.ixStatus = new SelectList(_outboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderlines.ixStatus);

            return View(outboundorderlines);
        }

        // POST: OutboundOrderLines/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundOrderLine,sOutboundOrderLine,sOrderLineReference,ixMaterial,sBatchNumber,sSerialNumber,nBaseUnitQuantityOrdered,nBaseUnitQuantityShipped,ixStatus")] OutboundOrderLinesPost outboundorderlines)
        {
            if (ModelState.IsValid)
            {
                outboundorderlines.UserName = User.Identity.Name;
                _outboundorderlinesService.Edit(outboundorderlines);
                return RedirectToAction("Index");
            }
			ViewBag.ixMaterial = new SelectList(_outboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", outboundorderlines.ixMaterial);
			ViewBag.ixStatus = new SelectList(_outboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderlines.ixStatus);

            return View(outboundorderlines);
        }


        // GET: OutboundOrderLines/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundorderlinesService.Get(id));
        }

        // POST: OutboundOrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundOrderLinesPost outboundorderlines = _outboundorderlinesService.GetPost(id);
            outboundorderlines.UserName = User.Identity.Name;
            _outboundorderlinesService.Delete(outboundorderlines);
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
            string sOutboundOrderLine;

            OutboundOrderLinesPost outboundorderlines;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundOrderLine = _outboundorderlinesService.Get(nID).sOutboundOrderLine;
                            if (!_outboundorderlinesService.VerifyOutboundOrderLineDeleteOK(nID, sOutboundOrderLine).Any())
                            {
                                outboundorderlines = _outboundorderlinesService.GetPost(nID);
                                outboundorderlines.UserName = User.Identity.Name;
                                _outboundorderlinesService.Delete(outboundorderlines);
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
        public IActionResult VerifyOutboundOrderLine(long ixOutboundOrderLine, string sOutboundOrderLine)
        {
            string validationResponse = "";

            if (!_outboundorderlinesService.VerifyOutboundOrderLineUnique(ixOutboundOrderLine, sOutboundOrderLine))
            {
                validationResponse = $"OutboundOrderLine {sOutboundOrderLine} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

