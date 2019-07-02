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

    public class LocationFunctionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ILocationFunctionsService _locationfunctionsService;

        public LocationFunctionsController(ILocationFunctionsService locationfunctionsService )
        {
            _locationfunctionsService = locationfunctionsService;
        }

        // GET: LocationFunctions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var locationfunctions = _locationfunctionsService.Index();
            return View(locationfunctions.ToList());
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
            var locationfunctions = _locationfunctionsService.Index();
            return PartialView("IndexGrid", locationfunctions.ToList());
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
                IGrid<LocationFunctions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<LocationFunctions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportLocationFunctions.xlsx");
            }
        }

        private IGrid<LocationFunctions> CreateExportableGrid()
        {
            IGrid<LocationFunctions> grid = new Grid<LocationFunctions>(_locationfunctionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sLocationFunction).Titled("Location Function").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLocationFunctionCode).Titled("Location Function Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<LocationFunctions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: LocationFunctions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_locationfunctionsService.Get(id));
        }

        // GET: LocationFunctions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: LocationFunctions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixLocationFunction,sLocationFunction,sLocationFunctionCode")] LocationFunctionsPost locationfunctions)
        {
            if (ModelState.IsValid)
            {
                locationfunctions.UserName = User.Identity.Name;
                _locationfunctionsService.Create(locationfunctions);
                return RedirectToAction("Index");
            }

            return View(locationfunctions);
        }

        // GET: LocationFunctions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            LocationFunctionsPost locationfunctions = _locationfunctionsService.GetPost(id);
            if (locationfunctions == null)
            {
                return NotFound();
            }

            return View(locationfunctions);
        }

        // POST: LocationFunctions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixLocationFunction,sLocationFunction,sLocationFunctionCode")] LocationFunctionsPost locationfunctions)
        {
            if (ModelState.IsValid)
            {
                locationfunctions.UserName = User.Identity.Name;
                _locationfunctionsService.Edit(locationfunctions);
                return RedirectToAction("Index");
            }

            return View(locationfunctions);
        }


        // GET: LocationFunctions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_locationfunctionsService.Get(id));
        }

        // POST: LocationFunctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LocationFunctionsPost locationfunctions = _locationfunctionsService.GetPost(id);
            locationfunctions.UserName = User.Identity.Name;
            _locationfunctionsService.Delete(locationfunctions);
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
            string sLocationFunction;

            LocationFunctionsPost locationfunctions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sLocationFunction = _locationfunctionsService.Get(nID).sLocationFunction;
                            if (!_locationfunctionsService.VerifyLocationFunctionDeleteOK(nID, sLocationFunction).Any())
                            {
                                locationfunctions = _locationfunctionsService.GetPost(nID);
                                locationfunctions.UserName = User.Identity.Name;
                                _locationfunctionsService.Delete(locationfunctions);
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
        public IActionResult VerifyLocationFunction(long ixLocationFunction, string sLocationFunction)
        {
            string validationResponse = "";

            if (!_locationfunctionsService.VerifyLocationFunctionUnique(ixLocationFunction, sLocationFunction))
            {
                validationResponse = $"LocationFunction {sLocationFunction} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

