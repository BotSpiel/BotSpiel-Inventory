using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDocumentMessageTypesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixDocumentMessageType { get; set; }
		String sDocumentMessageType { get; set; }
		String sDocumentMessageTypeCode { get; set; }
		String UserName { get; set; }
    }
}
  

