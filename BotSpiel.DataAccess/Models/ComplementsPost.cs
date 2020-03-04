using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class ComplementsPost : IComplementsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Complement ID")]
		public virtual Int64 ixComplement { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Complement")]
		public virtual String sComplement { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Required]
		[Display(Name = "Complement Made")]
		public virtual String sComplementMade { get; set; }
		[Required]
		[Display(Name = "Complement Accepted")]
		public virtual String sComplementAccepted { get; set; }
		[Required]
		[Display(Name = "Response Type ID")]
		public virtual Int64 ixResponseType { get; set; }
		[Required]
		[Display(Name = "Active")]
		public virtual Boolean bActive { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

