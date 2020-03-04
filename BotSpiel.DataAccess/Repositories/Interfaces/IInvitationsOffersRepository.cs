using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IInvitationsOffersRepository
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
        void RegisterCreate(InvitationsOffersPost invitationsoffersPost);
        void RegisterEdit(InvitationsOffersPost invitationsoffersPost);
        void RegisterDelete(InvitationsOffersPost invitationsoffersPost);
        void Rollback();
        void Commit();
    }
}
  

