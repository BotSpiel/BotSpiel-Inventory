using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InvitationsOffersPost : IInvitationsOffersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Invitation Offer ID")]
		public virtual Int64 ixInvitationOffer { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Invitation Offer")]
		public virtual String sInvitationOffer { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Required]
		[Display(Name = "Invitation Offered")]
		public virtual String sInvitationOffered { get; set; }
		[Required]
		[Display(Name = "Accept Decline")]
		public virtual String sAcceptDecline { get; set; }
		[Required]
		[Display(Name = "Response Type ID")]
		public virtual Int64 ixResponseType { get; set; }
		[Required]
		[Display(Name = "Active")]
		public virtual Boolean bActive { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

