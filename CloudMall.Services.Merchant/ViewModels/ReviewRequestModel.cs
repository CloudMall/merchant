using System.ComponentModel.DataAnnotations;
using WeihanLi.Common.Models;

namespace CloudMall.Services.Merchant.ViewModels
{
    public class ReviewRequestModel
    {
        public ReviewState State { get; set; }

        [StringLength(256)]
        public string Remark { get; set; }
    }
}