using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class DocumentMessageTypesService : IDocumentMessageTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IDocumentMessageTypesRepository _documentmessagetypesRepository;

        public DocumentMessageTypesService(IDocumentMessageTypesRepository documentmessagetypesRepository)
        {
            _documentmessagetypesRepository = documentmessagetypesRepository;
        }

        public DocumentMessageTypesPost GetPost(Int64 ixDocumentMessageType) => _documentmessagetypesRepository.GetPost(ixDocumentMessageType);
        public DocumentMessageTypes Get(Int64 ixDocumentMessageType) => _documentmessagetypesRepository.Get(ixDocumentMessageType);
        public IQueryable<DocumentMessageTypes> Index() => _documentmessagetypesRepository.Index();
        public IQueryable<DocumentMessageTypes> IndexDb() => _documentmessagetypesRepository.IndexDb();
        public bool VerifyDocumentMessageTypeUnique(Int64 ixDocumentMessageType, string sDocumentMessageType) => _documentmessagetypesRepository.VerifyDocumentMessageTypeUnique(ixDocumentMessageType, sDocumentMessageType);
        public List<string> VerifyDocumentMessageTypeDeleteOK(Int64 ixDocumentMessageType, string sDocumentMessageType) => _documentmessagetypesRepository.VerifyDocumentMessageTypeDeleteOK(ixDocumentMessageType, sDocumentMessageType);

        public Task<Int64> Create(DocumentMessageTypesPost documentmessagetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._documentmessagetypesRepository.RegisterCreate(documentmessagetypesPost);
            try
            {
                this._documentmessagetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._documentmessagetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(documentmessagetypesPost.ixDocumentMessageType);

        }
        public Task Edit(DocumentMessageTypesPost documentmessagetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._documentmessagetypesRepository.RegisterEdit(documentmessagetypesPost);
            try
            {
                this._documentmessagetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._documentmessagetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(DocumentMessageTypesPost documentmessagetypesPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._documentmessagetypesRepository.RegisterDelete(documentmessagetypesPost);
            try
            {
                this._documentmessagetypesRepository.Commit();
            }
            catch(Exception ex)
            {
                this._documentmessagetypesRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

