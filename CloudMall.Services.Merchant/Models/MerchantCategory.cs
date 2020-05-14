using System.ComponentModel.DataAnnotations;
using WeihanLi.Common.Models;

namespace CloudMall.Services.Merchant.Models
{
    public class MerchantCategory : BaseEntity<int>
    {
        [StringLength(16)]
        [Required]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public int ParentId { get; set; }

        public string Extra { get; set; }
    }
}