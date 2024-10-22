using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<IResult> AddCategoryAsyncByLanguage(AddCategoryDTO model)
        {
            var result = await _categoryDAL.AddCategoryAsync(model);
            if (result.Success)
                return new SuccessResult(result.StatusCode);
            return new ErrorResult(message: result.Message, statusCode: result.StatusCode);
        }

        public IResult DeleteCategory(Guid id)
        {
            var findCategory = _categoryDAL.Get(x => x.Id == id);
            if (findCategory == null)
                return new ErrorResult(System.Net.HttpStatusCode.NotFound);

            _categoryDAL.Delete(findCategory);
            return new SuccessResult(System.Net.HttpStatusCode.OK);
        }

        public IDataResult<List<GetCategoryDTO>> GetAllCategories()
        {
            var result = _categoryDAL.GetAll(x => x.IsDelete == false);
            return null;
        }

        public IDataResult<GetCategoryDTO> GetCategoryById(Guid id, string langCode)
        {
            var result = _categoryDAL.GetCategoryById(id, langCode);
            if (result == null)
                return new ErrorDataResult<GetCategoryDTO>(System.Net.HttpStatusCode.BadRequest);
            return new SuccessDataResult<GetCategoryDTO>(data: result, HttpStatusCode.OK);
        }

        public async Task UpdateCategoryAsyncByLanguage(UpdateCategoryDTO model)
        {
            await _categoryDAL.UpdateCategoryAsync(model);
        }
    }
}
