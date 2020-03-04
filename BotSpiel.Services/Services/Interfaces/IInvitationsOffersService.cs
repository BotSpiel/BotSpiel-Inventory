using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IInvitationsOffersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        InvitationsOffersPost GetPost(Int64 ixInvitationOffer);        
		InvitationsOffers Get(Int64 ixInvitationOffer);
        IQueryable<InvitationsOffers> Index();
        IQueryable<InvitationsOffers> IndexDb();
       IQueryable<Languages> selectLanguages();
        IQueryable<LanguageStyles> selectLanguageStyles();
        IQueryable<ResponseTypes> selectResponseTypes();
       IQueryable<Languages> LanguagesDb();
        IQueryable<LanguageStyles> LanguageStylesDb();
        IQueryable<ResponseTypes> ResponseTypesDb();
        List<string> VerifyInvitationOfferDeleteOK(Int64 ixInvitationOffer, string sInvitationOffer);

        Task<Int64> Create(InvitationsOffersPost invitationsoffersPost);
        Task Edit(InvitationsOffersPost invitationsoffersPost);
        Task Delete(InvitationsOffersPost invitationsoffersPost);
    }
}
  

