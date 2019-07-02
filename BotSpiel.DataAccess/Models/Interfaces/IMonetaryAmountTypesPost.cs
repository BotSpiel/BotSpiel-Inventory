using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMonetaryAmountTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixMonetaryAmountType { get; set; }
		String sMonetaryAmountType { get; set; }
		String sMonetaryAmountTypeCode { get; set; }
		String UserName { get; set; }
    }
}
  

