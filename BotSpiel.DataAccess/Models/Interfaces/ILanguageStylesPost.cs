using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ILanguageStylesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixLanguageStyle { get; set; }
		String sLanguageStyle { get; set; }
		String UserName { get; set; }
    }
}
  

