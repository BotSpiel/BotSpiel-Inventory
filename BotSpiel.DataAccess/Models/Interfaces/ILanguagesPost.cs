using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ILanguagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixLanguage { get; set; }
		String sLanguage { get; set; }
		String sLanguageCode { get; set; }
		String UserName { get; set; }
    }
}
  

