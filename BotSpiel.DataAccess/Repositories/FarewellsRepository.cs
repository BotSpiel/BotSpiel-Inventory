using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class FarewellsRepository : IFarewellsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly FarewellsDB _context;
  
        public FarewellsRepository(FarewellsDB context)
        {
            _context = context;
  
        }

        public FarewellsPost GetPost(Int64 ixFarewell) => _context.FarewellsPost.AsNoTracking().Where(x => x.ixFarewell == ixFarewell).First();
         
		public Farewells Get(Int64 ixFarewell)
        {
            Farewells farewells = _context.Farewells.AsNoTracking().Where(x => x.ixFarewell == ixFarewell).First();
            farewells.Languages = _context.Languages.Find(farewells.ixLanguage);
            farewells.LanguageStyles = _context.LanguageStyles.Find(farewells.ixLanguageStyle);
            farewells.ResponseTypes = _context.ResponseTypes.Find(farewells.ixResponseType);

            return farewells;
        }

        public IQueryable<Farewells> Index()
        {
            var farewells = _context.Farewells.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return farewells;
        }

        public IQueryable<Farewells> IndexDb()
        {
            var farewells = _context.Farewells.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return farewells;
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
        public List<string> VerifyFarewellDeleteOK(Int64 ixFarewell, string sFarewell)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(FarewellsPost farewellsPost)
		{
            _context.FarewellsPost.Add(farewellsPost); 
        }

        public void RegisterEdit(FarewellsPost farewellsPost)
        {
            _context.Entry(farewellsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(FarewellsPost farewellsPost)
        {
            _context.FarewellsPost.Remove(farewellsPost);
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
  

