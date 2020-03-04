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

    public class OutboundOrderLinesInventoryAllocationController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundOrderLinesInventoryAllocationService _outboundorderlinesinventoryallocationService;

        public OutboundOrderLinesInventoryAllocationController(IOutboundOrderLinesInventoryAllocationService outboundorderlinesinventoryallocationService )
        {
            _outboundorderlinesinventoryallocationService = outboundorderlinesinventoryallocationService;
        }

        // GET: OutboundOrderLinesInventoryAllocation
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundorderlinesinventoryallocation = _outboundorderlinesinventoryallocationService.Index();
            return View(outboundorderlinesinventoryallocation.ToList());
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
            var outboundorderlinesinventoryallocation = _outboundorderlinesinventoryallocationService.Index();
            return PartialView("IndexGrid", outboundorderlinesinventoryallocation.ToList());
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
                IGrid<OutboundOrderLinesInventoryAllocation> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundOrderLinesInventoryAllocation> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundOrderLinesInventoryAllocation.xlsx");
            }
        }

        private IGrid<OutboundOrderLinesInventoryAllocation> CreateExportableGrid()
        {
            IGrid<OutboundOrderLinesInventoryAllocation> grid = new Grid<OutboundOrderLinesInventoryAllocation>(_outboundorderlinesinventoryallocationService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundOrderLineInventoryAllocation).Titled("Outbound Order Line Inventory Allocation").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.OutboundOrderLines.sOutboundOrderLine).Titled("Outbound Order Line").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityAllocated).Titled("Base Unit Quantity Allocated").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityPicked).Titled("Base Unit Quantity Picked").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundOrderLinesInventoryAllocation>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundOrderLinesInventoryAllocation/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundorderlinesinventoryallocationService.Get(id));
        }

        // GET: OutboundOrderLinesInventoryAllocation/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinesinventoryallocationService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixStatus = new SelectList(_outboundorderlinesinventoryallocationService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundOrderLinesInventoryAllocation/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundOrderLineInventoryAllocation,sOutboundOrderLineInventoryAllocation,ixOutboundOrderLine,nBaseUnitQuantityAllocated,nBaseUnitQuantityPicked,ixStatus")] OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocation)
        {
            if (ModelState.IsValid)
            {
                outboundorderlinesinventoryallocation.UserName = User.Identity.Name;
                _outboundorderlinesinventoryallocationService.Create(outboundorderlinesinventoryallocation);
                return RedirectToAction("Index");
            }
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinesinventoryallocationService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixStatus = new SelectList(_outboundorderlinesinventoryallocationService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundorderlinesinventoryallocation);
        }

        // GET: OutboundOrderLinesInventoryAllocation/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocation = _outboundorderlinesinventoryallocationService.GetPost(id);
            if (outboundorderlinesinventoryallocation == null)
            {
                return NotFound();
            }
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinesinventoryallocationService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine", outboundorderlinesinventoryallocation.ixOutboundOrderLine);
			ViewBag.ixStatus = new SelectList(_outboundorderlinesinventoryallocationService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderlinesinventoryallocation.ixStatus);

            return View(outboundorderlinesinventoryallocation);
        }

        // POST: OutboundOrderLinesInventoryAllocation/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundOrderLineInventoryAllocation,sOutboundOrderLineInventoryAllocation,ixOutboundOrderLine,nBaseUnitQuantityAllocated,nBaseUnitQuantityPicked,ixStatus")] OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocation)
        {
            if (ModelState.IsValid)
            {
                outboundorderlinesinventoryallocation.UserName = User.Identity.Name;
                _outboundorderlinesinventoryallocationService.Edit(outboundorderlinesinventoryallocation);
                return RedirectToAction("Index");
            }
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinesinventoryallocationService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine", outboundorderlinesinventoryallocation.ixOutboundOrderLine);
			ViewBag.ixStatus = new SelectList(_outboundorderlinesinventoryallocationService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderlinesinventoryallocation.ixStatus);

            return View(outboundorderlinesinventoryallocation);
        }


        // GET: OutboundOrderLinesInventoryAllocation/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundorderlinesinventoryallocationService.Get(id));
        }

        // POST: OutboundOrderLinesInventoryAllocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocation = _outboundorderlinesinventoryallocationService.GetPost(id);
            outboundorderlinesinventoryallocation.UserName = User.Identity.Name;
            _outboundorderlinesinventoryallocationService.Delete(outboundorderlinesinventoryallocation);
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
            string sOutboundOrderLineInventoryAllocation;

            OutboundOrderLinesInventoryAllocationPost outboundorderlinesinventoryallocation;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundOrderLineInventoryAllocation = _outboundorderlinesinventoryallocationService.Get(nID).sOutboundOrderLineInventoryAllocation;
                            if (!_outboundorderlinesinventoryallocationService.VerifyOutboundOrderLineInventoryAllocationDeleteOK(nID, sOutboundOrderLineInventoryAllocation).Any())
                            {
                                outboundorderlinesinventoryallocation = _outboundorderlinesinventoryallocationService.GetPost(nID);
                                outboundorderlinesinventoryallocation.UserName = User.Identity.Name;
                                _outboundorderlinesinventoryallocationService.Delete(outboundorderlinesinventoryallocation);
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
        public IActionResult VerifyOutboundOrderLineInventoryAllocation(long ixOutboundOrderLineInventoryAllocation, string sOutboundOrderLineInventoryAllocation)
        {
            string validationResponse = "";

            if (!_outboundorderlinesinventoryallocationService.VerifyOutboundOrderLineInventoryAllocationUnique(ixOutboundOrderLineInventoryAllocation, sOutboundOrderLineInventoryAllocation))
            {
                validationResponse = $"OutboundOrderLineInventoryAllocation {sOutboundOrderLineInventoryAllocation} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

