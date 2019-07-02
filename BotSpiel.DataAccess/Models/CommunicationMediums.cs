using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CommunicationMediums : ICommunicationMediums
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Communication Medium ID")]
		public virtual Int64 ixCommunicationMedium { get; set; }
		[Display(Name = "Communication Medium ID")]
		public virtual Int64 ixCommunicationMediumEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Communication Medium")]
		public virtual String sCommunicationMedium { get; set; }
		[StringLength(30)]
		[Required]
		[Display(Name = "Communication Medium Code")]
		public virtual String sCommunicationMediumCode { get; set; }
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
    }
}
  

