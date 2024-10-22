using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<IResult> AddCategoryAsyncByLanguage(AddCategoryDTO model);
        Task UpdateCategoryAsyncByLanguage(UpdateCategoryDTO model);
        IDataResult<GetCategoryDTO> GetCategoryById(Guid id, string langCode);
        IResult DeleteCategory(Guid id);
        IDataResult<List<GetCategoryDTO>> GetAllCategories();
    }
}
