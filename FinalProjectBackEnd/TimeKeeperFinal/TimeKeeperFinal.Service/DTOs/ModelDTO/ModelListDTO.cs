using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeperFinal.Service.DTOs.ModelDTO
{
    public class ModelListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
