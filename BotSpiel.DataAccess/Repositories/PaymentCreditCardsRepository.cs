using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PaymentCreditCardsRepository : IPaymentCreditCardsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PaymentCreditCardsDB _context;
  
        public PaymentCreditCardsRepository(PaymentCreditCardsDB context)
        {
            _context = context;
  
        }

        public PaymentCreditCardsPost GetPost(Int64 ixPaymentCreditCard) => _context.PaymentCreditCardsPost.AsNoTracking().Where(x => x.ixPaymentCreditCard == ixPaymentCreditCard).First();
         
		public PaymentCreditCards Get(Int64 ixPaymentCreditCard)
        {
            PaymentCreditCards paymentcreditcards = _context.PaymentCreditCards.AsNoTracking().Where(x => x.ixPaymentCreditCard == ixPaymentCreditCard).First();
            return paymentcreditcards;
        }

        public IQueryable<PaymentCreditCards> Index()
        {
            var paymentcreditcards = _context.PaymentCreditCards.AsNoTracking(); 
            return paymentcreditcards;
        }

        public IQueryable<PaymentCreditCards> IndexDb()
        {
            var paymentcreditcards = _context.PaymentCreditCards.AsNoTracking(); 
            return paymentcreditcards;
        }
        public bool VerifyPaymentCreditCardUnique(Int64 ixPaymentCreditCard, string sPaymentCreditCard)
        {
            if (_context.PaymentCreditCards.AsNoTracking().Where(x => x.sPaymentCreditCard == sPaymentCreditCard).Any() && ixPaymentCreditCard == 0L) return false;
            else if (_context.PaymentCreditCards.AsNoTracking().Where(x => x.sPaymentCreditCard == sPaymentCreditCard && x.ixPaymentCreditCard != ixPaymentCreditCard).Any() && ixPaymentCreditCard != 0L) return false;
            else return true;
        }

        public List<string> VerifyPaymentCreditCardDeleteOK(Int64 ixPaymentCreditCard, string sPaymentCreditCard)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(PaymentCreditCardsPost paymentcreditcardsPost)
		{
            _context.PaymentCreditCardsPost.Add(paymentcreditcardsPost); 
        }

        public void RegisterEdit(PaymentCreditCardsPost paymentcreditcardsPost)
        {
            _context.Entry(paymentcreditcardsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PaymentCreditCardsPost paymentcreditcardsPost)
        {
            _context.PaymentCreditCardsPost.Remove(paymentcreditcardsPost);
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
  

