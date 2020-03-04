using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PaymentsRepository : IPaymentsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PaymentsDB _context;
  
        public PaymentsRepository(PaymentsDB context)
        {
            _context = context;
  
        }

        public PaymentsPost GetPost(Int64 ixPayment) => _context.PaymentsPost.AsNoTracking().Where(x => x.ixPayment == ixPayment).First();
         
		public Payments Get(Int64 ixPayment)
        {
            Payments payments = _context.Payments.AsNoTracking().Where(x => x.ixPayment == ixPayment).First();
            payments.Invoices = _context.Invoices.Find(payments.ixInvoice);

            return payments;
        }

        public IQueryable<Payments> Index()
        {
            var payments = _context.Payments.Include(a => a.Invoices).AsNoTracking(); 
            return payments;
        }

        public IQueryable<Payments> IndexDb()
        {
            var payments = _context.Payments.Include(a => a.Invoices).AsNoTracking(); 
            return payments;
        }
       public IQueryable<Invoices> selectInvoices()
        {
            List<Invoices> invoices = new List<Invoices>();
            _context.Invoices.Include(a => a.Purchases).AsNoTracking()
                .ToList()
                .ForEach(x => invoices.Add(x));
            return invoices.AsQueryable();
        }
       public IQueryable<Invoices> InvoicesDb()
        {
            List<Invoices> invoices = new List<Invoices>();
            _context.Invoices.Include(a => a.Purchases).AsNoTracking()
                .ToList()
                .ForEach(x => invoices.Add(x));
            return invoices.AsQueryable();
        }
        public bool VerifyPaymentUnique(Int64 ixPayment, string sPayment)
        {
            if (_context.Payments.AsNoTracking().Where(x => x.sPayment == sPayment).Any() && ixPayment == 0L) return false;
            else if (_context.Payments.AsNoTracking().Where(x => x.sPayment == sPayment && x.ixPayment != ixPayment).Any() && ixPayment != 0L) return false;
            else return true;
        }

        public List<string> VerifyPaymentDeleteOK(Int64 ixPayment, string sPayment)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(PaymentsPost paymentsPost)
		{
            _context.PaymentsPost.Add(paymentsPost); 
        }

        public void RegisterEdit(PaymentsPost paymentsPost)
        {
            _context.Entry(paymentsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PaymentsPost paymentsPost)
        {
            _context.PaymentsPost.Remove(paymentsPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

