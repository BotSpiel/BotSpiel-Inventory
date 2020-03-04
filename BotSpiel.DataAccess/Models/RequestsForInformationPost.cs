using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestsForInformationPost : IRequestsForInformationPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Request For Information ID")]
		public virtual Int64 ixRequestForInformation { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Request For Information")]
		public virtual String sRequestForInformation { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Required]
		[Display(Name = "Topic ID")]
		public virtual Int64 ixTopic { get; set; }
		[Required]
		[Display(Name = "Information Request")]
		public virtual String sInformationRequest { get; set; }
		[Required]
		[Display(Name = "Information Request Response")]
		public virtual String sInformationRequestResponse { get; set; }
		[Required]
		[Display(Name = "Response Type ID")]
		public virtual Int64 ixResponseType { get; set; }
		[Required]
		[Display(Name = "Active")]
		public virtual Boolean bActive { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

