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

    public class CommunicationMediumsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ICommunicationMediumsService _communicationmediumsService;

        public CommunicationMediumsController(ICommunicationMediumsService communicationmediumsService )
        {
            _communicationmediumsService = communicationmediumsService;
        }

        // GET: CommunicationMediums
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var communicationmediums = _communicationmediumsService.Index();
            return View(communicationmediums.ToList());
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
            var communicationmediums = _communicationmediumsService.Index();
            return PartialView("IndexGrid", communicationmediums.ToList());
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
                IGrid<CommunicationMediums> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<CommunicationMediums> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportCommunicationMediums.xlsx");
            }
        }

        private IGrid<CommunicationMediums> CreateExportableGrid()
        {
            IGrid<CommunicationMediums> grid = new Grid<CommunicationMediums>(_communicationmediumsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sCommunicationMedium).Titled("Communication Medium").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCommunicationMediumCode).Titled("Communication Medium Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<CommunicationMediums>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: CommunicationMediums/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_communicationmediumsService.Get(id));
        }

        // GET: CommunicationMediums/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: CommunicationMediums/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixCommunicationMedium,sCommunicationMedium,sCommunicationMediumCode")] CommunicationMediumsPost communicationmediums)
        {
            if (ModelState.IsValid)
            {
                communicationmediums.UserName = User.Identity.Name;
                _communicationmediumsService.Create(communicationmediums);
                return RedirectToAction("Index");
            }

            return View(communicationmediums);
        }

        // GET: CommunicationMediums/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            CommunicationMediumsPost communicationmediums = _communicationmediumsService.GetPost(id);
            if (communicationmediums == null)
            {
                return NotFound();
            }

            return View(communicationmediums);
        }

        // POST: CommunicationMediums/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixCommunicationMedium,sCommunicationMedium,sCommunicationMediumCode")] CommunicationMediumsPost communicationmediums)
        {
            if (ModelState.IsValid)
            {
                communicationmediums.UserName = User.Identity.Name;
                _communicationmediumsService.Edit(communicationmediums);
                return RedirectToAction("Index");
            }

            return View(communicationmediums);
        }


        // GET: CommunicationMediums/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_communicationmediumsService.Get(id));
        }

        // POST: CommunicationMediums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CommunicationMediumsPost communicationmediums = _communicationmediumsService.GetPost(id);
            communicationmediums.UserName = User.Identity.Name;
            _communicationmediumsService.Delete(communicationmediums);
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
            string sCommunicationMedium;

            CommunicationMediumsPost communicationmediums;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sCommunicationMedium = _communicationmediumsService.Get(nID).sCommunicationMedium;
                            if (!_communicationmediumsService.VerifyCommunicationMediumDeleteOK(nID, sCommunicationMedium).Any())
                            {
                                communicationmediums = _communicationmediumsService.GetPost(nID);
                                communicationmediums.UserName = User.Identity.Name;
                                _communicationmediumsService.Delete(communicationmediums);
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
        public IActionResult VerifyCommunicationMedium(long ixCommunicationMedium, string sCommunicationMedium)
        {
            string validationResponse = "";

            if (!_communicationmediumsService.VerifyCommunicationMediumUnique(ixCommunicationMedium, sCommunicationMedium))
            {
                validationResponse = $"CommunicationMedium {sCommunicationMedium} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

