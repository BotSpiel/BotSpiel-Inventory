using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Questions : IQuestions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Questions()
        {
		LanguageStyles _LanguageStylesFKDiffLanguage = new LanguageStyles();
		LanguageStylesFKDiffLanguage = _LanguageStylesFKDiffLanguage;
		LanguageStyles _LanguageStyles = new LanguageStyles();
		LanguageStyles = _LanguageStyles;
		Topics _Topics = new Topics();
		Topics = _Topics;
		ResponseTypes _ResponseTypes = new ResponseTypes();
		ResponseTypes = _ResponseTypes;

        }
		[Display(Name = "Question ID")]
		public virtual Int64 ixQuestion { get; set; }
		[Display(Name = "Question ID")]
		public virtual Int64 ixQuestionEdit { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Question")]
		public virtual String sQuestion { get; set; }
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
		[Display(Name = "Ask")]
		public virtual String sAsk { get; set; }
		[Required]
		[Display(Name = "Answer")]
		public virtual String sAnswer { get; set; }
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
		public virtual LanguageStyles LanguageStylesFKDiffLanguage { get; set; }
		[ForeignKey("ixLanguageStyle")]
		public virtual LanguageStyles LanguageStyles { get; set; }
		[ForeignKey("ixTopic")]
		public virtual Topics Topics { get; set; }
		[ForeignKey("ixResponseType")]
		public virtual ResponseTypes ResponseTypes { get; set; }
    }
}
  

