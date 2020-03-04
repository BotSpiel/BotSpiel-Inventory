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

    public class SendEmailsController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly ISendEmailsService _sendemailsService;

        public SendEmailsController(ISendEmailsService sendemailsService )
        {
            _sendemailsService = sendemailsService;
        }

        // GET: SendEmails
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var sendemails = _sendemailsService.Index();
            return View(sendemails.ToList());
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
            var sendemails = _sendemailsService.Index();
            return PartialView("IndexGrid", sendemails.ToList());
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
                IGrid<SendEmails> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<SendEmails> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportSendEmails.xlsx");
            }
        }

        private IGrid<SendEmails> CreateExportableGrid()
        {
            IGrid<SendEmails> grid = new Grid<SendEmails>(_sendemailsService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sSendEmail).Titled("Send Email").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.People.sPerson).Titled("Person").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sSubject).Titled("Subject").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sAttachment).Titled("Attachment").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<SendEmails>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: SendEmails/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_sendemailsService.Get(id));
        }

        // GET: SendEmails/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPerson = new SelectList(_sendemailsService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson");

            return View();
        }

        // POST: SendEmails/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixSendEmail,sSendEmail,ixPerson,sSubject,sContent,sAttachment")] SendEmailsPost sendemails)
        {
            if (ModelState.IsValid)
            {
                sendemails.UserName = User.Identity.Name;
                _sendemailsService.Create(sendemails);
                return RedirectToAction("Index");
            }
			ViewBag.ixPerson = new SelectList(_sendemailsService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson");

            return View(sendemails);
        }

        // GET: SendEmails/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            SendEmailsPost sendemails = _sendemailsService.GetPost(id);
            if (sendemails == null)
            {
                return NotFound();
            }
			ViewBag.ixPerson = new SelectList(_sendemailsService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson", sendemails.ixPerson);

            return View(sendemails);
        }

        // POST: SendEmails/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixSendEmail,sSendEmail,ixPerson,sSubject,sContent,sAttachment")] SendEmailsPost sendemails)
        {
            if (ModelState.IsValid)
            {
                sendemails.UserName = User.Identity.Name;
                _sendemailsService.Edit(sendemails);
                return RedirectToAction("Index");
            }
			ViewBag.ixPerson = new SelectList(_sendemailsService.selectPeople().Select( x => new { x.ixPerson, x.sPerson }), "ixPerson", "sPerson", sendemails.ixPerson);

            return View(sendemails);
        }


        // GET: SendEmails/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_sendemailsService.Get(id));
        }

        // POST: SendEmails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            SendEmailsPost sendemails = _sendemailsService.GetPost(id);
            sendemails.UserName = User.Identity.Name;
            _sendemailsService.Delete(sendemails);
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
            string sSendEmail;

            SendEmailsPost sendemails;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sSendEmail = _sendemailsService.Get(nID).sSendEmail;
                            if (!_sendemailsService.VerifySendEmailDeleteOK(nID, sSendEmail).Any())
                            {
                                sendemails = _sendemailsService.GetPost(nID);
                                sendemails.UserName = User.Identity.Name;
                                _sendemailsService.Delete(sendemails);
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
        public IActionResult VerifySendEmail(long ixSendEmail, string sSendEmail)
        {
            string validationResponse = "";

            if (!_sendemailsService.VerifySendEmailUnique(ixSendEmail, sSendEmail))
            {
                validationResponse = $"SendEmail {sSendEmail} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

