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

    public class PurchaseTextMessagesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPurchaseTextMessagesService _purchasetextmessagesService;

        public PurchaseTextMessagesController(IPurchaseTextMessagesService purchasetextmessagesService )
        {
            _purchasetextmessagesService = purchasetextmessagesService;
        }

        // GET: PurchaseTextMessages
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var purchasetextmessages = _purchasetextmessagesService.Index();
            return View(purchasetextmessages.ToList());
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
            var purchasetextmessages = _purchasetextmessagesService.Index();
            return PartialView("IndexGrid", purchasetextmessages.ToList());
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
                IGrid<PurchaseTextMessages> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PurchaseTextMessages> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPurchaseTextMessages.xlsx");
            }
        }

        private IGrid<PurchaseTextMessages> CreateExportableGrid()
        {
            IGrid<PurchaseTextMessages> grid = new Grid<PurchaseTextMessages>(_purchasetextmessagesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPurchaseTextMessage).Titled("Purchase Text Message").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.Purchases.sPurchase).Titled("Purchase").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.SendTextMessages.sSendTextMessage).Titled("Send Text Message").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PurchaseTextMessages>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PurchaseTextMessages/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_purchasetextmessagesService.Get(id));
        }

        // GET: PurchaseTextMessages/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
			ViewBag.ixPurchase = new SelectList(_purchasetextmessagesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");
			ViewBag.ixSendTextMessage = new SelectList(_purchasetextmessagesService.selectSendTextMessages().Select( x => new { x.ixSendTextMessage, x.sSendTextMessage }), "ixSendTextMessage", "sSendTextMessage");

            return View();
        }

        // POST: PurchaseTextMessages/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPurchaseTextMessage,sPurchaseTextMessage,ixPurchase,ixSendTextMessage")] PurchaseTextMessagesPost purchasetextmessages)
        {
            if (ModelState.IsValid)
            {
                purchasetextmessages.UserName = User.Identity.Name;
                _purchasetextmessagesService.Create(purchasetextmessages);
                return RedirectToAction("Index");
            }
			ViewBag.ixPurchase = new SelectList(_purchasetextmessagesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase");
			ViewBag.ixSendTextMessage = new SelectList(_purchasetextmessagesService.selectSendTextMessages().Select( x => new { x.ixSendTextMessage, x.sSendTextMessage }), "ixSendTextMessage", "sSendTextMessage");

            return View(purchasetextmessages);
        }

        // GET: PurchaseTextMessages/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PurchaseTextMessagesPost purchasetextmessages = _purchasetextmessagesService.GetPost(id);
            if (purchasetextmessages == null)
            {
                return NotFound();
            }
			ViewBag.ixPurchase = new SelectList(_purchasetextmessagesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", purchasetextmessages.ixPurchase);
			ViewBag.ixSendTextMessage = new SelectList(_purchasetextmessagesService.selectSendTextMessages().Select( x => new { x.ixSendTextMessage, x.sSendTextMessage }), "ixSendTextMessage", "sSendTextMessage", purchasetextmessages.ixSendTextMessage);

            return View(purchasetextmessages);
        }

        // POST: PurchaseTextMessages/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPurchaseTextMessage,sPurchaseTextMessage,ixPurchase,ixSendTextMessage")] PurchaseTextMessagesPost purchasetextmessages)
        {
            if (ModelState.IsValid)
            {
                purchasetextmessages.UserName = User.Identity.Name;
                _purchasetextmessagesService.Edit(purchasetextmessages);
                return RedirectToAction("Index");
            }
			ViewBag.ixPurchase = new SelectList(_purchasetextmessagesService.selectPurchases().Select( x => new { x.ixPurchase, x.sPurchase }), "ixPurchase", "sPurchase", purchasetextmessages.ixPurchase);
			ViewBag.ixSendTextMessage = new SelectList(_purchasetextmessagesService.selectSendTextMessages().Select( x => new { x.ixSendTextMessage, x.sSendTextMessage }), "ixSendTextMessage", "sSendTextMessage", purchasetextmessages.ixSendTextMessage);

            return View(purchasetextmessages);
        }


        // GET: PurchaseTextMessages/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_purchasetextmessagesService.Get(id));
        }

        // POST: PurchaseTextMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PurchaseTextMessagesPost purchasetextmessages = _purchasetextmessagesService.GetPost(id);
            purchasetextmessages.UserName = User.Identity.Name;
            _purchasetextmessagesService.Delete(purchasetextmessages);
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
            string sPurchaseTextMessage;

            PurchaseTextMessagesPost purchasetextmessages;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPurchaseTextMessage = _purchasetextmessagesService.Get(nID).sPurchaseTextMessage;
                            if (!_purchasetextmessagesService.VerifyPurchaseTextMessageDeleteOK(nID, sPurchaseTextMessage).Any())
                            {
                                purchasetextmessages = _purchasetextmessagesService.GetPost(nID);
                                purchasetextmessages.UserName = User.Identity.Name;
                                _purchasetextmessagesService.Delete(purchasetextmessages);
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
        public IActionResult VerifyPurchaseTextMessage(long ixPurchaseTextMessage, string sPurchaseTextMessage)
        {
            string validationResponse = "";

            if (!_purchasetextmessagesService.VerifyPurchaseTextMessageUnique(ixPurchaseTextMessage, sPurchaseTextMessage))
            {
                validationResponse = $"PurchaseTextMessage {sPurchaseTextMessage} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

