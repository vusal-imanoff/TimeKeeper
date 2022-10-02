using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Core.Entities
{
    public class Slider : BaseEntity
    {
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
