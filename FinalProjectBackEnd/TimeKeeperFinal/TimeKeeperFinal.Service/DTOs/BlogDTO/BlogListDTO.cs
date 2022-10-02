using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.BlogDTO
{
    public class BlogListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainBlog { get; set; }
        public string SubBlog { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
    }
}
