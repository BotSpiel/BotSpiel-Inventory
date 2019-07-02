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

    public class AisleFaceStorageTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IAisleFaceStorageTypesService _aislefacestoragetypesService;

        public AisleFaceStorageTypesController(IAisleFaceStorageTypesService aislefacestoragetypesService )
        {
            _aislefacestoragetypesService = aislefacestoragetypesService;
        }

        // GET: AisleFaceStorageTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var aislefacestoragetypes = _aislefacestoragetypesService.Index();
            return View(aislefacestoragetypes.ToList());
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
            var aislefacestoragetypes = _aislefacestoragetypesService.Index();
            return PartialView("IndexGrid", aislefacestoragetypes.ToList());
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
                IGrid<AisleFaceStorageTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<AisleFaceStorageTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportAisleFaceStorageTypes.xlsx");
            }
        }

        private IGrid<AisleFaceStorageTypes> CreateExportableGrid()
        {
            IGrid<AisleFaceStorageTypes> grid = new Grid<AisleFaceStorageTypes>(_aislefacestoragetypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sAisleFaceStorageType).Titled("Aisle Face Storage Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<AisleFaceStorageTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: AisleFaceStorageTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_aislefacestoragetypesService.Get(id));
        }

        // GET: AisleFaceStorageTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: AisleFaceStorageTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixAisleFaceStorageType,sAisleFaceStorageType")] AisleFaceStorageTypesPost aislefacestoragetypes)
        {
            if (ModelState.IsValid)
            {
                aislefacestoragetypes.UserName = User.Identity.Name;
                _aislefacestoragetypesService.Create(aislefacestoragetypes);
                return RedirectToAction("Index");
            }

            return View(aislefacestoragetypes);
        }

        // GET: AisleFaceStorageTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            AisleFaceStorageTypesPost aislefacestoragetypes = _aislefacestoragetypesService.GetPost(id);
            if (aislefacestoragetypes == null)
            {
                return NotFound();
            }

            return View(aislefacestoragetypes);
        }

        // POST: AisleFaceStorageTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixAisleFaceStorageType,sAisleFaceStorageType")] AisleFaceStorageTypesPost aislefacestoragetypes)
        {
            if (ModelState.IsValid)
            {
                aislefacestoragetypes.UserName = User.Identity.Name;
                _aislefacestoragetypesService.Edit(aislefacestoragetypes);
                return RedirectToAction("Index");
            }

            return View(aislefacestoragetypes);
        }


        // GET: AisleFaceStorageTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_aislefacestoragetypesService.Get(id));
        }

        // POST: AisleFaceStorageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AisleFaceStorageTypesPost aislefacestoragetypes = _aislefacestoragetypesService.GetPost(id);
            aislefacestoragetypes.UserName = User.Identity.Name;
            _aislefacestoragetypesService.Delete(aislefacestoragetypes);
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
            string sAisleFaceStorageType;

            AisleFaceStorageTypesPost aislefacestoragetypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sAisleFaceStorageType = _aislefacestoragetypesService.Get(nID).sAisleFaceStorageType;
                            if (!_aislefacestoragetypesService.VerifyAisleFaceStorageTypeDeleteOK(nID, sAisleFaceStorageType).Any())
                            {
                                aislefacestoragetypes = _aislefacestoragetypesService.GetPost(nID);
                                aislefacestoragetypes.UserName = User.Identity.Name;
                                _aislefacestoragetypesService.Delete(aislefacestoragetypes);
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
        public IActionResult VerifyAisleFaceStorageType(long ixAisleFaceStorageType, string sAisleFaceStorageType)
        {
            string validationResponse = "";

            if (!_aislefacestoragetypesService.VerifyAisleFaceStorageTypeUnique(ixAisleFaceStorageType, sAisleFaceStorageType))
            {
                validationResponse = $"AisleFaceStorageType {sAisleFaceStorageType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

