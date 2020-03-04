using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestsForAction : IRequestsForAction
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public RequestsForAction()
        {
		Languages _Languages = new Languages();
		Languages = _Languages;
		LanguageStyles _LanguageStyles = new LanguageStyles();
		LanguageStyles = _LanguageStyles;

        }
		[Display(Name = "Request For Action ID")]
		public virtual Int64 ixRequestForAction { get; set; }
		[Display(Name = "Request For Action ID")]
		public virtual Int64 ixRequestForActionEdit { get; set; }
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
    }
}
  

