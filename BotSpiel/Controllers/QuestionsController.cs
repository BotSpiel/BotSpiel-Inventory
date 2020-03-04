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

    public class QuestionsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IQuestionsService _questionsService;

        public QuestionsController(IQuestionsService questionsService )
        {
            _questionsService = questionsService;
        }

        // GET: Questions
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var questions = _questionsService.Index();
            return View(questions.ToList());
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
            var questions = _questionsService.Index();
            return PartialView("IndexGrid", questions.ToList());
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
                IGrid<Questions> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<Questions> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportQuestions.xlsx");
            }
        }

        private IGrid<Questions> CreateExportableGrid()
        {
            IGrid<Questions> grid = new Grid<Questions>(_questionsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sQuestion).Titled("Question").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.LanguageStylesFKDiffLanguage.sLanguageStyle).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LanguageStyles.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Topics.sTopic).Titled("Topic").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.ResponseTypes.sResponseType).Titled("Response Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bActive).Titled("Active").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<Questions>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: Questions/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_questionsService.Get(id));
        }

        // GET: Questions/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixLanguageStyle = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_questionsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");
			ViewBag.ixTopic = new SelectList(_questionsService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic");

            return View();
        }

        // POST: Questions/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixQuestion,sQuestion,ixLanguage,ixLanguageStyle,ixTopic,sAsk,sAnswer,ixResponseType,bActive")] QuestionsPost questions)
        {
            if (ModelState.IsValid)
            {
                questions.UserName = User.Identity.Name;
                _questionsService.Create(questions);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixLanguageStyle = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_questionsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");
			ViewBag.ixTopic = new SelectList(_questionsService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic");

            return View(questions);
        }

        // GET: Questions/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            QuestionsPost questions = _questionsService.GetPost(id);
            if (questions == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", questions.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", questions.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_questionsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", questions.ixResponseType);
			ViewBag.ixTopic = new SelectList(_questionsService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic", questions.ixTopic);

            return View(questions);
        }

        // POST: Questions/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixQuestion,sQuestion,ixLanguage,ixLanguageStyle,ixTopic,sAsk,sAnswer,ixResponseType,bActive")] QuestionsPost questions)
        {
            if (ModelState.IsValid)
            {
                questions.UserName = User.Identity.Name;
                _questionsService.Edit(questions);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", questions.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_questionsService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", questions.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_questionsService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", questions.ixResponseType);
			ViewBag.ixTopic = new SelectList(_questionsService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic", questions.ixTopic);

            return View(questions);
        }


        // GET: Questions/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_questionsService.Get(id));
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            QuestionsPost questions = _questionsService.GetPost(id);
            questions.UserName = User.Identity.Name;
            _questionsService.Delete(questions);
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
            string sQuestion;

            QuestionsPost questions;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sQuestion = _questionsService.Get(nID).sQuestion;
                            if (!_questionsService.VerifyQuestionDeleteOK(nID, sQuestion).Any())
                            {
                                questions = _questionsService.GetPost(nID);
                                questions.UserName = User.Identity.Name;
                                _questionsService.Delete(questions);
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
        public IActionResult VerifyQuestion(long ixQuestion, string sQuestion)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

