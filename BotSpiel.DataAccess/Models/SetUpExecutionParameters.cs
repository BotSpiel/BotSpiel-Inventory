using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class SetUpExecutionParameters : ISetUpExecutionParameters
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public SetUpExecutionParameters()
        {
		Facilities _Facilities = new Facilities();
		Facilities = _Facilities;
		Companies _Companies = new Companies();
		Companies = _Companies;
		FacilityWorkAreas _FacilityWorkAreas = new FacilityWorkAreas();
		FacilityWorkAreas = _FacilityWorkAreas;

        }
		[Display(Name = "Set Up Execution Parameter ID")]
		public virtual Int64 ixSetUpExecutionParameter { get; set; }
		[Display(Name = "Set Up Execution Parameter ID")]
		public virtual Int64 ixSetUpExecutionParameterEdit { get; set; }
		[Display(Name = "Set Up Execution Parameter")]
		public virtual String sSetUpExecutionParameter { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Facility Work Area ID")]
		public virtual Int64 ixFacilityWorkArea { get; set; }
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
		[ForeignKey("ixFacility")]
		public virtual Facilities Facilities { get; set; }
		[ForeignKey("ixCompany")]
		public virtual Companies Companies { get; set; }
		[ForeignKey("ixFacilityWorkArea")]
		public virtual FacilityWorkAreas FacilityWorkAreas { get; set; }
    }
}
  

