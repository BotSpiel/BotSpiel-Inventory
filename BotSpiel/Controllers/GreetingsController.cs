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

    public class GreetingsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IGreetingsService _greetingsService;

        public GreetingsController(IGreetingsService greetingsService )
        {
            _greetingsService = greetingsService;
        }

        // GET: Greetings
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var greetings = _greetingsService.Index();
            return View(greetings.ToList());
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
            var greetings = _greetingsService.Index();
            return PartialView("IndexGrid", greetings.ToList());
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
                IGrid<Greetings> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Greetings> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportGreetings.xlsx");
            }
        }

        private IGrid<Greetings> CreateExportableGrid()
        {
            IGrid<Greetings> grid = new Grid<Greetings>(_greetingsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sGreeting).Titled("Greeting").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Languages.sLanguage).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LanguageStyles.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sGreetingOffered).Titled("Greeting Offered").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sGreetingResponse).Titled("Greeting Response").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.ResponseTypes.sResponseType).Titled("Response Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bActive).Titled("Active").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Greetings>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Greetings/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_greetingsService.Get(id));
        }

        // GET: Greetings/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_greetingsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_greetingsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_greetingsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View();
        }

        // POST: Greetings/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixGreeting,sGreeting,ixLanguage,ixLanguageStyle,sGreetingOffered,sGreetingResponse,ixResponseType,bActive")] GreetingsPost greetings)
        {
            if (ModelState.IsValid)
            {
                greetings.UserName = User.Identity.Name;
                _greetingsService.Create(greetings);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_greetingsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_greetingsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_greetingsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View(greetings);
        }

        // GET: Greetings/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            GreetingsPost greetings = _greetingsService.GetPost(id);
            if (greetings == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_greetingsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", greetings.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_greetingsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", greetings.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_greetingsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", greetings.ixResponseType);

            return View(greetings);
        }

        // POST: Greetings/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixGreeting,sGreeting,ixLanguage,ixLanguageStyle,sGreetingOffered,sGreetingResponse,ixResponseType,bActive")] GreetingsPost greetings)
        {
            if (ModelState.IsValid)
            {
                greetings.UserName = User.Identity.Name;
                _greetingsService.Edit(greetings);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_greetingsService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", greetings.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_greetingsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", greetings.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_greetingsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", greetings.ixResponseType);

            return View(greetings);
        }


        // GET: Greetings/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_greetingsService.Get(id));
        }

        // POST: Greetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            GreetingsPost greetings = _greetingsService.GetPost(id);
            greetings.UserName = User.Identity.Name;
            _greetingsService.Delete(greetings);
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
            string sGreeting;

            GreetingsPost greetings;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sGreeting = _greetingsService.Get(nID).sGreeting;
                            if (!_greetingsService.VerifyGreetingDeleteOK(nID, sGreeting).Any())
                            {
                                greetings = _greetingsService.GetPost(nID);
                                greetings.UserName = User.Identity.Name;
                                _greetingsService.Delete(greetings);
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
        public IActionResult VerifyGreeting(long ixGreeting, string sGreeting)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

