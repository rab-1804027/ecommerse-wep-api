using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommer_web_api.DTO
{
    public class CategoryReadDto
    {
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }    
    }
}