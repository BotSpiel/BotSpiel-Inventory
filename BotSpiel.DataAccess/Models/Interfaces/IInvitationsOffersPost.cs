using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvitationsOffersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInvitationOffer { get; set; }
		String sInvitationOffer { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sInvitationOffered { get; set; }
		String sAcceptDecline { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		String UserName { get; set; }
    }
}
  

