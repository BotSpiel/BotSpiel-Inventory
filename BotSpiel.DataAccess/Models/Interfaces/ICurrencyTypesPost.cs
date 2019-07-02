using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICurrencyTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixCurrencyType { get; set; }
		String sCurrencyType { get; set; }
		String sCurrencyTypeCode { get; set; }
		String UserName { get; set; }
    }
}
  

