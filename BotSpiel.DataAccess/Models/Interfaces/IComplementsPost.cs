using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IComplementsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixComplement { get; set; }
		String sComplement { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sComplementMade { get; set; }
		String sComplementAccepted { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		String UserName { get; set; }
    }
}
  

