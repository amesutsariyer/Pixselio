using System;
using System.Collections.Generic;
using System.Text;

namespace Pixselio.Data
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<PhotosTag> PhotosTag { get; set; }
    }
}
