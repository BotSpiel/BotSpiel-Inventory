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

    public class BaySequenceTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IBaySequenceTypesService _baysequencetypesService;

        public BaySequenceTypesController(IBaySequenceTypesService baysequencetypesService )
        {
            _baysequencetypesService = baysequencetypesService;
        }

        // GET: BaySequenceTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var baysequencetypes = _baysequencetypesService.Index();
            return View(baysequencetypes.ToList());
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
            var baysequencetypes = _baysequencetypesService.Index();
            return PartialView("IndexGrid", baysequencetypes.ToList());
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
                IGrid<BaySequenceTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<BaySequenceTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportBaySequenceTypes.xlsx");
            }
        }

        private IGrid<BaySequenceTypes> CreateExportableGrid()
        {
            IGrid<BaySequenceTypes> grid = new Grid<BaySequenceTypes>(_baysequencetypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sBaySequenceType).Titled("Bay Sequence Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<BaySequenceTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: BaySequenceTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_baysequencetypesService.Get(id));
        }

        // GET: BaySequenceTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: BaySequenceTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixBaySequenceType,sBaySequenceType")] BaySequenceTypesPost baysequencetypes)
        {
            if (ModelState.IsValid)
            {
                baysequencetypes.UserName = User.Identity.Name;
                _baysequencetypesService.Create(baysequencetypes);
                return RedirectToAction("Index");
            }

            return View(baysequencetypes);
        }

        // GET: BaySequenceTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            BaySequenceTypesPost baysequencetypes = _baysequencetypesService.GetPost(id);
            if (baysequencetypes == null)
            {
                return NotFound();
            }

            return View(baysequencetypes);
        }

        // POST: BaySequenceTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixBaySequenceType,sBaySequenceType")] BaySequenceTypesPost baysequencetypes)
        {
            if (ModelState.IsValid)
            {
                baysequencetypes.UserName = User.Identity.Name;
                _baysequencetypesService.Edit(baysequencetypes);
                return RedirectToAction("Index");
            }

            return View(baysequencetypes);
        }


        // GET: BaySequenceTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_baysequencetypesService.Get(id));
        }

        // POST: BaySequenceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            BaySequenceTypesPost baysequencetypes = _baysequencetypesService.GetPost(id);
            baysequencetypes.UserName = User.Identity.Name;
            _baysequencetypesService.Delete(baysequencetypes);
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
            string sBaySequenceType;

            BaySequenceTypesPost baysequencetypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sBaySequenceType = _baysequencetypesService.Get(nID).sBaySequenceType;
                            if (!_baysequencetypesService.VerifyBaySequenceTypeDeleteOK(nID, sBaySequenceType).Any())
                            {
                                baysequencetypes = _baysequencetypesService.GetPost(nID);
                                baysequencetypes.UserName = User.Identity.Name;
                                _baysequencetypesService.Delete(baysequencetypes);
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
        public IActionResult VerifyBaySequenceType(long ixBaySequenceType, string sBaySequenceType)
        {
            string validationResponse = "";

            if (!_baysequencetypesService.VerifyBaySequenceTypeUnique(ixBaySequenceType, sBaySequenceType))
            {
                validationResponse = $"BaySequenceType {sBaySequenceType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

