using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IAccusationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixAccusation { get; set; }
		String sAccusation { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sAccusationMade { get; set; }
		String sAdmissionDenial { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		String UserName { get; set; }
    }
}
  

