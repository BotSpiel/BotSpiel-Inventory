using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IFarewells
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixFarewell { get; set; }
		Int64 ixFarewellEdit { get; set; }
		String sFarewell { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sFarewellOffered { get; set; }
		String sFarewellResponse { get; set; }
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
  

