﻿using Entities.Concrete;
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
        Task AddCategoryAsyncByLanguage(AddCategoryDTO model);
        Task UpdateCategoryAsyncByLanguage(UpdateCategoryDTO model);
        GetCategoryDTO GetCategoryById(Guid id, string langCode);
    }
}
