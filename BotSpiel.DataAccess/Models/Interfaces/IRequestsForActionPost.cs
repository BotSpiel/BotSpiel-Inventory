using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IRequestsForActionPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixRequestForAction { get; set; }
		String sRequestForAction { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sActionRequest { get; set; }
		String sModule { get; set; }
		String sEntity { get; set; }
		String sEntityIntent { get; set; }
		String UserName { get; set; }
    }
}
  

