using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ITaxesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixTax { get; set; }
		String sTax { get; set; }
		Int64 ixCountry { get; set; }
		Int64 ixCountrySubDivision { get; set; }
		Decimal nRate { get; set; }
		String UserName { get; set; }
    }
}
  

