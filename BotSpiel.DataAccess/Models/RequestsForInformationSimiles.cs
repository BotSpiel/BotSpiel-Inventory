using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestsForInformationSimiles : IRequestsForInformationSimiles
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public RequestsForInformationSimiles()
        {
		RequestsForInformation _RequestsForInformation = new RequestsForInformation();
		RequestsForInformation = _RequestsForInformation;

        }
		[Display(Name = "Requests For Information Simile ID")]
		public virtual Int64 ixRequestsForInformationSimile { get; set; }
		[Display(Name = "Requests For Information Simile ID")]
		public virtual Int64 ixRequestsForInformationSimileEdit { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Requests For Information Simile")]
		public virtual String sRequestsForInformationSimile { get; set; }
		[Required]
		[Display(Name = "Request For Information ID")]
		public virtual Int64 ixRequestForInformation { get; set; }
		[Required]
		[Display(Name = "Requests For Information Simile Text")]
		public virtual String sRequestsForInformationSimileText { get; set; }
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
		[ForeignKey("ixRequestForInformation")]
		public virtual RequestsForInformation RequestsForInformation { get; set; }
    }
}
  

