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

    public class OutboundOrderTypesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IOutboundOrderTypesService _outboundordertypesService;

        public OutboundOrderTypesController(IOutboundOrderTypesService outboundordertypesService )
        {
            _outboundordertypesService = outboundordertypesService;
        }

        // GET: OutboundOrderTypes
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var outboundordertypes = _outboundordertypesService.Index();
            return View(outboundordertypes.ToList());
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
            var outboundordertypes = _outboundordertypesService.Index();
            return PartialView("IndexGrid", outboundordertypes.ToList());
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
                IGrid<OutboundOrderTypes> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<OutboundOrderTypes> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportOutboundOrderTypes.xlsx");
            }
        }

        private IGrid<OutboundOrderTypes> CreateExportableGrid()
        {
            IGrid<OutboundOrderTypes> grid = new Grid<OutboundOrderTypes>(_outboundordertypesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sOutboundOrderType).Titled("Outbound Order Type").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<OutboundOrderTypes>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: OutboundOrderTypes/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_outboundordertypesService.Get(id));
        }

        // GET: OutboundOrderTypes/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: OutboundOrderTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixOutboundOrderType,sOutboundOrderType")] OutboundOrderTypesPost outboundordertypes)
        {
            if (ModelState.IsValid)
            {
                outboundordertypes.UserName = User.Identity.Name;
                _outboundordertypesService.Create(outboundordertypes);
                return RedirectToAction("Index");
            }

            return View(outboundordertypes);
        }

        // GET: OutboundOrderTypes/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            OutboundOrderTypesPost outboundordertypes = _outboundordertypesService.GetPost(id);
            if (outboundordertypes == null)
            {
                return NotFound();
            }

            return View(outboundordertypes);
        }

        // POST: OutboundOrderTypes/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixOutboundOrderType,sOutboundOrderType")] OutboundOrderTypesPost outboundordertypes)
        {
            if (ModelState.IsValid)
            {
                outboundordertypes.UserName = User.Identity.Name;
                _outboundordertypesService.Edit(outboundordertypes);
                return RedirectToAction("Index");
            }

            return View(outboundordertypes);
        }


        // GET: OutboundOrderTypes/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_outboundordertypesService.Get(id));
        }

        // POST: OutboundOrderTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            OutboundOrderTypesPost outboundordertypes = _outboundordertypesService.GetPost(id);
            outboundordertypes.UserName = User.Identity.Name;
            _outboundordertypesService.Delete(outboundordertypes);
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
            string sOutboundOrderType;

            OutboundOrderTypesPost outboundordertypes;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sOutboundOrderType = _outboundordertypesService.Get(nID).sOutboundOrderType;
                            if (!_outboundordertypesService.VerifyOutboundOrderTypeDeleteOK(nID, sOutboundOrderType).Any())
                            {
                                outboundordertypes = _outboundordertypesService.GetPost(nID);
                                outboundordertypes.UserName = User.Identity.Name;
                                _outboundordertypesService.Delete(outboundordertypes);
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
        public IActionResult VerifyOutboundOrderType(long ixOutboundOrderType, string sOutboundOrderType)
        {
            string validationResponse = "";

            if (!_outboundordertypesService.VerifyOutboundOrderTypeUnique(ixOutboundOrderType, sOutboundOrderType))
            {
                validationResponse = $"OutboundOrderType {sOutboundOrderType} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

