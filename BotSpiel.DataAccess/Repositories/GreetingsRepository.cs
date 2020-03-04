using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class GreetingsRepository : IGreetingsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly GreetingsDB _context;
  
        public GreetingsRepository(GreetingsDB context)
        {
            _context = context;
  
        }

        public GreetingsPost GetPost(Int64 ixGreeting) => _context.GreetingsPost.AsNoTracking().Where(x => x.ixGreeting == ixGreeting).First();
         
		public Greetings Get(Int64 ixGreeting)
        {
            Greetings greetings = _context.Greetings.AsNoTracking().Where(x => x.ixGreeting == ixGreeting).First();
            greetings.Languages = _context.Languages.Find(greetings.ixLanguage);
            greetings.LanguageStyles = _context.LanguageStyles.Find(greetings.ixLanguageStyle);
            greetings.ResponseTypes = _context.ResponseTypes.Find(greetings.ixResponseType);

            return greetings;
        }

        public IQueryable<Greetings> Index()
        {
            var greetings = _context.Greetings.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return greetings;
        }

        public IQueryable<Greetings> IndexDb()
        {
            var greetings = _context.Greetings.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return greetings;
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
        public List<string> VerifyGreetingDeleteOK(Int64 ixGreeting, string sGreeting)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(GreetingsPost greetingsPost)
		{
            _context.GreetingsPost.Add(greetingsPost); 
        }

        public void RegisterEdit(GreetingsPost greetingsPost)
        {
            _context.Entry(greetingsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(GreetingsPost greetingsPost)
        {
            _context.GreetingsPost.Remove(greetingsPost);
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
  

