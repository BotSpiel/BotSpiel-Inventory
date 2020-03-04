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

    public class OutboundOrderLinePackingController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundOrderLinePackingService _outboundorderlinepackingService;

        public OutboundOrderLinePackingController(IOutboundOrderLinePackingService outboundorderlinepackingService )
        {
            _outboundorderlinepackingService = outboundorderlinepackingService;
        }

        // GET: OutboundOrderLinePacking
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundorderlinepacking = _outboundorderlinepackingService.Index();
            return View(outboundorderlinepacking.ToList());
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
            var outboundorderlinepacking = _outboundorderlinepackingService.Index();
            return PartialView("IndexGrid", outboundorderlinepacking.ToList());
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
                IGrid<OutboundOrderLinePacking> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundOrderLinePacking> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundOrderLinePacking.xlsx");
            }
        }

        private IGrid<OutboundOrderLinePacking> CreateExportableGrid()
        {
            IGrid<OutboundOrderLinePacking> grid = new Grid<OutboundOrderLinePacking>(_outboundorderlinepackingService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundOrderLinePack).Titled("Outbound Order Line Pack").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.OutboundOrderLines.sOutboundOrderLine).Titled("Outbound Order Line").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.HandlingUnits.sHandlingUnit).Titled("Handling Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityPacked).Titled("Base Unit Quantity Packed").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundOrderLinePacking>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundOrderLinePacking/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundorderlinepackingService.Get(id));
        }

        // GET: OutboundOrderLinePacking/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderlinepackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinepackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixStatus = new SelectList(_outboundorderlinepackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: OutboundOrderLinePacking/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundOrderLinePack,sOutboundOrderLinePack,ixOutboundOrderLine,ixHandlingUnit,nBaseUnitQuantityPacked,ixStatus")] OutboundOrderLinePackingPost outboundorderlinepacking)
        {
            if (ModelState.IsValid)
            {
                outboundorderlinepacking.UserName = User.Identity.Name;
                _outboundorderlinepackingService.Create(outboundorderlinepacking);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderlinepackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinepackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine");
			ViewBag.ixStatus = new SelectList(_outboundorderlinepackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(outboundorderlinepacking);
        }

        // GET: OutboundOrderLinePacking/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundOrderLinePackingPost outboundorderlinepacking = _outboundorderlinepackingService.GetPost(id);
            if (outboundorderlinepacking == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderlinepackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", outboundorderlinepacking.ixHandlingUnit);
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinepackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine", outboundorderlinepacking.ixOutboundOrderLine);
			ViewBag.ixStatus = new SelectList(_outboundorderlinepackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderlinepacking.ixStatus);

            return View(outboundorderlinepacking);
        }

        // POST: OutboundOrderLinePacking/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundOrderLinePack,sOutboundOrderLinePack,ixOutboundOrderLine,ixHandlingUnit,nBaseUnitQuantityPacked,ixStatus")] OutboundOrderLinePackingPost outboundorderlinepacking)
        {
            if (ModelState.IsValid)
            {
                outboundorderlinepacking.UserName = User.Identity.Name;
                _outboundorderlinepackingService.Edit(outboundorderlinepacking);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_outboundorderlinepackingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", outboundorderlinepacking.ixHandlingUnit);
			ViewBag.ixOutboundOrderLine = new SelectList(_outboundorderlinepackingService.selectOutboundOrderLines().Select( x => new { x.ixOutboundOrderLine, x.sOutboundOrderLine }), "ixOutboundOrderLine", "sOutboundOrderLine", outboundorderlinepacking.ixOutboundOrderLine);
			ViewBag.ixStatus = new SelectList(_outboundorderlinepackingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", outboundorderlinepacking.ixStatus);

            return View(outboundorderlinepacking);
        }


        // GET: OutboundOrderLinePacking/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundorderlinepackingService.Get(id));
        }

        // POST: OutboundOrderLinePacking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundOrderLinePackingPost outboundorderlinepacking = _outboundorderlinepackingService.GetPost(id);
            outboundorderlinepacking.UserName = User.Identity.Name;
            _outboundorderlinepackingService.Delete(outboundorderlinepacking);
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
            string sOutboundOrderLinePack;

            OutboundOrderLinePackingPost outboundorderlinepacking;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundOrderLinePack = _outboundorderlinepackingService.Get(nID).sOutboundOrderLinePack;
                            if (!_outboundorderlinepackingService.VerifyOutboundOrderLinePackDeleteOK(nID, sOutboundOrderLinePack).Any())
                            {
                                outboundorderlinepacking = _outboundorderlinepackingService.GetPost(nID);
                                outboundorderlinepacking.UserName = User.Identity.Name;
                                _outboundorderlinepackingService.Delete(outboundorderlinepacking);
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
        public IActionResult VerifyOutboundOrderLinePack(long ixOutboundOrderLinePack, string sOutboundOrderLinePack)
        {
            string validationResponse = "";

            if (!_outboundorderlinepackingService.VerifyOutboundOrderLinePackUnique(ixOutboundOrderLinePack, sOutboundOrderLinePack))
            {
                validationResponse = $"OutboundOrderLinePack {sOutboundOrderLinePack} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

