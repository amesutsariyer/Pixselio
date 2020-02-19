using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixselio.Dto.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
