using CloudMall.Services.Merchant.Database;
using CloudMall.Services.Merchant.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WeihanLi.Common.Helpers;
using WeihanLi.Common.Models;
using WeihanLi.EntityFramework;
using WeihanLi.Extensions;

namespace CloudMall.Services.Merchant.Controllers
{
    public class MerchantCategoriesController : MerchantControllerBase
    {
        private readonly IEFRepository<MerchantDbContext, MerchantCategory> _repository;

        public MerchantCategoriesController(IEFRepositoryFactory<MerchantDbContext> repositoryFactory) : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetRepository<MerchantCategory>();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int parentId = 0)
        {
            var predict = ExpressionHelper.True<MerchantCategory>();
            if (parentId >= 0)
            {
                predict = predict.And(c => c.ParentId == parentId);
            }

            var result = await _repository.GetAsync(query => query
                .WithPredict(predict)
                .WithOrderBy(q =>
                        q.OrderBy(x => x.Id)));

            return Ok(ResultModel.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MerchantCategory category)
        {
            if (await _repository.ExistAsync(c => c.ParentId == category.ParentId && c.Name == category.Name))
            {
                return BadRequest(ResultModel.Fail<MerchantCategory>("分类已存在"));
            }
            //
            await _repository.InsertAsync(category);
            return Ok(ResultModel.Success(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]MerchantCategory category)
        {
            if (id <= 0)
            {
                return BadRequest(ResultModel.Fail<MerchantCategory>("请求参数异常"));
            }
            var before = await _repository.FindAsync(id);
            if (null == before)
            {
                return BadRequest(ResultModel.Fail<MerchantCategory>($"分类不存在,id:{id}"));
            }
            if (await _repository.ExistAsync(c =>
                c.ParentId == category.ParentId
                && c.Name == category.Name
                && c.Id != id
                ))
            {
                return BadRequest(ResultModel.Fail<MerchantCategory>("分类已存在"));
            }
            category.Id = id;
            await _repository.UpdateAsync(category);
            return Ok(ResultModel.Success(category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ResultModel.Fail<MerchantCategory>("请求参数异常"));
            }

            var result = await _repository.DeleteAsync(new MerchantCategory()
            {
                Id = id
            });
            return Ok(ResultModel.Success(result));
        }
    }
}