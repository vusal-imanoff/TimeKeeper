using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Name { get; set; }
        public string MainBlog { get; set; }
        public string SubBlog { get; set; }
        public string Image { get; set; }
    }
}
