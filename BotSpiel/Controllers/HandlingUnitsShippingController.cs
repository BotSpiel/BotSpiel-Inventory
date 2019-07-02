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

    public class HandlingUnitsShippingController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IHandlingUnitsShippingService _handlingunitsshippingService;

        public HandlingUnitsShippingController(IHandlingUnitsShippingService handlingunitsshippingService )
        {
            _handlingunitsshippingService = handlingunitsshippingService;
        }

        // GET: HandlingUnitsShipping
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var handlingunitsshipping = _handlingunitsshippingService.Index();
            return View(handlingunitsshipping.ToList());
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
            var handlingunitsshipping = _handlingunitsshippingService.Index();
            return PartialView("IndexGrid", handlingunitsshipping.ToList());
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
                IGrid<HandlingUnitsShipping> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<HandlingUnitsShipping> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportHandlingUnitsShipping.xlsx");
            }
        }

        private IGrid<HandlingUnitsShipping> CreateExportableGrid()
        {
            IGrid<HandlingUnitsShipping> grid = new Grid<HandlingUnitsShipping>(_handlingunitsshippingService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sHandlingUnitShipping).Titled("Handling Unit Shipping").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.HandlingUnits.sHandlingUnit).Titled("Handling Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Statuses.sStatus).Titled("Status").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<HandlingUnitsShipping>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: HandlingUnitsShipping/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_handlingunitsshippingService.Get(id));
        }

        // GET: HandlingUnitsShipping/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnit = new SelectList(_handlingunitsshippingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixStatus = new SelectList(_handlingunitsshippingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View();
        }

        // POST: HandlingUnitsShipping/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixHandlingUnitShipping,sHandlingUnitShipping,ixHandlingUnit,ixStatus")] HandlingUnitsShippingPost handlingunitsshipping)
        {
            if (ModelState.IsValid)
            {
                handlingunitsshipping.UserName = User.Identity.Name;
                _handlingunitsshippingService.Create(handlingunitsshipping);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_handlingunitsshippingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixStatus = new SelectList(_handlingunitsshippingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus");

            return View(handlingunitsshipping);
        }

        // GET: HandlingUnitsShipping/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            HandlingUnitsShippingPost handlingunitsshipping = _handlingunitsshippingService.GetPost(id);
            if (handlingunitsshipping == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnit = new SelectList(_handlingunitsshippingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", handlingunitsshipping.ixHandlingUnit);
			ViewBag.ixStatus = new SelectList(_handlingunitsshippingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", handlingunitsshipping.ixStatus);

            return View(handlingunitsshipping);
        }

        // POST: HandlingUnitsShipping/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixHandlingUnitShipping,sHandlingUnitShipping,ixHandlingUnit,ixStatus")] HandlingUnitsShippingPost handlingunitsshipping)
        {
            if (ModelState.IsValid)
            {
                handlingunitsshipping.UserName = User.Identity.Name;
                _handlingunitsshippingService.Edit(handlingunitsshipping);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_handlingunitsshippingService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", handlingunitsshipping.ixHandlingUnit);
			ViewBag.ixStatus = new SelectList(_handlingunitsshippingService.selectStatuses().Select( x => new { x.ixStatus, x.sStatus }), "ixStatus", "sStatus", handlingunitsshipping.ixStatus);

            return View(handlingunitsshipping);
        }


        // GET: HandlingUnitsShipping/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_handlingunitsshippingService.Get(id));
        }

        // POST: HandlingUnitsShipping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HandlingUnitsShippingPost handlingunitsshipping = _handlingunitsshippingService.GetPost(id);
            handlingunitsshipping.UserName = User.Identity.Name;
            _handlingunitsshippingService.Delete(handlingunitsshipping);
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
            string sHandlingUnitShipping;

            HandlingUnitsShippingPost handlingunitsshipping;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sHandlingUnitShipping = _handlingunitsshippingService.Get(nID).sHandlingUnitShipping;
                            if (!_handlingunitsshippingService.VerifyHandlingUnitShippingDeleteOK(nID, sHandlingUnitShipping).Any())
                            {
                                handlingunitsshipping = _handlingunitsshippingService.GetPost(nID);
                                handlingunitsshipping.UserName = User.Identity.Name;
                                _handlingunitsshippingService.Delete(handlingunitsshipping);
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
        public IActionResult VerifyHandlingUnitShipping(long ixHandlingUnitShipping, string sHandlingUnitShipping)
        {
            string validationResponse = "";

            if (!_handlingunitsshippingService.VerifyHandlingUnitShippingUnique(ixHandlingUnitShipping, sHandlingUnitShipping))
            {
                validationResponse = $"HandlingUnitShipping {sHandlingUnitShipping} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

