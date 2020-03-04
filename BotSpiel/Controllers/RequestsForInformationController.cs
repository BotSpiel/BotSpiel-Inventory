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

    public class RequestsForInformationController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IRequestsForInformationService _requestsforinformationService;

        public RequestsForInformationController(IRequestsForInformationService requestsforinformationService )
        {
            _requestsforinformationService = requestsforinformationService;
        }

        // GET: RequestsForInformation
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var requestsforinformation = _requestsforinformationService.Index();
            return View(requestsforinformation.ToList());
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
            var requestsforinformation = _requestsforinformationService.Index();
            return PartialView("IndexGrid", requestsforinformation.ToList());
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
                IGrid<RequestsForInformation> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<RequestsForInformation> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportRequestsForInformation.xlsx");
            }
        }

        private IGrid<RequestsForInformation> CreateExportableGrid()
        {
            IGrid<RequestsForInformation> grid = new Grid<RequestsForInformation>(_requestsforinformationService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sRequestForInformation).Titled("Request For Information").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Languages.sLanguage).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LanguageStyles.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.Topics.sTopic).Titled("Topic").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.ResponseTypes.sResponseType).Titled("Response Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bActive).Titled("Active").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<RequestsForInformation>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: RequestsForInformation/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_requestsforinformationService.Get(id));
        }

        // GET: RequestsForInformation/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_requestsforinformationService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_requestsforinformationService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_requestsforinformationService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");
			ViewBag.ixTopic = new SelectList(_requestsforinformationService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic");

            return View();
        }

        // POST: RequestsForInformation/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixRequestForInformation,sRequestForInformation,ixLanguage,ixLanguageStyle,ixTopic,sInformationRequest,sInformationRequestResponse,ixResponseType,bActive")] RequestsForInformationPost requestsforinformation)
        {
            if (ModelState.IsValid)
            {
                requestsforinformation.UserName = User.Identity.Name;
                _requestsforinformationService.Create(requestsforinformation);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_requestsforinformationService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_requestsforinformationService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_requestsforinformationService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");
			ViewBag.ixTopic = new SelectList(_requestsforinformationService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic");

            return View(requestsforinformation);
        }

        // GET: RequestsForInformation/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            RequestsForInformationPost requestsforinformation = _requestsforinformationService.GetPost(id);
            if (requestsforinformation == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_requestsforinformationService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", requestsforinformation.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_requestsforinformationService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", requestsforinformation.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_requestsforinformationService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", requestsforinformation.ixResponseType);
			ViewBag.ixTopic = new SelectList(_requestsforinformationService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic", requestsforinformation.ixTopic);

            return View(requestsforinformation);
        }

        // POST: RequestsForInformation/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixRequestForInformation,sRequestForInformation,ixLanguage,ixLanguageStyle,ixTopic,sInformationRequest,sInformationRequestResponse,ixResponseType,bActive")] RequestsForInformationPost requestsforinformation)
        {
            if (ModelState.IsValid)
            {
                requestsforinformation.UserName = User.Identity.Name;
                _requestsforinformationService.Edit(requestsforinformation);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_requestsforinformationService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", requestsforinformation.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_requestsforinformationService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", requestsforinformation.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_requestsforinformationService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", requestsforinformation.ixResponseType);
			ViewBag.ixTopic = new SelectList(_requestsforinformationService.selectTopics().Select( x => new { x.ixTopic, x.sTopic }), "ixTopic", "sTopic", requestsforinformation.ixTopic);

            return View(requestsforinformation);
        }


        // GET: RequestsForInformation/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_requestsforinformationService.Get(id));
        }

        // POST: RequestsForInformation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            RequestsForInformationPost requestsforinformation = _requestsforinformationService.GetPost(id);
            requestsforinformation.UserName = User.Identity.Name;
            _requestsforinformationService.Delete(requestsforinformation);
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
            string sRequestForInformation;

            RequestsForInformationPost requestsforinformation;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sRequestForInformation = _requestsforinformationService.Get(nID).sRequestForInformation;
                            if (!_requestsforinformationService.VerifyRequestForInformationDeleteOK(nID, sRequestForInformation).Any())
                            {
                                requestsforinformation = _requestsforinformationService.GetPost(nID);
                                requestsforinformation.UserName = User.Identity.Name;
                                _requestsforinformationService.Delete(requestsforinformation);
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
        public IActionResult VerifyRequestForInformation(long ixRequestForInformation, string sRequestForInformation)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

