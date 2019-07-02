using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class BusinessPartners : IBusinessPartners
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public BusinessPartners()
        {
		BusinessPartnerTypes _BusinessPartnerTypes = new BusinessPartnerTypes();
		BusinessPartnerTypes = _BusinessPartnerTypes;
		Companies _Companies = new Companies();
		Companies = _Companies;
		Addresses _Addresses = new Addresses();
		Addresses = _Addresses;

        }
		[Display(Name = "Business Partner ID")]
		public virtual Int64 ixBusinessPartner { get; set; }
		[Display(Name = "Business Partner ID")]
		public virtual Int64 ixBusinessPartnerEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Business Partner")]
		public virtual String sBusinessPartner { get; set; }
		[Required]
		[Display(Name = "Business Partner Type ID")]
		public virtual Int64 ixBusinessPartnerType { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Address ID")]
		public virtual Int64 ixAddress { get; set; }
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
		[ForeignKey("ixBusinessPartnerType")]
		public virtual BusinessPartnerTypes BusinessPartnerTypes { get; set; }
		[ForeignKey("ixCompany")]
		public virtual Companies Companies { get; set; }
		[ForeignKey("ixAddress")]
		public virtual Addresses Addresses { get; set; }
    }
}
  

