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

    public class PickBatchTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPickBatchTypesService _pickbatchtypesService;

        public PickBatchTypesController(IPickBatchTypesService pickbatchtypesService )
        {
            _pickbatchtypesService = pickbatchtypesService;
        }

        // GET: PickBatchTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var pickbatchtypes = _pickbatchtypesService.Index();
            return View(pickbatchtypes.ToList());
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
            var pickbatchtypes = _pickbatchtypesService.Index();
            return PartialView("IndexGrid", pickbatchtypes.ToList());
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
                IGrid<PickBatchTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PickBatchTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPickBatchTypes.xlsx");
            }
        }

        private IGrid<PickBatchTypes> CreateExportableGrid()
        {
            IGrid<PickBatchTypes> grid = new Grid<PickBatchTypes>(_pickbatchtypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPickBatchType).Titled("Pick Batch Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PickBatchTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PickBatchTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_pickbatchtypesService.Get(id));
        }

        // GET: PickBatchTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: PickBatchTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPickBatchType,sPickBatchType")] PickBatchTypesPost pickbatchtypes)
        {
            if (ModelState.IsValid)
            {
                pickbatchtypes.UserName = User.Identity.Name;
                _pickbatchtypesService.Create(pickbatchtypes);
                return RedirectToAction("Index");
            }

            return View(pickbatchtypes);
        }

        // GET: PickBatchTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PickBatchTypesPost pickbatchtypes = _pickbatchtypesService.GetPost(id);
            if (pickbatchtypes == null)
            {
                return NotFound();
            }

            return View(pickbatchtypes);
        }

        // POST: PickBatchTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPickBatchType,sPickBatchType")] PickBatchTypesPost pickbatchtypes)
        {
            if (ModelState.IsValid)
            {
                pickbatchtypes.UserName = User.Identity.Name;
                _pickbatchtypesService.Edit(pickbatchtypes);
                return RedirectToAction("Index");
            }

            return View(pickbatchtypes);
        }


        // GET: PickBatchTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_pickbatchtypesService.Get(id));
        }

        // POST: PickBatchTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PickBatchTypesPost pickbatchtypes = _pickbatchtypesService.GetPost(id);
            pickbatchtypes.UserName = User.Identity.Name;
            _pickbatchtypesService.Delete(pickbatchtypes);
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
            string sPickBatchType;

            PickBatchTypesPost pickbatchtypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPickBatchType = _pickbatchtypesService.Get(nID).sPickBatchType;
                            if (!_pickbatchtypesService.VerifyPickBatchTypeDeleteOK(nID, sPickBatchType).Any())
                            {
                                pickbatchtypes = _pickbatchtypesService.GetPost(nID);
                                pickbatchtypes.UserName = User.Identity.Name;
                                _pickbatchtypesService.Delete(pickbatchtypes);
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
        public IActionResult VerifyPickBatchType(long ixPickBatchType, string sPickBatchType)
        {
            string validationResponse = "";

            if (!_pickbatchtypesService.VerifyPickBatchTypeUnique(ixPickBatchType, sPickBatchType))
            {
                validationResponse = $"PickBatchType {sPickBatchType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

