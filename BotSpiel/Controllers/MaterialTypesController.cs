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

    public class MaterialTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMaterialTypesService _materialtypesService;

        public MaterialTypesController(IMaterialTypesService materialtypesService )
        {
            _materialtypesService = materialtypesService;
        }

        // GET: MaterialTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var materialtypes = _materialtypesService.Index();
            return View(materialtypes.ToList());
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
            var materialtypes = _materialtypesService.Index();
            return PartialView("IndexGrid", materialtypes.ToList());
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
                IGrid<MaterialTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MaterialTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMaterialTypes.xlsx");
            }
        }

        private IGrid<MaterialTypes> CreateExportableGrid()
        {
            IGrid<MaterialTypes> grid = new Grid<MaterialTypes>(_materialtypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMaterialType).Titled("Material Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MaterialTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MaterialTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_materialtypesService.Get(id));
        }

        // GET: MaterialTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MaterialTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMaterialType,sMaterialType")] MaterialTypesPost materialtypes)
        {
            if (ModelState.IsValid)
            {
                materialtypes.UserName = User.Identity.Name;
                _materialtypesService.Create(materialtypes);
                return RedirectToAction("Index");
            }

            return View(materialtypes);
        }

        // GET: MaterialTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MaterialTypesPost materialtypes = _materialtypesService.GetPost(id);
            if (materialtypes == null)
            {
                return NotFound();
            }

            return View(materialtypes);
        }

        // POST: MaterialTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMaterialType,sMaterialType")] MaterialTypesPost materialtypes)
        {
            if (ModelState.IsValid)
            {
                materialtypes.UserName = User.Identity.Name;
                _materialtypesService.Edit(materialtypes);
                return RedirectToAction("Index");
            }

            return View(materialtypes);
        }


        // GET: MaterialTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_materialtypesService.Get(id));
        }

        // POST: MaterialTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MaterialTypesPost materialtypes = _materialtypesService.GetPost(id);
            materialtypes.UserName = User.Identity.Name;
            _materialtypesService.Delete(materialtypes);
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
            string sMaterialType;

            MaterialTypesPost materialtypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMaterialType = _materialtypesService.Get(nID).sMaterialType;
                            if (!_materialtypesService.VerifyMaterialTypeDeleteOK(nID, sMaterialType).Any())
                            {
                                materialtypes = _materialtypesService.GetPost(nID);
                                materialtypes.UserName = User.Identity.Name;
                                _materialtypesService.Delete(materialtypes);
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
        public IActionResult VerifyMaterialType(long ixMaterialType, string sMaterialType)
        {
            string validationResponse = "";

            if (!_materialtypesService.VerifyMaterialTypeUnique(ixMaterialType, sMaterialType))
            {
                validationResponse = $"MaterialType {sMaterialType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

