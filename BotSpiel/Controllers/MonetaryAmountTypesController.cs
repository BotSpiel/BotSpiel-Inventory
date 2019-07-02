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

    public class MonetaryAmountTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IMonetaryAmountTypesService _monetaryamounttypesService;

        public MonetaryAmountTypesController(IMonetaryAmountTypesService monetaryamounttypesService )
        {
            _monetaryamounttypesService = monetaryamounttypesService;
        }

        // GET: MonetaryAmountTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var monetaryamounttypes = _monetaryamounttypesService.Index();
            return View(monetaryamounttypes.ToList());
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
            var monetaryamounttypes = _monetaryamounttypesService.Index();
            return PartialView("IndexGrid", monetaryamounttypes.ToList());
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
                IGrid<MonetaryAmountTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<MonetaryAmountTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportMonetaryAmountTypes.xlsx");
            }
        }

        private IGrid<MonetaryAmountTypes> CreateExportableGrid()
        {
            IGrid<MonetaryAmountTypes> grid = new Grid<MonetaryAmountTypes>(_monetaryamounttypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sMonetaryAmountType).Titled("Monetary Amount Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sMonetaryAmountTypeCode).Titled("Monetary Amount Type Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<MonetaryAmountTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: MonetaryAmountTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_monetaryamounttypesService.Get(id));
        }

        // GET: MonetaryAmountTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: MonetaryAmountTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixMonetaryAmountType,sMonetaryAmountType,sMonetaryAmountTypeCode")] MonetaryAmountTypesPost monetaryamounttypes)
        {
            if (ModelState.IsValid)
            {
                monetaryamounttypes.UserName = User.Identity.Name;
                _monetaryamounttypesService.Create(monetaryamounttypes);
                return RedirectToAction("Index");
            }

            return View(monetaryamounttypes);
        }

        // GET: MonetaryAmountTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            MonetaryAmountTypesPost monetaryamounttypes = _monetaryamounttypesService.GetPost(id);
            if (monetaryamounttypes == null)
            {
                return NotFound();
            }

            return View(monetaryamounttypes);
        }

        // POST: MonetaryAmountTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixMonetaryAmountType,sMonetaryAmountType,sMonetaryAmountTypeCode")] MonetaryAmountTypesPost monetaryamounttypes)
        {
            if (ModelState.IsValid)
            {
                monetaryamounttypes.UserName = User.Identity.Name;
                _monetaryamounttypesService.Edit(monetaryamounttypes);
                return RedirectToAction("Index");
            }

            return View(monetaryamounttypes);
        }


        // GET: MonetaryAmountTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_monetaryamounttypesService.Get(id));
        }

        // POST: MonetaryAmountTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MonetaryAmountTypesPost monetaryamounttypes = _monetaryamounttypesService.GetPost(id);
            monetaryamounttypes.UserName = User.Identity.Name;
            _monetaryamounttypesService.Delete(monetaryamounttypes);
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
            string sMonetaryAmountType;

            MonetaryAmountTypesPost monetaryamounttypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sMonetaryAmountType = _monetaryamounttypesService.Get(nID).sMonetaryAmountType;
                            if (!_monetaryamounttypesService.VerifyMonetaryAmountTypeDeleteOK(nID, sMonetaryAmountType).Any())
                            {
                                monetaryamounttypes = _monetaryamounttypesService.GetPost(nID);
                                monetaryamounttypes.UserName = User.Identity.Name;
                                _monetaryamounttypesService.Delete(monetaryamounttypes);
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
        public IActionResult VerifyMonetaryAmountType(long ixMonetaryAmountType, string sMonetaryAmountType)
        {
            string validationResponse = "";

            if (!_monetaryamounttypesService.VerifyMonetaryAmountTypeUnique(ixMonetaryAmountType, sMonetaryAmountType))
            {
                validationResponse = $"MonetaryAmountType {sMonetaryAmountType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

