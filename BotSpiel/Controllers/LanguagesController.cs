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

    public class LanguagesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ILanguagesService _languagesService;

        public LanguagesController(ILanguagesService languagesService )
        {
            _languagesService = languagesService;
        }

        // GET: Languages
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var languages = _languagesService.Index();
            return View(languages.ToList());
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
            var languages = _languagesService.Index();
            return PartialView("IndexGrid", languages.ToList());
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
                IGrid<Languages> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Languages> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportLanguages.xlsx");
            }
        }

        private IGrid<Languages> CreateExportableGrid()
        {
            IGrid<Languages> grid = new Grid<Languages>(_languagesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sLanguage).Titled("Language").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sLanguageCode).Titled("Language Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Languages>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Languages/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_languagesService.Get(id));
        }

        // GET: Languages/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: Languages/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixLanguage,sLanguage,sLanguageCode")] LanguagesPost languages)
        {
            if (ModelState.IsValid)
            {
                languages.UserName = User.Identity.Name;
                _languagesService.Create(languages);
                return RedirectToAction("Index");
            }

            return View(languages);
        }

        // GET: Languages/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            LanguagesPost languages = _languagesService.GetPost(id);
            if (languages == null)
            {
                return NotFound();
            }

            return View(languages);
        }

        // POST: Languages/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixLanguage,sLanguage,sLanguageCode")] LanguagesPost languages)
        {
            if (ModelState.IsValid)
            {
                languages.UserName = User.Identity.Name;
                _languagesService.Edit(languages);
                return RedirectToAction("Index");
            }

            return View(languages);
        }


        // GET: Languages/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_languagesService.Get(id));
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            LanguagesPost languages = _languagesService.GetPost(id);
            languages.UserName = User.Identity.Name;
            _languagesService.Delete(languages);
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
            string sLanguage;

            LanguagesPost languages;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sLanguage = _languagesService.Get(nID).sLanguage;
                            if (!_languagesService.VerifyLanguageDeleteOK(nID, sLanguage).Any())
                            {
                                languages = _languagesService.GetPost(nID);
                                languages.UserName = User.Identity.Name;
                                _languagesService.Delete(languages);
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
        public IActionResult VerifyLanguage(long ixLanguage, string sLanguage)
        {
            string validationResponse = "";

            if (!_languagesService.VerifyLanguageUnique(ixLanguage, sLanguage))
            {
                validationResponse = $"Language {sLanguage} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

