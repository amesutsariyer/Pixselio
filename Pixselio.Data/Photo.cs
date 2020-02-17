using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pixselio.Data
{
   public class Photo : BaseEntity
    {
        public Photo()
        {
            Tags = new HashSet<Tag>();
        }
        [Required]
        public string Name { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
