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

    public class DropInventoryUnitsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IDropInventoryUnitsService _dropinventoryunitsService;

        public DropInventoryUnitsController(IDropInventoryUnitsService dropinventoryunitsService )
        {
            _dropinventoryunitsService = dropinventoryunitsService;
        }

        // GET: DropInventoryUnits
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var dropinventoryunits = _dropinventoryunitsService.Index();
            return View(dropinventoryunits.ToList());
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
            var dropinventoryunits = _dropinventoryunitsService.Index();
            return PartialView("IndexGrid", dropinventoryunits.ToList());
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
                IGrid<DropInventoryUnits> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<DropInventoryUnits> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportDropInventoryUnits.xlsx");
            }
        }

        private IGrid<DropInventoryUnits> CreateExportableGrid()
        {
            IGrid<DropInventoryUnits> grid = new Grid<DropInventoryUnits>(_dropinventoryunitsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sDropInventoryUnit).Titled("Drop Inventory Unit").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<DropInventoryUnits>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: DropInventoryUnits/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_dropinventoryunitsService.Get(id));
        }

        // GET: DropInventoryUnits/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: DropInventoryUnits/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixDropInventoryUnit,sDropInventoryUnit")] DropInventoryUnitsPost dropinventoryunits)
        {
            if (ModelState.IsValid)
            {
                dropinventoryunits.UserName = User.Identity.Name;
                _dropinventoryunitsService.Create(dropinventoryunits);
                return RedirectToAction("Index");
            }

            return View(dropinventoryunits);
        }

        // GET: DropInventoryUnits/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            DropInventoryUnitsPost dropinventoryunits = _dropinventoryunitsService.GetPost(id);
            if (dropinventoryunits == null)
            {
                return NotFound();
            }

            return View(dropinventoryunits);
        }

        // POST: DropInventoryUnits/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixDropInventoryUnit,sDropInventoryUnit")] DropInventoryUnitsPost dropinventoryunits)
        {
            if (ModelState.IsValid)
            {
                dropinventoryunits.UserName = User.Identity.Name;
                _dropinventoryunitsService.Edit(dropinventoryunits);
                return RedirectToAction("Index");
            }

            return View(dropinventoryunits);
        }


        // GET: DropInventoryUnits/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_dropinventoryunitsService.Get(id));
        }

        // POST: DropInventoryUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DropInventoryUnitsPost dropinventoryunits = _dropinventoryunitsService.GetPost(id);
            dropinventoryunits.UserName = User.Identity.Name;
            _dropinventoryunitsService.Delete(dropinventoryunits);
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
            string sDropInventoryUnit;

            DropInventoryUnitsPost dropinventoryunits;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sDropInventoryUnit = _dropinventoryunitsService.Get(nID).sDropInventoryUnit;
                            if (!_dropinventoryunitsService.VerifyDropInventoryUnitDeleteOK(nID, sDropInventoryUnit).Any())
                            {
                                dropinventoryunits = _dropinventoryunitsService.GetPost(nID);
                                dropinventoryunits.UserName = User.Identity.Name;
                                _dropinventoryunitsService.Delete(dropinventoryunits);
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
        public IActionResult VerifyDropInventoryUnit(long ixDropInventoryUnit, string sDropInventoryUnit)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

