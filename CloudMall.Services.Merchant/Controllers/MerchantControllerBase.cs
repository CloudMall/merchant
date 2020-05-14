using CloudMall.Services.Merchant.Database;
using Microsoft.AspNetCore.Mvc;
using WeihanLi.EntityFramework;

namespace CloudMall.Services.Merchant.Controllers
{
    [Route("api/merchant/[controller]")]
    [ApiController]
    public abstract class MerchantControllerBase : ControllerBase
    {
        protected readonly IEFRepositoryFactory<MerchantDbContext> RepositoryFactory;

        protected MerchantControllerBase(IEFRepositoryFactory<MerchantDbContext> repositoryFactory)
        {
            RepositoryFactory = repositoryFactory;
        }
    }
}