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

    public class ReceivingController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IReceivingService _receivingService;

        public ReceivingController(IReceivingService receivingService )
        {
            _receivingService = receivingService;
        }

        // GET: Receiving
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var receiving = _receivingService.Index();
            return View(receiving.ToList());
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
            var receiving = _receivingService.Index();
            return PartialView("IndexGrid", receiving.ToList());
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
                IGrid<Receiving> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Receiving> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportReceiving.xlsx");
            }
        }

        private IGrid<Receiving> CreateExportableGrid()
        {
            IGrid<Receiving> grid = new Grid<Receiving>(_receivingService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sReceipt).Titled("Receipt").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.InventoryLocations.sInventoryLocation).Titled("Inventory Location").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InboundOrders.sInboundOrder).Titled("Inbound Order").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.HandlingUnits.sHandlingUnit).Titled("Handling Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Materials.sMaterial).Titled("Material").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.nHandlingUnitQuantity).Titled("Handling Unit Quantity").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sBatchNumber).Titled("Batch Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sSerialNumber).Titled("Serial Number").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.nBaseUnitQuantityReceived).Titled("Base Unit Quantity Received").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Receiving>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Receiving/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_receivingService.Get(id));
        }

        // GET: Receiving/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnit = new SelectList(_receivingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixHandlingUnitType = new SelectList(_receivingService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
			ViewBag.ixInboundOrder = new SelectList(_receivingService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder");
			ViewBag.ixInventoryLocation = new SelectList(_receivingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixMaterial = new SelectList(_receivingService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_receivingService.selectMaterialHandlingUnitConfigurations().Select( x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
			ViewBag.ixStatus = new SelectList(_receivingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: Receiving/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixReceipt,sReceipt,ixInventoryLocation,ixInboundOrder,ixHandlingUnit,ixMaterial,ixMaterialHandlingUnitConfiguration,ixHandlingUnitType,nHandlingUnitQuantity,sBatchNumber,sSerialNumber,nBaseUnitQuantityReceived,ixStatus")] ReceivingPost receiving)
        {
            if (ModelState.IsValid)
            {
                receiving.UserName = User.Identity.Name;
                _receivingService.Create(receiving);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_receivingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixHandlingUnitType = new SelectList(_receivingService.selectHandlingUnitTypes().Select( x => new { x.ixHandlingUnitType, x.sHandlingUnitType }), "ixHandlingUnitType", "sHandlingUnitType");
			ViewBag.ixInboundOrder = new SelectList(_receivingService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder");
			ViewBag.ixInventoryLocation = new SelectList(_receivingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");
			ViewBag.ixMaterial = new SelectList(_receivingService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial");
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_receivingService.selectMaterialHandlingUnitConfigurations().Select( x => new { x.ixMaterialHandlingUnitConfiguration, x.sMaterialHandlingUnitConfiguration }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration");
			ViewBag.ixStatus = new SelectList(_receivingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(receiving);
        }

        // GET: Receiving/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ReceivingPost receiving = _receivingService.GetPost(id);
            if (receiving == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnit = new SelectList(_receivingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", receiving.ixHandlingUnit);
			ViewBag.ixHandlingUnitType = new SelectList(_receivingService.selectHandlingUnitTypesNullable().Select( x => new { ixHandlingUnitType = x.Key, sHandlingUnitType = x.Value }), "ixHandlingUnitType", "sHandlingUnitType", receiving.ixHandlingUnitType);
			ViewBag.ixInboundOrder = new SelectList(_receivingService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder", receiving.ixInboundOrder);
			ViewBag.ixInventoryLocation = new SelectList(_receivingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", receiving.ixInventoryLocation);
			ViewBag.ixMaterial = new SelectList(_receivingService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", receiving.ixMaterial);
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_receivingService.selectMaterialHandlingUnitConfigurationsNullable().Select( x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", receiving.ixMaterialHandlingUnitConfiguration);
			ViewBag.ixStatus = new SelectList(_receivingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", receiving.ixStatus);

            return View(receiving);
        }

        // POST: Receiving/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixReceipt,sReceipt,ixInventoryLocation,ixInboundOrder,ixHandlingUnit,ixMaterial,ixMaterialHandlingUnitConfiguration,ixHandlingUnitType,nHandlingUnitQuantity,sBatchNumber,sSerialNumber,nBaseUnitQuantityReceived,ixStatus")] ReceivingPost receiving)
        {
            if (ModelState.IsValid)
            {
                receiving.UserName = User.Identity.Name;
                _receivingService.Edit(receiving);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_receivingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", receiving.ixHandlingUnit);
			ViewBag.ixHandlingUnitType = new SelectList(_receivingService.selectHandlingUnitTypesNullable().Select( x => new { ixHandlingUnitType = x.Key, sHandlingUnitType = x.Value }), "ixHandlingUnitType", "sHandlingUnitType", receiving.ixHandlingUnitType);
			ViewBag.ixInboundOrder = new SelectList(_receivingService.selectInboundOrders().Select( x => new { x.ixInboundOrder, x.sInboundOrder }), "ixInboundOrder", "sInboundOrder", receiving.ixInboundOrder);
			ViewBag.ixInventoryLocation = new SelectList(_receivingService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", receiving.ixInventoryLocation);
			ViewBag.ixMaterial = new SelectList(_receivingService.selectMaterials().Select( x => new { x.ixMaterial, x.sMaterial }), "ixMaterial", "sMaterial", receiving.ixMaterial);
			ViewBag.ixMaterialHandlingUnitConfiguration = new SelectList(_receivingService.selectMaterialHandlingUnitConfigurationsNullable().Select( x => new { ixMaterialHandlingUnitConfiguration = x.Key, sMaterialHandlingUnitConfiguration = x.Value }), "ixMaterialHandlingUnitConfiguration", "sMaterialHandlingUnitConfiguration", receiving.ixMaterialHandlingUnitConfiguration);
			ViewBag.ixStatus = new SelectList(_receivingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", receiving.ixStatus);

            return View(receiving);
        }


        // GET: Receiving/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_receivingService.Get(id));
        }

        // POST: Receiving/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ReceivingPost receiving = _receivingService.GetPost(id);
            receiving.UserName = User.Identity.Name;
            _receivingService.Delete(receiving);
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
            string sReceipt;

            ReceivingPost receiving;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sReceipt = _receivingService.Get(nID).sReceipt;
                            if (!_receivingService.VerifyReceiptDeleteOK(nID, sReceipt).Any())
                            {
                                receiving = _receivingService.GetPost(nID);
                                receiving.UserName = User.Identity.Name;
                                _receivingService.Delete(receiving);
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
        public IActionResult VerifyReceipt(long ixReceipt, string sReceipt)
        {
            string validationResponse = "";

            if (!_receivingService.VerifyReceiptUnique(ixReceipt, sReceipt))
            {
                validationResponse = $"Receipt {sReceipt} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

