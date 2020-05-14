using Microsoft.AspNetCore.Mvc;

namespace CloudMall.Services.Merchant.Controllers
{
    [Route("api/merchant/[controller]")]
    public abstract class MerchantControllerBase : ControllerBase
    {
    }
}