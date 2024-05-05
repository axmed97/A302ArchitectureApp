using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFCategoryDAL : EFRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public async Task AddCategoryAsync(AddCategoryDTO model)
        {
			try
			{
				using var context = new AppDbContext();

				Category category = new()
				{
					CreatedDate = DateTime.Now,
				};
				await context.Categories.AddAsync(category);
				await context.SaveChangesAsync();

				for (int i = 0; i < model.Language.Count; i++)
				{
					CategoryLanguage categoryLanguage = new()
					{
						CategoryName = model.Language[i].CategoryName,
						LangCode = model.Language[i].LangCode,
						CategoryId = category.Id
					};
					await context.CategoryLanguages.AddAsync(categoryLanguage);
				}
				await context.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
        }

        public GetCategoryDTO GetCategoryById(Guid id, string langCode)
        {
			try
			{
				var context = new AppDbContext();

				var findCategory = context.CategoryLanguages.FirstOrDefault(x => x.CategoryId == id && x.LangCode == langCode);
				
				GetCategoryDTO getCategoryDTO = new()
				{
					CategoryName = findCategory.CategoryName,
					LangCode = findCategory.LangCode,
					Id = findCategory.Id
				};
				return getCategoryDTO;
				
			}
			catch (Exception)
			{

				throw;
			}
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO model)
        {
			try
			{
				using var context = new AppDbContext();

				var findCategory = context.Categories.FirstOrDefault(x => x.Id == model.Id);
				if(findCategory == null)
				{
					throw new Exception("Category not found!");
				}

				var findCategories = context.CategoryLanguages.Where(x => x.CategoryId == model.Id).ToList();
				context.CategoryLanguages.RemoveRange(findCategories);
				await context.SaveChangesAsync();

				for (int i = 0; i < model.Language.Count; i++)
				{
					CategoryLanguage categoryLanguage = new()
					{
						CategoryId = findCategory.Id,
						CategoryName = model.Language[i].CategoryName,
						LangCode = model.Language[i].LangCode
					};
					await context.CategoryLanguages.AddAsync(categoryLanguage);
				}
				await context.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
