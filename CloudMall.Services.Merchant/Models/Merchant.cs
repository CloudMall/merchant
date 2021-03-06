﻿using System.ComponentModel.DataAnnotations;
using WeihanLi.Common.Models;

namespace CloudMall.Services.Merchant.Models
{
    public class Merchant : BaseEntityWithDeleted
    {
        [StringLength(16)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public string LogoUrl { get; set; }

        public string Extra { get; set; }

        public int CategoryId { get; set; }

        public ReviewState State { get; set; }

        [StringLength(256)]
        public string Remark { get; set; }
    }
}