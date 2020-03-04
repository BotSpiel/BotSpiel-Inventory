using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class PaymentAddressesRepository : IPaymentAddressesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly PaymentAddressesDB _context;
  
        public PaymentAddressesRepository(PaymentAddressesDB context)
        {
            _context = context;
  
        }

        public PaymentAddressesPost GetPost(Int64 ixPaymentAddress) => _context.PaymentAddressesPost.AsNoTracking().Where(x => x.ixPaymentAddress == ixPaymentAddress).First();
         
		public PaymentAddresses Get(Int64 ixPaymentAddress)
        {
            PaymentAddresses paymentaddresses = _context.PaymentAddresses.AsNoTracking().Where(x => x.ixPaymentAddress == ixPaymentAddress).First();
            return paymentaddresses;
        }

        public IQueryable<PaymentAddresses> Index()
        {
            var paymentaddresses = _context.PaymentAddresses.AsNoTracking(); 
            return paymentaddresses;
        }

        public IQueryable<PaymentAddresses> IndexDb()
        {
            var paymentaddresses = _context.PaymentAddresses.AsNoTracking(); 
            return paymentaddresses;
        }
        public bool VerifyPaymentAddressUnique(Int64 ixPaymentAddress, string sPaymentAddress)
        {
            if (_context.PaymentAddresses.AsNoTracking().Where(x => x.sPaymentAddress == sPaymentAddress).Any() && ixPaymentAddress == 0L) return false;
            else if (_context.PaymentAddresses.AsNoTracking().Where(x => x.sPaymentAddress == sPaymentAddress && x.ixPaymentAddress != ixPaymentAddress).Any() && ixPaymentAddress != 0L) return false;
            else return true;
        }

        public List<string> VerifyPaymentAddressDeleteOK(Int64 ixPaymentAddress, string sPaymentAddress)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(PaymentAddressesPost paymentaddressesPost)
		{
            _context.PaymentAddressesPost.Add(paymentaddressesPost); 
        }

        public void RegisterEdit(PaymentAddressesPost paymentaddressesPost)
        {
            _context.Entry(paymentaddressesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(PaymentAddressesPost paymentaddressesPost)
        {
            _context.PaymentAddressesPost.Remove(paymentaddressesPost);
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
  

