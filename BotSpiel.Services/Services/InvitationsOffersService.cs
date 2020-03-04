using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class InvitationsOffersService : IInvitationsOffersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IInvitationsOffersRepository _invitationsoffersRepository;

        public InvitationsOffersService(IInvitationsOffersRepository invitationsoffersRepository)
        {
            _invitationsoffersRepository = invitationsoffersRepository;
        }

        public InvitationsOffersPost GetPost(Int64 ixInvitationOffer) => _invitationsoffersRepository.GetPost(ixInvitationOffer);
        public InvitationsOffers Get(Int64 ixInvitationOffer) => _invitationsoffersRepository.Get(ixInvitationOffer);
        public IQueryable<InvitationsOffers> Index() => _invitationsoffersRepository.Index();
        public IQueryable<InvitationsOffers> IndexDb() => _invitationsoffersRepository.IndexDb();
       public IQueryable<Languages> selectLanguages() => _invitationsoffersRepository.selectLanguages();
        public IQueryable<LanguageStyles> selectLanguageStyles() => _invitationsoffersRepository.selectLanguageStyles();
        public IQueryable<ResponseTypes> selectResponseTypes() => _invitationsoffersRepository.selectResponseTypes();
       public IQueryable<Languages> LanguagesDb() => _invitationsoffersRepository.LanguagesDb();
        public IQueryable<LanguageStyles> LanguageStylesDb() => _invitationsoffersRepository.LanguageStylesDb();
        public IQueryable<ResponseTypes> ResponseTypesDb() => _invitationsoffersRepository.ResponseTypesDb();
        public List<string> VerifyInvitationOfferDeleteOK(Int64 ixInvitationOffer, string sInvitationOffer) => _invitationsoffersRepository.VerifyInvitationOfferDeleteOK(ixInvitationOffer, sInvitationOffer);

        public Task<Int64> Create(InvitationsOffersPost invitationsoffersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invitationsoffersRepository.RegisterCreate(invitationsoffersPost);
            try
            {
                this._invitationsoffersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invitationsoffersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(invitationsoffersPost.ixInvitationOffer);

        }
        public Task Edit(InvitationsOffersPost invitationsoffersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invitationsoffersRepository.RegisterEdit(invitationsoffersPost);
            try
            {
                this._invitationsoffersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invitationsoffersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(InvitationsOffersPost invitationsoffersPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._invitationsoffersRepository.RegisterDelete(invitationsoffersPost);
            try
            {
                this._invitationsoffersRepository.Commit();
            }
            catch(Exception ex)
            {
                this._invitationsoffersRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

