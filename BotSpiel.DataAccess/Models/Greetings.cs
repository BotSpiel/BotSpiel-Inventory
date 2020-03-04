using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Greetings : IGreetings
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Greetings()
        {
		Languages _Languages = new Languages();
		Languages = _Languages;
		LanguageStyles _LanguageStyles = new LanguageStyles();
		LanguageStyles = _LanguageStyles;
		ResponseTypes _ResponseTypes = new ResponseTypes();
		ResponseTypes = _ResponseTypes;

        }
		[Display(Name = "Greeting ID")]
		public virtual Int64 ixGreeting { get; set; }
		[Display(Name = "Greeting ID")]
		public virtual Int64 ixGreetingEdit { get; set; }
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
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
		[ForeignKey("ixLanguage")]
		public virtual Languages Languages { get; set; }
		[ForeignKey("ixLanguageStyle")]
		public virtual LanguageStyles LanguageStyles { get; set; }
		[ForeignKey("ixResponseType")]
		public virtual ResponseTypes ResponseTypes { get; set; }
    }
}
  

