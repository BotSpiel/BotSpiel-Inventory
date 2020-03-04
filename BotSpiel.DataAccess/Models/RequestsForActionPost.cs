using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestsForActionPost : IRequestsForActionPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Request For Action ID")]
		public virtual Int64 ixRequestForAction { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Request For Action")]
		public virtual String sRequestForAction { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Required]
		[Display(Name = "Action Request")]
		public virtual String sActionRequest { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Module")]
		public virtual String sModule { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Entity")]
		public virtual String sEntity { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Entity Intent")]
		public virtual String sEntityIntent { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

