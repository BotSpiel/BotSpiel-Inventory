using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Repositories;


namespace BotSpiel.Services
{

    public class DocumentsService : IDocumentsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
         
        private readonly IDocumentsRepository _documentsRepository;

        public DocumentsService(IDocumentsRepository documentsRepository)
        {
            _documentsRepository = documentsRepository;
        }

        public DocumentsPost GetPost(Int64 ixDocument) => _documentsRepository.GetPost(ixDocument);
        public Documents Get(Int64 ixDocument) => _documentsRepository.Get(ixDocument);
        public IQueryable<Documents> Index() => _documentsRepository.Index();
       public IQueryable<Statuses> selectStatuses() => _documentsRepository.selectStatuses();
        public IQueryable<DocumentMessageTypes> selectDocumentMessageTypes() => _documentsRepository.selectDocumentMessageTypes();
        public bool VerifyDocumentUnique(Int64 ixDocument, string sDocument) => _documentsRepository.VerifyDocumentUnique(ixDocument, sDocument);
        public List<string> VerifyDocumentDeleteOK(Int64 ixDocument, string sDocument) => _documentsRepository.VerifyDocumentDeleteOK(ixDocument, sDocument);

        public Task<Int64> Create(DocumentsPost documentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._documentsRepository.RegisterCreate(documentsPost);
            try
            {
                this._documentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._documentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

			return Task.FromResult(documentsPost.ixDocument);

        }
        public Task Edit(DocumentsPost documentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._documentsRepository.RegisterEdit(documentsPost);
            try
            {
                this._documentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._documentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
        public Task Delete(DocumentsPost documentsPost)
        {
            // Additional validations

            // Pre-process

            // Process
            this._documentsRepository.RegisterDelete(documentsPost);
            try
            {
                this._documentsRepository.Commit();
            }
            catch(Exception ex)
            {
                this._documentsRepository.Rollback();
                // Log exception
                throw;
            }

            // Post-process

            return Task.CompletedTask;

        }
    }
}
  

