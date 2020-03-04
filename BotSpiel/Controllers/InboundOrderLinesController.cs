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

    public class InboundOrderLinesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInboundOrderLinesService _inboundorderlinesService;

        public InboundOrderLinesController(IInboundOrderLinesService inboundorderlinesService )
        {
            _inboundorderlinesService = inboundorderlinesService;
        }

        // GET: InboundOrderLines
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inboundorderlines = _inboundorderlinesService.Index();
            return View(inboundorderlines.ToList());
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
            var inboundorderlines = _inboundorderlinesService.Index();
            return PartialView("IndexGrid", inboundorderlines.ToList());
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
                IGrid<InboundOrderLines> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InboundOrderLines> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInboundOrderLines.xlsx");
            }
        }

        private IGrid<InboundOrderLines> CreateExportableGrid()
        {
            IGrid<InboundOrderLines> grid = new Grid<InboundOrderLines>(_inboundorderlinesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInboundOrderLine).Titled("Inbound Order Line").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.InboundOrders.sInboundOrder).Titled("Inbound Order").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sOrderLineReference).Titled("Order Line Reference").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Materials.sMaterial).Titled("Material").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nHandlingUnitQuantity).Titled("Handling Unit Quantity").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityExpected).Titled("Base Unit Quantity Expected").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityReceived).Titled("Base Unit Quantity Received").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sSerialNumber).Titled("Serial Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sBatchNumber).Titled("Batch Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtExpireAt).Titled("Expire At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InboundOrderLines>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InboundOrderLines/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inboundorderlinesService.Get(id));
        }

        // GET: InboundOrderLines/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnitType = new SelectList(_inboundorderlinesService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
			ViewBag.ixInboundOrder = new SelectList(_inboundorderlinesService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder");
			ViewBag.ixMaterial = new SelectList(_inboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Select( x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
			ViewBag.ixStatus = new SelectList(_inboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        //Custom Code Start | Added Code Block 
        [Authorize]
        [HttpGet]
        public ActionResult CreateWithID(long id)
        {
            ViewBag.ixHandlingUnitType = new SelectList(_inboundorderlinesService.selectHandlingUnitTypes().Select(x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            ViewBag.ixInboundOrder = new SelectList(_inboundorderlinesService.selectInboundOrders().Where(x => x.ixInboundOrder == id).Select(x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder");
            ViewBag.ixMaterial = new SelectList(_inboundorderlinesService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Select(x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
            ViewBag.ixStatus = new SelectList(_inboundorderlinesService.selectStatuses().Select(x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: InboundOrderLines/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithID([Bind("ixInboundOrderLine,sInboundOrderLine,ixInboundOrder,sOrderLineReference,ixMaterial,ixMaterialHandlingUnitConfiguration,ixHandlingUnitType,nHandlingUnitQuantity,nBaseUnitQuantityExpected,nBaseUnitQuantityReceived,sSerialNumber,sBatchNumber,dtExpireAt,ixStatus")] InboundOrderLinesPost inboundorderlines)
        {
            if (ModelState.IsValid)
            {
                inboundorderlines.UserName = User.Identity.Name;
                _inboundorderlinesService.Create(inboundorderlines);
                return RedirectToAction("Edit", "InboundOrders", new { id = inboundorderlines.ixInboundOrder });
            }
            ViewBag.ixHandlingUnitType = new SelectList(_inboundorderlinesService.selectHandlingUnitTypes().Select(x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
            ViewBag.ixInboundOrder = new SelectList(_inboundorderlinesService.selectInboundOrders().Select(x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder");
            ViewBag.ixMaterial = new SelectList(_inboundorderlinesService.selectMaterials().Select(x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
            ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Select(x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
            ViewBag.ixStatus = new SelectList(_inboundorderlinesService.selectStatuses().Select(x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(inboundorderlines);
        }
        //Custom Code End

        // POST: InboundOrderLines/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInboundOrderLine,sInboundOrderLine,ixInboundOrder,sOrderLineReference,ixMaterial,ixMaterialHandlingUnitConfiguration,ixHandlingUnitType,nHandlingUnitQuantity,nBaseUnitQuantityExpected,nBaseUnitQuantityReceived,sSerialNumber,sBatchNumber,dtExpireAt,ixStatus")] InboundOrderLinesPost inboundorderlines)
        {
            if (ModelState.IsValid)
            {
                inboundorderlines.UserName = User.Identity.Name;
                _inboundorderlinesService.Create(inboundorderlines);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnitType = new SelectList(_inboundorderlinesService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
			ViewBag.ixInboundOrder = new SelectList(_inboundorderlinesService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder");
			ViewBag.ixMaterial = new SelectList(_inboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Select( x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
			ViewBag.ixStatus = new SelectList(_inboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(inboundorderlines);
        }

        // GET: InboundOrderLines/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InboundOrderLinesPost inboundorderlines = _inboundorderlinesService.GetPost(id);
            if (inboundorderlines == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnitType = new SelectList(_inboundorderlinesService.selectHandlingUnitTypesNullable().Select( x => new { ixHandlingUnitType = x.Key, sHandlingUnitType = x.Value }), "ixHandlingUnitType", "sHandlingUnitType", inboundorderlines.ixHandlingUnitType);
			ViewBag.ixInboundOrder = new SelectList(_inboundorderlinesService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder", inboundorderlines.ixInboundOrder);
			ViewBag.ixMaterial = new SelectList(_inboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inboundorderlines.ixMaterial);
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_inboundorderlinesService.selectMaterialHandlingUnitConfigurationsNullable().Select( x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", inboundorderlines.ixMaterialHandlingUnitConfiguration);
			ViewBag.ixStatus = new SelectList(_inboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inboundorderlines.ixStatus);

            return View(inboundorderlines);
        }

        // POST: InboundOrderLines/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInboundOrderLine,sInboundOrderLine,ixInboundOrder,sOrderLineReference,ixMaterial,ixMaterialHandlingUnitConfiguration,ixHandlingUnitType,nHandlingUnitQuantity,nBaseUnitQuantityExpected,nBaseUnitQuantityReceived,sSerialNumber,sBatchNumber,dtExpireAt,ixStatus")] InboundOrderLinesPost inboundorderlines)
        {
            if (ModelState.IsValid)
            {
                inboundorderlines.UserName = User.Identity.Name;
                _inboundorderlinesService.Edit(inboundorderlines);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnitType = new SelectList(_inboundorderlinesService.selectHandlingUnitTypesNullable().Select( x => new { ixHandlingUnitType = x.Key, sHandlingUnitType = x.Value }), "ixHandlingUnitType", "sHandlingUnitType", inboundorderlines.ixHandlingUnitType);
			ViewBag.ixInboundOrder = new SelectList(_inboundorderlinesService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder", inboundorderlines.ixInboundOrder);
			ViewBag.ixMaterial = new SelectList(_inboundorderlinesService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", inboundorderlines.ixMaterial);
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_inboundorderlinesService.selectMaterialHandlingUnitConfigurationsNullable().Select( x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", inboundorderlines.ixMaterialHandlingUnitConfiguration);
			ViewBag.ixStatus = new SelectList(_inboundorderlinesService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", inboundorderlines.ixStatus);

            return View(inboundorderlines);
        }


        // GET: InboundOrderLines/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inboundorderlinesService.Get(id));
        }

        // POST: InboundOrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InboundOrderLinesPost inboundorderlines = _inboundorderlinesService.GetPost(id);
            inboundorderlines.UserName = User.Identity.Name;
            _inboundorderlinesService.Delete(inboundorderlines);
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
            string sInboundOrderLine;

            InboundOrderLinesPost inboundorderlines;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInboundOrderLine = _inboundorderlinesService.Get(nID).sInboundOrderLine;
                            if (!_inboundorderlinesService.VerifyInboundOrderLineDeleteOK(nID, sInboundOrderLine).Any())
                            {
                                inboundorderlines = _inboundorderlinesService.GetPost(nID);
                                inboundorderlines.UserName = User.Identity.Name;
                                _inboundorderlinesService.Delete(inboundorderlines);
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
        public IActionResult VerifyInboundOrderLine(long ixInboundOrderLine, string sInboundOrderLine)
        {
            string validationResponse = "";

            if (!_inboundorderlinesService.VerifyInboundOrderLineUnique(ixInboundOrderLine, sInboundOrderLine))
            {
                validationResponse = $"InboundOrderLine {sInboundOrderLine} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }

        //Custom Code Start | Added Code Block 
        [AcceptVerbs("Get", "Post")]
        [HttpPost]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration(string Id, string nUnits)
        {
            var nBaseUnitQuantity = _inboundorderlinesService.MaterialHandlingUnitConfigurationsDb().Where(x => x.ixMaterialHandlingUnitConfiguration == Convert.ToInt64(Id)).Select(x => new { nBaseUnitQuantityExpected = x.nQuantity * Convert.ToInt64(nUnits) });
            return Json(nBaseUnitQuantity);
        }
        //Custom Code End

    }
}
 

