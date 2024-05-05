using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class AddCategoryDTO
    {
        public List<AddCategoryLanguageDTO> Language { get; set; }
    }
}
