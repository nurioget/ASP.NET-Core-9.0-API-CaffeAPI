﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Dtos.CaffeInfoDtos
{
    public class UpdateCaffeInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? WebSieteUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string OpeningHourse { get; set; }
        public string ImageUrl { get; set; }
    }
}
