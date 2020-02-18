using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pixselio.Data
{
   public class Photo : BaseEntity
    {
  
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public List<PhotosTag> PhotosTag { get; set; }
    }
}
