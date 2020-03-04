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

    public class FarewellsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IFarewellsService _farewellsService;

        public FarewellsController(IFarewellsService farewellsService )
        {
            _farewellsService = farewellsService;
        }

        // GET: Farewells
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var farewells = _farewellsService.Index();
            return View(farewells.ToList());
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
            var farewells = _farewellsService.Index();
            return PartialView("IndexGrid", farewells.ToList());
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
                IGrid<Farewells> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Farewells> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportFarewells.xlsx");
            }
        }

        private IGrid<Farewells> CreateExportableGrid()
        {
            IGrid<Farewells> grid = new Grid<Farewells>(_farewellsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sFarewell).Titled("Farewell").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Languages.sLanguage).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LanguageStyles.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.ResponseTypes.sResponseType).Titled("Response Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bActive).Titled("Active").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Farewells>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Farewells/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_farewellsService.Get(id));
        }

        // GET: Farewells/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_farewellsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_farewellsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_farewellsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View();
        }

        // POST: Farewells/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixFarewell,sFarewell,ixLanguage,ixLanguageStyle,sFarewellOffered,sFarewellResponse,ixResponseType,bActive")] FarewellsPost farewells)
        {
            if (ModelState.IsValid)
            {
                farewells.UserName = User.Identity.Name;
                _farewellsService.Create(farewells);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_farewellsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_farewellsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_farewellsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View(farewells);
        }

        // GET: Farewells/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            FarewellsPost farewells = _farewellsService.GetPost(id);
            if (farewells == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_farewellsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", farewells.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_farewellsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", farewells.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_farewellsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", farewells.ixResponseType);

            return View(farewells);
        }

        // POST: Farewells/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixFarewell,sFarewell,ixLanguage,ixLanguageStyle,sFarewellOffered,sFarewellResponse,ixResponseType,bActive")] FarewellsPost farewells)
        {
            if (ModelState.IsValid)
            {
                farewells.UserName = User.Identity.Name;
                _farewellsService.Edit(farewells);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_farewellsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", farewells.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_farewellsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", farewells.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_farewellsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", farewells.ixResponseType);

            return View(farewells);
        }


        // GET: Farewells/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_farewellsService.Get(id));
        }

        // POST: Farewells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            FarewellsPost farewells = _farewellsService.GetPost(id);
            farewells.UserName = User.Identity.Name;
            _farewellsService.Delete(farewells);
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
            string sFarewell;

            FarewellsPost farewells;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sFarewell = _farewellsService.Get(nID).sFarewell;
                            if (!_farewellsService.VerifyFarewellDeleteOK(nID, sFarewell).Any())
                            {
                                farewells = _farewellsService.GetPost(nID);
                                farewells.UserName = User.Identity.Name;
                                _farewellsService.Delete(farewells);
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
        public IActionResult VerifyFarewell(long ixFarewell, string sFarewell)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

