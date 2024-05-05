using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DTOs.CategoryDTOs
{
    public class UpdateCategoryDTO
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public List<UpdateCategoryLanguageDTO> Language { get; set; }
    }
}
