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

    public class PaymentAddressesDB : DbContext
    {

        public PaymentAddressesDB(DbContextOptions<PaymentAddressesDB> options)
            : base(options)
        {
        }

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		public DbSet<PaymentAddresses> PaymentAddresses { get; set; }
		public DbSet<PaymentAddressesPost> PaymentAddressesPost { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentAddresses>()
                .ToTable("tx_vw_PaymentAddresses")
                .HasKey(c => new { c.ixPaymentAddress });
            modelBuilder.Entity<PaymentAddressesPost>()
                .ToTable("tx_vw_PaymentAddressesPost")
                .HasKey(c => new { c.ixPaymentAddress });
        }

        public override int SaveChanges()
        {
            var changes = 0;
            foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.State != EntityState.Detached) && (e.Entity is PaymentAddressesPost)).ToList())
            {
                var tx_vw_paymentaddressespost = e.Entity as PaymentAddressesPost;
                switch (e.State)
                {
                    case EntityState.Added:
                        var con = Database.GetDbConnection();
                        using (var cmd = con.CreateCommand())
                        {
                            con.Open();
                            var sStreetAndNumberOrPostOfficeBoxOne = cmd.CreateParameter();
                            sStreetAndNumberOrPostOfficeBoxOne.ParameterName = "p0";
                            sStreetAndNumberOrPostOfficeBoxOne.Value = tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxOne;
                            var sStreetAndNumberOrPostOfficeBoxTwo = cmd.CreateParameter();
                            sStreetAndNumberOrPostOfficeBoxTwo.ParameterName = "p1";
                            sStreetAndNumberOrPostOfficeBoxTwo.Value = tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxTwo;
                            var sCityOrSuburb = cmd.CreateParameter();
                            sCityOrSuburb.ParameterName = "p2";
                            sCityOrSuburb.Value = tx_vw_paymentaddressespost.sCityOrSuburb;
                            var sCountrySubDivisionCode = cmd.CreateParameter();
                            sCountrySubDivisionCode.ParameterName = "p3";
                            sCountrySubDivisionCode.Value = tx_vw_paymentaddressespost.sCountrySubDivisionCode;
                            var sZipOrPostCode = cmd.CreateParameter();
                            sZipOrPostCode.ParameterName = "p4";
                            sZipOrPostCode.Value = tx_vw_paymentaddressespost.sZipOrPostCode;
                            var sCountryCode = cmd.CreateParameter();
                            sCountryCode.ParameterName = "p5";
                            sCountryCode.Value = tx_vw_paymentaddressespost.sCountryCode;
                            var UserName = cmd.CreateParameter();
                            UserName.ParameterName = "p6";
                            UserName.Value = tx_vw_paymentaddressespost.UserName;

                            var ixPaymentAddress = cmd.CreateParameter();
                            ixPaymentAddress.ParameterName = "p7";
                            ixPaymentAddress.DbType = DbType.Int64;
                            ixPaymentAddress.Direction = ParameterDirection.Output;

                            var sql = new StringBuilder();
                            sql.Append(@"exec dbo.tx_sp_CreatePaymentAddresses ");
                            sql.Append("@sStreetAndNumberOrPostOfficeBoxOne = @p0, ");
                            if (tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxTwo != null) { sql.Append("@sStreetAndNumberOrPostOfficeBoxTwo = @p1, "); }  
                            sql.Append("@sCityOrSuburb = @p2, ");
                            sql.Append("@sCountrySubDivisionCode = @p3, ");
                            sql.Append("@sZipOrPostCode = @p4, ");
                            sql.Append("@sCountryCode = @p5, ");
                            if (tx_vw_paymentaddressespost.UserName != null) { sql.Append("@UserName = @p6, "); }  
                            sql.Append("@ixPaymentAddress = @p7 output "); 
                            cmd.CommandText = sql.ToString();

                            cmd.Parameters.Add(sStreetAndNumberOrPostOfficeBoxOne);
                            if (tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxTwo != null) { cmd.Parameters.Add(sStreetAndNumberOrPostOfficeBoxTwo); }
                            cmd.Parameters.Add(sCityOrSuburb);
                            cmd.Parameters.Add(sCountrySubDivisionCode);
                            cmd.Parameters.Add(sZipOrPostCode);
                            cmd.Parameters.Add(sCountryCode);
                            if (tx_vw_paymentaddressespost.UserName != null) { cmd.Parameters.Add(UserName); }

                            cmd.Parameters.Add(ixPaymentAddress); 
                            cmd.ExecuteNonQuery();
                            tx_vw_paymentaddressespost.ixPaymentAddress = (Int64)ixPaymentAddress.Value;
                            con.Close();
                        }
						e.GetInfrastructure().MarkAsTemporary(e.Metadata.FindProperty("ixPaymentAddress"), false);
						e.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_ChangePaymentAddresses @ixPaymentAddress = @p0, @sStreetAndNumberOrPostOfficeBoxOne = @p1, @sStreetAndNumberOrPostOfficeBoxTwo = @p2, @sCityOrSuburb = @p3, @sCountrySubDivisionCode = @p4, @sZipOrPostCode = @p5, @sCountryCode = @p6, @UserName = @p7", tx_vw_paymentaddressespost.ixPaymentAddress, tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxOne, tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxTwo, tx_vw_paymentaddressespost.sCityOrSuburb, tx_vw_paymentaddressespost.sCountrySubDivisionCode, tx_vw_paymentaddressespost.sZipOrPostCode, tx_vw_paymentaddressespost.sCountryCode, tx_vw_paymentaddressespost.UserName);
                        e.State = EntityState.Detached;                            
						break;

                    case EntityState.Deleted:
                        Database.ExecuteSqlCommand("exec dbo.tx_sp_DeletePaymentAddresses @ixPaymentAddress = @p0, @sStreetAndNumberOrPostOfficeBoxOne = @p1, @sStreetAndNumberOrPostOfficeBoxTwo = @p2, @sCityOrSuburb = @p3, @sCountrySubDivisionCode = @p4, @sZipOrPostCode = @p5, @sCountryCode = @p6, @UserName = @p7", tx_vw_paymentaddressespost.ixPaymentAddress, tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxOne, tx_vw_paymentaddressespost.sStreetAndNumberOrPostOfficeBoxTwo, tx_vw_paymentaddressespost.sCityOrSuburb, tx_vw_paymentaddressespost.sCountrySubDivisionCode, tx_vw_paymentaddressespost.sZipOrPostCode, tx_vw_paymentaddressespost.sCountryCode, tx_vw_paymentaddressespost.UserName);
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
  

