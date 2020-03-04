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

    public class PaymentAddressesController : Controller
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        private readonly IPaymentAddressesService _paymentaddressesService;

        public PaymentAddressesController(IPaymentAddressesService paymentaddressesService )
        {
            _paymentaddressesService = paymentaddressesService;
        }

        // GET: PaymentAddresses
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var paymentaddresses = _paymentaddressesService.Index();
            return View(paymentaddresses.ToList());
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
            var paymentaddresses = _paymentaddressesService.Index();
            return PartialView("IndexGrid", paymentaddresses.ToList());
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
                IGrid<PaymentAddresses> grid = CreateExportableGrid();
                ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                foreach (IGridColumn column in grid.Columns)
                {
                    sheet.Cells[1, col].Value = column.Title;
                    sheet.Column(col++).Width = 18;
                }

                foreach (IGridRow<PaymentAddresses> gridRow in grid.Rows)
                {
                    col = 1;
                    foreach (IGridColumn column in grid.Columns)
                        sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                    row++;
                }

                return File(package.GetAsByteArray(), "application/unknown", "ExportPaymentAddresses.xlsx");
            }
        }

        private IGrid<PaymentAddresses> CreateExportableGrid()
        {
            IGrid<PaymentAddresses> grid = new Grid<PaymentAddresses>(_paymentaddressesService.Index().ToList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;
				grid.Columns.Add(model => model.sPaymentAddress).Titled("Payment Address").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sStreetAndNumberOrPostOfficeBoxOne).Titled("Street And Number Or Post Office Box One").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sStreetAndNumberOrPostOfficeBoxTwo).Titled("Street And Number Or Post Office Box Two").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCityOrSuburb).Titled("City Or Suburb").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCountrySubDivisionCode).Titled("Country Sub Division Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sZipOrPostCode).Titled("Zip Or Post Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sCountryCode).Titled("Country Code").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.dtCreatedAt).Titled("Created At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.dtChangedAt).Titled("Changed At").Sortable(true).Filterable(true);
				grid.Columns.Add(model => model.sCreatedBy).Titled("Created By").Sortable(true).Filterable(true).MultiFilterable(true);
				grid.Columns.Add(model => model.sChangedBy).Titled("Changed By").Sortable(true).Filterable(true).MultiFilterable(true);

            grid.Pager = new GridPager<PaymentAddresses>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<Int32, String> { { 0, "All" }, { 5, "5" }, { 10, "10" }, { 15, "15" }, { 20, "20" }, { 40, "40" }, { 100, "100" } };
            grid.Pager.ShowPageSizes = true;
            grid.Pager.RowsPerPage = 20;

            return grid;
        }


        // GET: PaymentAddresses/Details/1
        [Authorize]
        public ActionResult Details(long id)
        {
            return View(_paymentaddressesService.Get(id));
        }

        // GET: PaymentAddresses/Create
        
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: PaymentAddresses/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ixPaymentAddress,sPaymentAddress,sStreetAndNumberOrPostOfficeBoxOne,sStreetAndNumberOrPostOfficeBoxTwo,sCityOrSuburb,sCountrySubDivisionCode,sZipOrPostCode,sCountryCode")] PaymentAddressesPost paymentaddresses)
        {
            if (ModelState.IsValid)
            {
                paymentaddresses.UserName = User.Identity.Name;
                _paymentaddressesService.Create(paymentaddresses);
                return RedirectToAction("Index");
            }

            return View(paymentaddresses);
        }

        // GET: PaymentAddresses/Edit/1
        [Authorize]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            PaymentAddressesPost paymentaddresses = _paymentaddressesService.GetPost(id);
            if (paymentaddresses == null)
            {
                return NotFound();
            }

            return View(paymentaddresses);
        }

        // POST: PaymentAddresses/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ixPaymentAddress,sPaymentAddress,sStreetAndNumberOrPostOfficeBoxOne,sStreetAndNumberOrPostOfficeBoxTwo,sCityOrSuburb,sCountrySubDivisionCode,sZipOrPostCode,sCountryCode")] PaymentAddressesPost paymentaddresses)
        {
            if (ModelState.IsValid)
            {
                paymentaddresses.UserName = User.Identity.Name;
                _paymentaddressesService.Edit(paymentaddresses);
                return RedirectToAction("Index");
            }

            return View(paymentaddresses);
        }


        // GET: PaymentAddresses/Delete/1
        [Authorize]
        [HttpGet]
        public ActionResult Delete(long id)
        {
            return View(_paymentaddressesService.Get(id));
        }

        // POST: PaymentAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PaymentAddressesPost paymentaddresses = _paymentaddressesService.GetPost(id);
            paymentaddresses.UserName = User.Identity.Name;
            _paymentaddressesService.Delete(paymentaddresses);
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
            string sPaymentAddress;

            PaymentAddressesPost paymentaddresses;

            sIDs.ToList()
                .ForEach(s =>
                    {
                        if (long.TryParse(s, out nID))
                        {
                            sPaymentAddress = _paymentaddressesService.Get(nID).sPaymentAddress;
                            if (!_paymentaddressesService.VerifyPaymentAddressDeleteOK(nID, sPaymentAddress).Any())
                            {
                                paymentaddresses = _paymentaddressesService.GetPost(nID);
                                paymentaddresses.UserName = User.Identity.Name;
                                _paymentaddressesService.Delete(paymentaddresses);
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
        public IActionResult VerifyPaymentAddress(long ixPaymentAddress, string sPaymentAddress)
        {
            string validationResponse = "";

            if (!_paymentaddressesService.VerifyPaymentAddressUnique(ixPaymentAddress, sPaymentAddress))
            {
                validationResponse = $"PaymentAddress {sPaymentAddress} already exists.";
            }
            if (validationResponse != "")
            return Json(validationResponse);
            else
            return Json(true);
        }



    }
}
 

