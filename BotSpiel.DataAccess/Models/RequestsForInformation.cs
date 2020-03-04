using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestsForInformation : IRequestsForInformation
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public RequestsForInformation()
        {
		Languages _Languages = new Languages();
		Languages = _Languages;
		LanguageStyles _LanguageStyles = new LanguageStyles();
		LanguageStyles = _LanguageStyles;
		Topics _Topics = new Topics();
		Topics = _Topics;
		ResponseTypes _ResponseTypes = new ResponseTypes();
		ResponseTypes = _ResponseTypes;

        }
		[Display(Name = "Request For Information ID")]
		public virtual Int64 ixRequestForInformation { get; set; }
		[Display(Name = "Request For Information ID")]
		public virtual Int64 ixRequestForInformationEdit { get; set; }
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
		[ForeignKey("ixTopic")]
		public virtual Topics Topics { get; set; }
		[ForeignKey("ixResponseType")]
		public virtual ResponseTypes ResponseTypes { get; set; }
    }
}
  

