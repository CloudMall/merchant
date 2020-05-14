using WeihanLi.Common.Models;

namespace CloudMall.Services.Merchant.Database
{
    public class MerchantManager : BaseEntity
    {
        public int UserId { get; set; }

        public int MerchantId { get; set; }
    }
}