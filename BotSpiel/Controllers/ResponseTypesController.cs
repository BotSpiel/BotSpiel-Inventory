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

    public class ResponseTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IResponseTypesService _responsetypesService;

        public ResponseTypesController(IResponseTypesService responsetypesService )
        {
            _responsetypesService = responsetypesService;
        }

        // GET: ResponseTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var responsetypes = _responsetypesService.Index();
            return View(responsetypes.ToList());
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
            var responsetypes = _responsetypesService.Index();
            return PartialView("IndexGrid", responsetypes.ToList());
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
                IGrid<ResponseTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<ResponseTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportResponseTypes.xlsx");
            }
        }

        private IGrid<ResponseTypes> CreateExportableGrid()
        {
            IGrid<ResponseTypes> grid = new Grid<ResponseTypes>(_responsetypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sResponseType).Titled("Response Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<ResponseTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: ResponseTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_responsetypesService.Get(id));
        }

        // GET: ResponseTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: ResponseTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixResponseType,sResponseType")] ResponseTypesPost responsetypes)
        {
            if (ModelState.IsValid)
            {
                responsetypes.UserName = User.Identity.Name;
                _responsetypesService.Create(responsetypes);
                return RedirectToAction("Index");
            }

            return View(responsetypes);
        }

        // GET: ResponseTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            ResponseTypesPost responsetypes = _responsetypesService.GetPost(id);
            if (responsetypes == null)
            {
                return NotFound();
            }

            return View(responsetypes);
        }

        // POST: ResponseTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixResponseType,sResponseType")] ResponseTypesPost responsetypes)
        {
            if (ModelState.IsValid)
            {
                responsetypes.UserName = User.Identity.Name;
                _responsetypesService.Edit(responsetypes);
                return RedirectToAction("Index");
            }

            return View(responsetypes);
        }


        // GET: ResponseTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_responsetypesService.Get(id));
        }

        // POST: ResponseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ResponseTypesPost responsetypes = _responsetypesService.GetPost(id);
            responsetypes.UserName = User.Identity.Name;
            _responsetypesService.Delete(responsetypes);
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
            string sResponseType;

            ResponseTypesPost responsetypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sResponseType = _responsetypesService.Get(nID).sResponseType;
                            if (!_responsetypesService.VerifyResponseTypeDeleteOK(nID, sResponseType).Any())
                            {
                                responsetypes = _responsetypesService.GetPost(nID);
                                responsetypes.UserName = User.Identity.Name;
                                _responsetypesService.Delete(responsetypes);
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
        public IActionResult VerifyResponseType(long ixResponseType, string sResponseType)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

