using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IAccusations
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixAccusation { get; set; }
		Int64 ixAccusationEdit { get; set; }
		String sAccusation { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sAccusationMade { get; set; }
		String sAdmissionDenial { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Languages Languages { get; set; }
		LanguageStyles LanguageStyles { get; set; }
		ResponseTypes ResponseTypes { get; set; }
    }
}
  

