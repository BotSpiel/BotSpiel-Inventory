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

    public class HandlingUnitTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IHandlingUnitTypesService _handlingunittypesService;

        public HandlingUnitTypesController(IHandlingUnitTypesService handlingunittypesService )
        {
            _handlingunittypesService = handlingunittypesService;
        }

        // GET: HandlingUnitTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var handlingunittypes = _handlingunittypesService.Index();
            return View(handlingunittypes.ToList());
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
            var handlingunittypes = _handlingunittypesService.Index();
            return PartialView("IndexGrid", handlingunittypes.ToList());
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
                IGrid<HandlingUnitTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<HandlingUnitTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportHandlingUnitTypes.xlsx");
            }
        }

        private IGrid<HandlingUnitTypes> CreateExportableGrid()
        {
            IGrid<HandlingUnitTypes> grid = new Grid<HandlingUnitTypes>(_handlingunittypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sHandlingUnitType).Titled("Handling Unit Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<HandlingUnitTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: HandlingUnitTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_handlingunittypesService.Get(id));
        }

        // GET: HandlingUnitTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: HandlingUnitTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixHandlingUnitType,sHandlingUnitType")] HandlingUnitTypesPost handlingunittypes)
        {
            if (ModelState.IsValid)
            {
                handlingunittypes.UserName = User.Identity.Name;
                _handlingunittypesService.Create(handlingunittypes);
                return RedirectToAction("Index");
            }

            return View(handlingunittypes);
        }

        // GET: HandlingUnitTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            HandlingUnitTypesPost handlingunittypes = _handlingunittypesService.GetPost(id);
            if (handlingunittypes == null)
            {
                return NotFound();
            }

            return View(handlingunittypes);
        }

        // POST: HandlingUnitTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixHandlingUnitType,sHandlingUnitType")] HandlingUnitTypesPost handlingunittypes)
        {
            if (ModelState.IsValid)
            {
                handlingunittypes.UserName = User.Identity.Name;
                _handlingunittypesService.Edit(handlingunittypes);
                return RedirectToAction("Index");
            }

            return View(handlingunittypes);
        }


        // GET: HandlingUnitTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_handlingunittypesService.Get(id));
        }

        // POST: HandlingUnitTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            HandlingUnitTypesPost handlingunittypes = _handlingunittypesService.GetPost(id);
            handlingunittypes.UserName = User.Identity.Name;
            _handlingunittypesService.Delete(handlingunittypes);
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
            string sHandlingUnitType;

            HandlingUnitTypesPost handlingunittypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sHandlingUnitType = _handlingunittypesService.Get(nID).sHandlingUnitType;
                            if (!_handlingunittypesService.VerifyHandlingUnitTypeDeleteOK(nID, sHandlingUnitType).Any())
                            {
                                handlingunittypes = _handlingunittypesService.GetPost(nID);
                                handlingunittypes.UserName = User.Identity.Name;
                                _handlingunittypesService.Delete(handlingunittypes);
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
        public IActionResult VerifyHandlingUnitType(long ixHandlingUnitType, string sHandlingUnitType)
        {
            string validationResponse = "";

            if (!_handlingunittypesService.VerifyHandlingUnitTypeUnique(ixHandlingUnitType, sHandlingUnitType))
            {
                validationResponse = $"HandlingUnitType {sHandlingUnitType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

