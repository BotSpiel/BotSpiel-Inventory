using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IBusinessPartnersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixBusinessPartner { get; set; }
		String sBusinessPartner { get; set; }
		Int64 ixBusinessPartnerType { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixAddress { get; set; }
		String UserName { get; set; }
    }
}
  

