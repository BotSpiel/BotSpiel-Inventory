using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class GreetingsPost : IGreetingsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Greeting ID")]
		public virtual Int64 ixGreeting { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Greeting")]
		public virtual String sGreeting { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[StringLength(4000)]
		[Required]
		[Display(Name = "Greeting Offered")]
		public virtual String sGreetingOffered { get; set; }
		[StringLength(4000)]
		[Required]
		[Display(Name = "Greeting Response")]
		public virtual String sGreetingResponse { get; set; }
		[Required]
		[Display(Name = "Response Type ID")]
		public virtual Int64 ixResponseType { get; set; }
		[Required]
		[Display(Name = "Active")]
		public virtual Boolean bActive { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

