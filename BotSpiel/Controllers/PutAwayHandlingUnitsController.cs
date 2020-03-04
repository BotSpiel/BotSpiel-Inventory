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

    public class PutAwayHandlingUnitsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPutAwayHandlingUnitsService _putawayhandlingunitsService;

        public PutAwayHandlingUnitsController(IPutAwayHandlingUnitsService putawayhandlingunitsService )
        {
            _putawayhandlingunitsService = putawayhandlingunitsService;
        }

        // GET: PutAwayHandlingUnits
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var putawayhandlingunits = _putawayhandlingunitsService.Index();
            return View(putawayhandlingunits.ToList());
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
            var putawayhandlingunits = _putawayhandlingunitsService.Index();
            return PartialView("IndexGrid", putawayhandlingunits.ToList());
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
                IGrid<PutAwayHandlingUnits> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PutAwayHandlingUnits> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPutAwayHandlingUnits.xlsx");
            }
        }

        private IGrid<PutAwayHandlingUnits> CreateExportableGrid()
        {
            IGrid<PutAwayHandlingUnits> grid = new Grid<PutAwayHandlingUnits>(_putawayhandlingunitsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPutAwayHandlingUnit).Titled("Put Away Handling Unit").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sInventoryDropLocation).Titled("Inventory Drop Location").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.HandlingUnits.sHandlingUnit).Titled("Handling Unit").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.InventoryLocations.sInventoryLocation).Titled("Inventory Location").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PutAwayHandlingUnits>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PutAwayHandlingUnits/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_putawayhandlingunitsService.Get(id));
        }

        // GET: PutAwayHandlingUnits/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixHandlingUnit = new SelectList(_putawayhandlingunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryLocation = new SelectList(_putawayhandlingunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");

            return View();
        }

        // POST: PutAwayHandlingUnits/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPutAwayHandlingUnit,sPutAwayHandlingUnit,sInventoryDropLocation,ixHandlingUnit,ixInventoryLocation")] PutAwayHandlingUnitsPost putawayhandlingunits)
        {
            if (ModelState.IsValid)
            {
                putawayhandlingunits.UserName = User.Identity.Name;
                _putawayhandlingunitsService.Create(putawayhandlingunits);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_putawayhandlingunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit");
			ViewBag.ixInventoryLocation = new SelectList(_putawayhandlingunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation");

            return View(putawayhandlingunits);
        }

        // GET: PutAwayHandlingUnits/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PutAwayHandlingUnitsPost putawayhandlingunits = _putawayhandlingunitsService.GetPost(id);
            if (putawayhandlingunits == null)
            {
                return NotFound();
            }
			ViewBag.ixHandlingUnit = new SelectList(_putawayhandlingunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", putawayhandlingunits.ixHandlingUnit);
			ViewBag.ixInventoryLocation = new SelectList(_putawayhandlingunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", putawayhandlingunits.ixInventoryLocation);

            return View(putawayhandlingunits);
        }

        // POST: PutAwayHandlingUnits/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPutAwayHandlingUnit,sPutAwayHandlingUnit,sInventoryDropLocation,ixHandlingUnit,ixInventoryLocation")] PutAwayHandlingUnitsPost putawayhandlingunits)
        {
            if (ModelState.IsValid)
            {
                putawayhandlingunits.UserName = User.Identity.Name;
                _putawayhandlingunitsService.Edit(putawayhandlingunits);
                return RedirectToAction("Index");
            }
			ViewBag.ixHandlingUnit = new SelectList(_putawayhandlingunitsService.selectHandlingUnits().Select( x => new { x.ixHandlingUnit, x.sHandlingUnit }), "ixHandlingUnit", "sHandlingUnit", putawayhandlingunits.ixHandlingUnit);
			ViewBag.ixInventoryLocation = new SelectList(_putawayhandlingunitsService.selectInventoryLocations().Select( x => new { x.ixInventoryLocation, x.sInventoryLocation }), "ixInventoryLocation", "sInventoryLocation", putawayhandlingunits.ixInventoryLocation);

            return View(putawayhandlingunits);
        }


        // GET: PutAwayHandlingUnits/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_putawayhandlingunitsService.Get(id));
        }

        // POST: PutAwayHandlingUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PutAwayHandlingUnitsPost putawayhandlingunits = _putawayhandlingunitsService.GetPost(id);
            putawayhandlingunits.UserName = User.Identity.Name;
            _putawayhandlingunitsService.Delete(putawayhandlingunits);
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
            string sPutAwayHandlingUnit;

            PutAwayHandlingUnitsPost putawayhandlingunits;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPutAwayHandlingUnit = _putawayhandlingunitsService.Get(nID).sPutAwayHandlingUnit;
                            if (!_putawayhandlingunitsService.VerifyPutAwayHandlingUnitDeleteOK(nID, sPutAwayHandlingUnit).Any())
                            {
                                putawayhandlingunits = _putawayhandlingunitsService.GetPost(nID);
                                putawayhandlingunits.UserName = User.Identity.Name;
                                _putawayhandlingunitsService.Delete(putawayhandlingunits);
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
        public IActionResult VerifyPutAwayHandlingUnit(long ixPutAwayHandlingUnit, string sPutAwayHandlingUnit)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

