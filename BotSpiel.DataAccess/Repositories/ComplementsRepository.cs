using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class ComplementsRepository : IComplementsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly ComplementsDB _context;
  
        public ComplementsRepository(ComplementsDB context)
        {
            _context = context;
  
        }

        public ComplementsPost GetPost(Int64 ixComplement) => _context.ComplementsPost.AsNoTracking().Where(x => x.ixComplement == ixComplement).First();
         
		public Complements Get(Int64 ixComplement)
        {
            Complements complements = _context.Complements.AsNoTracking().Where(x => x.ixComplement == ixComplement).First();
            complements.Languages = _context.Languages.Find(complements.ixLanguage);
            complements.LanguageStyles = _context.LanguageStyles.Find(complements.ixLanguageStyle);
            complements.ResponseTypes = _context.ResponseTypes.Find(complements.ixResponseType);

            return complements;
        }

        public IQueryable<Complements> Index()
        {
            var complements = _context.Complements.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return complements;
        }

        public IQueryable<Complements> IndexDb()
        {
            var complements = _context.Complements.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return complements;
        }
       public IQueryable<Languages> selectLanguages()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
        public IQueryable<LanguageStyles> selectLanguageStyles()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> selectResponseTypes()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
       public IQueryable<Languages> LanguagesDb()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
        public IQueryable<LanguageStyles> LanguageStylesDb()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> ResponseTypesDb()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
        public List<string> VerifyComplementDeleteOK(Int64 ixComplement, string sComplement)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(ComplementsPost complementsPost)
		{
            _context.ComplementsPost.Add(complementsPost); 
        }

        public void RegisterEdit(ComplementsPost complementsPost)
        {
            _context.Entry(complementsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(ComplementsPost complementsPost)
        {
            _context.ComplementsPost.Remove(complementsPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

