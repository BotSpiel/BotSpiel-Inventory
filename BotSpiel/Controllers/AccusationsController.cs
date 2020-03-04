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

    public class AccusationsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IAccusationsService _accusationsService;

        public AccusationsController(IAccusationsService accusationsService )
        {
            _accusationsService = accusationsService;
        }

        // GET: Accusations
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var accusations = _accusationsService.Index();
            return View(accusations.ToList());
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
            var accusations = _accusationsService.Index();
            return PartialView("IndexGrid", accusations.ToList());
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
                IGrid<Accusations> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Accusations> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportAccusations.xlsx");
            }
        }

        private IGrid<Accusations> CreateExportableGrid()
        {
            IGrid<Accusations> grid = new Grid<Accusations>(_accusationsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sAccusation).Titled("Accusation").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Languages.sLanguage).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LanguageStyles.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.ResponseTypes.sResponseType).Titled("Response Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bActive).Titled("Active").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Accusations>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Accusations/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_accusationsService.Get(id));
        }

        // GET: Accusations/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_accusationsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_accusationsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_accusationsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View();
        }

        // POST: Accusations/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixAccusation,sAccusation,ixLanguage,ixLanguageStyle,sAccusationMade,sAdmissionDenial,ixResponseType,bActive")] AccusationsPost accusations)
        {
            if (ModelState.IsValid)
            {
                accusations.UserName = User.Identity.Name;
                _accusationsService.Create(accusations);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_accusationsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_accusationsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_accusationsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View(accusations);
        }

        // GET: Accusations/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            AccusationsPost accusations = _accusationsService.GetPost(id);
            if (accusations == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_accusationsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", accusations.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_accusationsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", accusations.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_accusationsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", accusations.ixResponseType);

            return View(accusations);
        }

        // POST: Accusations/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixAccusation,sAccusation,ixLanguage,ixLanguageStyle,sAccusationMade,sAdmissionDenial,ixResponseType,bActive")] AccusationsPost accusations)
        {
            if (ModelState.IsValid)
            {
                accusations.UserName = User.Identity.Name;
                _accusationsService.Edit(accusations);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_accusationsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", accusations.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_accusationsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", accusations.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_accusationsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", accusations.ixResponseType);

            return View(accusations);
        }


        // GET: Accusations/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_accusationsService.Get(id));
        }

        // POST: Accusations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AccusationsPost accusations = _accusationsService.GetPost(id);
            accusations.UserName = User.Identity.Name;
            _accusationsService.Delete(accusations);
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
            string sAccusation;

            AccusationsPost accusations;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sAccusation = _accusationsService.Get(nID).sAccusation;
                            if (!_accusationsService.VerifyAccusationDeleteOK(nID, sAccusation).Any())
                            {
                                accusations = _accusationsService.GetPost(nID);
                                accusations.UserName = User.Identity.Name;
                                _accusationsService.Delete(accusations);
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
        public IActionResult VerifyAccusation(long ixAccusation, string sAccusation)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

