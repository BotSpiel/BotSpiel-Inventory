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

    public class InvitationsOffersController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IInvitationsOffersService _invitationsoffersService;

        public InvitationsOffersController(IInvitationsOffersService invitationsoffersService )
        {
            _invitationsoffersService = invitationsoffersService;
        }

        // GET: InvitationsOffers
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var invitationsoffers = _invitationsoffersService.Index();
            return View(invitationsoffers.ToList());
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
            var invitationsoffers = _invitationsoffersService.Index();
            return PartialView("IndexGrid", invitationsoffers.ToList());
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
                IGrid<InvitationsOffers> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<InvitationsOffers> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportInvitationsOffers.xlsx");
            }
        }

        private IGrid<InvitationsOffers> CreateExportableGrid()
        {
            IGrid<InvitationsOffers> grid = new Grid<InvitationsOffers>(_invitationsoffersService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sInvitationOffer).Titled("Invitation Offer").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Languages.sLanguage).Titled("Language").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.LanguageStyles.sLanguageStyle).Titled("Language Style").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.ResponseTypes.sResponseType).Titled("Response Type").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.bActive).Titled("Active").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<InvitationsOffers>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: InvitationsOffers/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_invitationsoffersService.Get(id));
        }

        // GET: InvitationsOffers/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixLanguage = new SelectList(_invitationsoffersService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_invitationsoffersService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_invitationsoffersService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View();
        }

        // POST: InvitationsOffers/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixInvitationOffer,sInvitationOffer,ixLanguage,ixLanguageStyle,sInvitationOffered,sAcceptDecline,ixResponseType,bActive")] InvitationsOffersPost invitationsoffers)
        {
            if (ModelState.IsValid)
            {
                invitationsoffers.UserName = User.Identity.Name;
                _invitationsoffersService.Create(invitationsoffers);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_invitationsoffersService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage");
			ViewBag.ixLanguageStyle = new SelectList(_invitationsoffersService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle");
			ViewBag.ixResponseType = new SelectList(_invitationsoffersService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType");

            return View(invitationsoffers);
        }

        // GET: InvitationsOffers/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            InvitationsOffersPost invitationsoffers = _invitationsoffersService.GetPost(id);
            if (invitationsoffers == null)
            {
                return NotFound();
            }
			ViewBag.ixLanguage = new SelectList(_invitationsoffersService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", invitationsoffers.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_invitationsoffersService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", invitationsoffers.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_invitationsoffersService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", invitationsoffers.ixResponseType);

            return View(invitationsoffers);
        }

        // POST: InvitationsOffers/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixInvitationOffer,sInvitationOffer,ixLanguage,ixLanguageStyle,sInvitationOffered,sAcceptDecline,ixResponseType,bActive")] InvitationsOffersPost invitationsoffers)
        {
            if (ModelState.IsValid)
            {
                invitationsoffers.UserName = User.Identity.Name;
                _invitationsoffersService.Edit(invitationsoffers);
                return RedirectToAction("Index");
            }
			ViewBag.ixLanguage = new SelectList(_invitationsoffersService.selectLanguages().Select( x => new { x.ixLanguage, x.sLanguage }), "ixLanguage", "sLanguage", invitationsoffers.ixLanguage);
			ViewBag.ixLanguageStyle = new SelectList(_invitationsoffersService.selectLanguageStyles().Select( x => new { x.ixLanguageStyle, x.sLanguageStyle }), "ixLanguageStyle", "sLanguageStyle", invitationsoffers.ixLanguageStyle);
			ViewBag.ixResponseType = new SelectList(_invitationsoffersService.selectResponseTypes().Select( x => new { x.ixResponseType, x.sResponseType }), "ixResponseType", "sResponseType", invitationsoffers.ixResponseType);

            return View(invitationsoffers);
        }


        // GET: InvitationsOffers/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_invitationsoffersService.Get(id));
        }

        // POST: InvitationsOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            InvitationsOffersPost invitationsoffers = _invitationsoffersService.GetPost(id);
            invitationsoffers.UserName = User.Identity.Name;
            _invitationsoffersService.Delete(invitationsoffers);
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
            string sInvitationOffer;

            InvitationsOffersPost invitationsoffers;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sInvitationOffer = _invitationsoffersService.Get(nID).sInvitationOffer;
                            if (!_invitationsoffersService.VerifyInvitationOfferDeleteOK(nID, sInvitationOffer).Any())
                            {
                                invitationsoffers = _invitationsoffersService.GetPost(nID);
                                invitationsoffers.UserName = User.Identity.Name;
                                _invitationsoffersService.Delete(invitationsoffers);
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
        public IActionResult VerifyInvitationOffer(long ixInvitationOffer, string sInvitationOffer)
        {
            string validationResponse = "";

            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

