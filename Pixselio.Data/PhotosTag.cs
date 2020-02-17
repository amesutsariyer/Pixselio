using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pixselio.Data
{
    public class PhotosTag
    {
        public Photo Photo { get; set; }
        public int PhotoId { get; set; }
        
        public Tag Tag { get; set; }

        public int TagId { get; set; }

    }
}
