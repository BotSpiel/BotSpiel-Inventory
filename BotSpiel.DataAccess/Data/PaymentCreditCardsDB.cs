using System;
using System.Data;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using System.Web;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Data
{

    public class PaymentCreditCardsDB : DbContext
    {

        public PaymentCreditCardsDB(DbContextOptions<PaymentCreditCardsDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PaymentCreditCards> PaymentCreditCards { get; set; }
		public DbSet<PaymentCreditCardsPost> PaymentCreditCardsPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentCreditCards>()
                .ToTable("tx_vw_PaymentCreditCards")
                .HasKey(c => new { c.ixPaymentCreditCard });
            modelBuilder.Entity<PaymentCreditCardsPost>()
                .ToTable("tx_vw_PaymentCreditCardsPost")
                .HasKey(c => new { c.ixPaymentCreditCard });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is PaymentCreditCardsPost)).ToList())
            {
                var tx_vw_paymentcreditcardspost = e.Entity as PaymentCreditCardsPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sPaymentCreditCard = cmd.CreateParameter();
                            sPaymentCreditCard.ParameterName = "p0";
                            sPaymentCreditCard.Value = tx_vw_paymentcreditcardspost.sPaymentCreditCard;
                            var sCreditCardType = cmd.CreateParameter();
                            sCreditCardType.ParameterName = "p1";
                            sCreditCardType.Value = tx_vw_paymentcreditcardspost.sCreditCardType;
                            var sFirstName = cmd.CreateParameter();
                            sFirstName.ParameterName = "p2";
                            sFirstName.Value = tx_vw_paymentcreditcardspost.sFirstName;
                            var sLastName = cmd.CreateParameter();
                            sLastName.ParameterName = "p3";
                            sLastName.Value = tx_vw_paymentcreditcardspost.sLastName;
                            var nExpireMonth = cmd.CreateParameter();
                            nExpireMonth.ParameterName = "p4";
                            nExpireMonth.Value = tx_vw_paymentcreditcardspost.nExpireMonth;
                            var nExpireYear = cmd.CreateParameter();
                            nExpireYear.ParameterName = "p5";
                            nExpireYear.Value = tx_vw_paymentcreditcardspost.nExpireYear;
                            var sCvvTwo = cmd.CreateParameter();
                            sCvvTwo.ParameterName = "p6";
                            sCvvTwo.Value = tx_vw_paymentcreditcardspost.sCvvTwo;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p7";
                            UserName.Value = tx_vw_paymentcreditcardspost.UserName;

                            var ixPaymentCreditCard = cmd.CreateParameter();
                            ixPaymentCreditCard.ParameterName = "p8";
                            ixPaymentCreditCard.DbType = DbType.Int64;
                            ixPaymentCreditCard.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreatePaymentCreditCards ");
                            sql.Append("@sPaymentCreditCard = @p0, ");
                            sql.Append("@sCreditCardType = @p1, ");
                            sql.Append("@sFirstName = @p2, ");
                            sql.Append("@sLastName = @p3, ");
                            sql.Append("@nExpireMonth = @p4, ");
                            sql.Append("@nExpireYear = @p5, ");
                            sql.Append("@sCvvTwo = @p6, ");
                            if (tx_vw_paymentcreditcardspost.UserName != null) { sql.Append("@UserName = @p7, "); }  
                            sql.Append("@ixPaymentCreditCard = @p8 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sPaymentCreditCard);
                            cmd.Parameters.Add(sCreditCardType);
                            cmd.Parameters.Add(sFirstName);
                            cmd.Parameters.Add(sLastName);
                            cmd.Parameters.Add(nExpireMonth);
                            cmd.Parameters.Add(nExpireYear);
                            cmd.Parameters.Add(sCvvTwo);
                            if (tx_vw_paymentcreditcardspost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPaymentCreditCard); 
                            cmd.ExecuteNonQuery();
                            tx_vw_paymentcreditcardspost.ixPaymentCreditCard = (Int64)ixPaymentCreditCard.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPaymentCreditCard"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangePaymentCreditCards @ixPaymentCreditCard = @p0, @sPaymentCreditCard = @p1, @sCreditCardType = @p2, @sFirstName = @p3, @sLastName = @p4, @nExpireMonth = @p5, @nExpireYear = @p6, @sCvvTwo = @p7, @UserName = @p8", tx_vw_paymentcreditcardspost.ixPaymentCreditCard, tx_vw_paymentcreditcardspost.sPaymentCreditCard, tx_vw_paymentcreditcardspost.sCreditCardType, tx_vw_paymentcreditcardspost.sFirstName, tx_vw_paymentcreditcardspost.sLastName, tx_vw_paymentcreditcardspost.nExpireMonth, tx_vw_paymentcreditcardspost.nExpireYear, tx_vw_paymentcreditcardspost.sCvvTwo, tx_vw_paymentcreditcardspost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeletePaymentCreditCards @ixPaymentCreditCard = @p0, @sPaymentCreditCard = @p1, @sCreditCardType = @p2, @sFirstName = @p3, @sLastName = @p4, @nExpireMonth = @p5, @nExpireYear = @p6, @sCvvTwo = @p7, @UserName = @p8", tx_vw_paymentcreditcardspost.ixPaymentCreditCard, tx_vw_paymentcreditcardspost.sPaymentCreditCard, tx_vw_paymentcreditcardspost.sCreditCardType, tx_vw_paymentcreditcardspost.sFirstName, tx_vw_paymentcreditcardspost.sLastName, tx_vw_paymentcreditcardspost.nExpireMonth, tx_vw_paymentcreditcardspost.nExpireYear, tx_vw_paymentcreditcardspost.sCvvTwo, tx_vw_paymentcreditcardspost.UserName);
                        e.State = EntityState.Detached;                           
						break;
                }

                ++changes;
            }



            return changes; 
        }

        public void Rollback()
        {
            ChangeTracker.Entries().ToList().ForEach(x =>
            {
                x.State = EntityState.Detached;
            });
        }

    }
}
  

