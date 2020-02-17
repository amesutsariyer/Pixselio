using System;
using System.Collections.Generic;
using System.Text;

namespace Pixselio.Data
{
    public class Tag : BaseEntity
    {
        public Tag()
        {
            Photos = new HashSet<Photo>();
        }
        public string Name { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
