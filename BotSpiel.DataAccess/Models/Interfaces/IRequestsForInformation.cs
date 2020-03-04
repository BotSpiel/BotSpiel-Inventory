using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IRequestsForInformation
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixRequestForInformation { get; set; }
		Int64 ixRequestForInformationEdit { get; set; }
		String sRequestForInformation { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		Int64 ixTopic { get; set; }
		String sInformationRequest { get; set; }
		String sInformationRequestResponse { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Languages Languages { get; set; }
		LanguageStyles LanguageStyles { get; set; }
		Topics Topics { get; set; }
		ResponseTypes ResponseTypes { get; set; }
    }
}
  

