using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IRequestsForInformationPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixRequestForInformation { get; set; }
		String sRequestForInformation { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		Int64 ixTopic { get; set; }
		String sInformationRequest { get; set; }
		String sInformationRequestResponse { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		String UserName { get; set; }
    }
}
  

