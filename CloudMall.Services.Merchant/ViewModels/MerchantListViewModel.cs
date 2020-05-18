using System.ComponentModel.DataAnnotations;
using WeihanLi.Common.Models;

namespace CloudMall.Services.Merchant.ViewModels
{
    public class MerchantListViewModel : BaseEntity
    {
        [StringLength(16)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public string LogoUrl { get; set; }

        public int CategoryId { get; set; }
    }
}