using System;
using System.Collections.Generic;

namespace Pixselio.Dto
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path{ get; set; }
        public string Title { get; set; }
        public List<TagDto> Tags{ get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public string CreatedBy { get; set; }

    }
}
