using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public async Task AddCategoryAsyncByLanguage(AddCategoryDTO model)
        {
            await _categoryDAL.AddCategoryAsync(model);
        }

        public GetCategoryDTO GetCategoryById(Guid id, string langCode)
        {
            var result = _categoryDAL.GetCategoryById(id, langCode);
            return result;
        }

        public async Task UpdateCategoryAsyncByLanguage(UpdateCategoryDTO model)
        {
            await _categoryDAL.UpdateCategoryAsync(model);
        }
    }
}
