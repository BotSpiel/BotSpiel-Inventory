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

    public class CarrierTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICarrierTypesService _carriertypesService;

        public CarrierTypesController(ICarrierTypesService carriertypesService )
        {
            _carriertypesService = carriertypesService;
        }

        // GET: CarrierTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var carriertypes = _carriertypesService.Index();
            return View(carriertypes.ToList());
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
            var carriertypes = _carriertypesService.Index();
            return PartialView("IndexGrid", carriertypes.ToList());
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
                IGrid<CarrierTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<CarrierTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCarrierTypes.xlsx");
            }
        }

        private IGrid<CarrierTypes> CreateExportableGrid()
        {
            IGrid<CarrierTypes> grid = new Grid<CarrierTypes>(_carriertypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCarrierType).Titled("Carrier Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<CarrierTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: CarrierTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_carriertypesService.Get(id));
        }

        // GET: CarrierTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: CarrierTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCarrierType,sCarrierType")] CarrierTypesPost carriertypes)
        {
            if (ModelState.IsValid)
            {
                carriertypes.UserName = User.Identity.Name;
                _carriertypesService.Create(carriertypes);
                return RedirectToAction("Index");
            }

            return View(carriertypes);
        }

        // GET: CarrierTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CarrierTypesPost carriertypes = _carriertypesService.GetPost(id);
            if (carriertypes == null)
            {
                return NotFound();
            }

            return View(carriertypes);
        }

        // POST: CarrierTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCarrierType,sCarrierType")] CarrierTypesPost carriertypes)
        {
            if (ModelState.IsValid)
            {
                carriertypes.UserName = User.Identity.Name;
                _carriertypesService.Edit(carriertypes);
                return RedirectToAction("Index");
            }

            return View(carriertypes);
        }


        // GET: CarrierTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_carriertypesService.Get(id));
        }

        // POST: CarrierTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CarrierTypesPost carriertypes = _carriertypesService.GetPost(id);
            carriertypes.UserName = User.Identity.Name;
            _carriertypesService.Delete(carriertypes);
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
            string sCarrierType;

            CarrierTypesPost carriertypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCarrierType = _carriertypesService.Get(nID).sCarrierType;
                            if (!_carriertypesService.VerifyCarrierTypeDeleteOK(nID, sCarrierType).Any())
                            {
                                carriertypes = _carriertypesService.GetPost(nID);
                                carriertypes.UserName = User.Identity.Name;
                                _carriertypesService.Delete(carriertypes);
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
        public IActionResult VerifyCarrierType(long ixCarrierType, string sCarrierType)
        {
            string validationResponse = "";

            if (!_carriertypesService.VerifyCarrierTypeUnique(ixCarrierType, sCarrierType))
            {
                validationResponse = $"CarrierType {sCarrierType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

