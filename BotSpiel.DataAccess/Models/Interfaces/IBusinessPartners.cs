using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IBusinessPartners
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixBusinessPartner { get; set; }
		Int64 ixBusinessPartnerEdit { get; set; }
		String sBusinessPartner { get; set; }
		Int64 ixBusinessPartnerType { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixAddress { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		BusinessPartnerTypes BusinessPartnerTypes { get; set; }
		Companies Companies { get; set; }
		Addresses Addresses { get; set; }
    }
}
  

