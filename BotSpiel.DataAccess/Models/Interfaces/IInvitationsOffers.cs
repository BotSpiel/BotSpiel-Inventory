using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInvitationsOffers
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixInvitationOffer { get; set; }
		Int64 ixInvitationOfferEdit { get; set; }
		String sInvitationOffer { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sInvitationOffered { get; set; }
		String sAcceptDecline { get; set; }
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
  

