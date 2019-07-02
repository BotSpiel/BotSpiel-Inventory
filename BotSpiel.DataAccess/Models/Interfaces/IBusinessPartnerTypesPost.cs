using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IBusinessPartnerTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixBusinessPartnerType { get; set; }
		String sBusinessPartnerType { get; set; }
		String UserName { get; set; }
    }
}
  

