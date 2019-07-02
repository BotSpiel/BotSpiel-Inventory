using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IMessageResponseTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixMessageResponseType { get; set; }
		String sMessageResponseType { get; set; }
		String sMessageResponseTypeCode { get; set; }
		String UserName { get; set; }
    }
}
  

