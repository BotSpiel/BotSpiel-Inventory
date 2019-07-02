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

    public class InboundOrderTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInboundOrderTypesService _inboundordertypesService;

        public InboundOrderTypesController(IInboundOrderTypesService inboundordertypesService )
        {
            _inboundordertypesService = inboundordertypesService;
        }

        // GET: InboundOrderTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var inboundordertypes = _inboundordertypesService.Index();
            return View(inboundordertypes.ToList());
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
            var inboundordertypes = _inboundordertypesService.Index();
            return PartialView("IndexGrid", inboundordertypes.ToList());
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
                IGrid<InboundOrderTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InboundOrderTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInboundOrderTypes.xlsx");
            }
        }

        private IGrid<InboundOrderTypes> CreateExportableGrid()
        {
            IGrid<InboundOrderTypes> grid = new Grid<InboundOrderTypes>(_inboundordertypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInboundOrderType).Titled("Inbound Order Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InboundOrderTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InboundOrderTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_inboundordertypesService.Get(id));
        }

        // GET: InboundOrderTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: InboundOrderTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInboundOrderType,sInboundOrderType")] InboundOrderTypesPost inboundordertypes)
        {
            if (ModelState.IsValid)
            {
                inboundordertypes.UserName = User.Identity.Name;
                _inboundordertypesService.Create(inboundordertypes);
                return RedirectToAction("Index");
            }

            return View(inboundordertypes);
        }

        // GET: InboundOrderTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InboundOrderTypesPost inboundordertypes = _inboundordertypesService.GetPost(id);
            if (inboundordertypes == null)
            {
                return NotFound();
            }

            return View(inboundordertypes);
        }

        // POST: InboundOrderTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInboundOrderType,sInboundOrderType")] InboundOrderTypesPost inboundordertypes)
        {
            if (ModelState.IsValid)
            {
                inboundordertypes.UserName = User.Identity.Name;
                _inboundordertypesService.Edit(inboundordertypes);
                return RedirectToAction("Index");
            }

            return View(inboundordertypes);
        }


        // GET: InboundOrderTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_inboundordertypesService.Get(id));
        }

        // POST: InboundOrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InboundOrderTypesPost inboundordertypes = _inboundordertypesService.GetPost(id);
            inboundordertypes.UserName = User.Identity.Name;
            _inboundordertypesService.Delete(inboundordertypes);
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
            string sInboundOrderType;

            InboundOrderTypesPost inboundordertypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInboundOrderType = _inboundordertypesService.Get(nID).sInboundOrderType;
                            if (!_inboundordertypesService.VerifyInboundOrderTypeDeleteOK(nID, sInboundOrderType).Any())
                            {
                                inboundordertypes = _inboundordertypesService.GetPost(nID);
                                inboundordertypes.UserName = User.Identity.Name;
                                _inboundordertypesService.Delete(inboundordertypes);
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
        public IActionResult VerifyInboundOrderType(long ixInboundOrderType, string sInboundOrderType)
        {
            string validationResponse = "";

            if (!_inboundordertypesService.VerifyInboundOrderTypeUnique(ixInboundOrderType, sInboundOrderType))
            {
                validationResponse = $"InboundOrderType {sInboundOrderType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

