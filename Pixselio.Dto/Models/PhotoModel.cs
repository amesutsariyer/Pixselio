using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pixselio.Dto.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
