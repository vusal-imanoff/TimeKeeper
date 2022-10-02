using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.TagDTO
{
    public class TagListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
