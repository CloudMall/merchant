using CloudMall.Services.Merchant.Database;
using CloudMall.Services.Merchant.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeihanLi.Common.Models;
using WeihanLi.EntityFramework;

namespace CloudMall.Services.Merchant.Controllers
{
    public class MerchantManagersController : MerchantControllerBase
    {
        private readonly IEFRepository<MerchantDbContext, MerchantManager> _repository;

        public MerchantManagersController(IEFRepositoryFactory<MerchantDbContext> repositoryFactory)
            : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetRepository<MerchantManager>();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MerchantManager model)
        {
            if (await _repository.ExistAsync(c => c.UserId == model.UserId && c.MerchantId == model.MerchantId))
            {
                return BadRequest(ResultModel.Fail<MerchantManager>("数据已存在"));
            }
            await _repository.InsertAsync(model);
            return Ok(ResultModel.Success(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ResultModel.Fail<MerchantManager>("请求参数异常"));
            }

            var result = await _repository.DeleteAsync(new MerchantManager()
            {
                Id = id
            });
            return Ok(ResultModel.Success(result));
        }
    }
}