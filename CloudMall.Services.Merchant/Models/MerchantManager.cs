using System;
using WeihanLi.Common.Models;

namespace CloudMall.Services.Merchant.Models
{
    public class MerchantManager : BaseEntity
    {
        public Guid UserId { get; set; }

        public int MerchantId { get; set; }
    }
}