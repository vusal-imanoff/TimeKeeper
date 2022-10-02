using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.SliderDTO
{
    public class SliderGetDTO
    {
        public int Id { get; set; }
        public string MainTitle { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
