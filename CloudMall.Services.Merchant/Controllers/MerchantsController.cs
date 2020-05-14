﻿using CloudMall.Services.Merchant.Database;
using CloudMall.Services.Merchant.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeihanLi.Common.Helpers;
using WeihanLi.Common.Models;
using WeihanLi.EntityFramework;

namespace CloudMall.Services.Merchant.Controllers
{
    public class MerchantsController : MerchantControllerBase
    {
        private readonly IEFRepository<MerchantDbContext, Models.Merchant> _repository;

        public MerchantsController(IEFRepositoryFactory<MerchantDbContext> repositoryFactory) : base(repositoryFactory)
        {
            _repository = repositoryFactory.GetRepository<Models.Merchant>();
        }

        [HttpGet]
        public async Task<IActionResult> Get(string keyword, int pageNum = 1, int pageSize = 10)
        {
            if (pageNum <= 0) pageNum = 1;
            if (pageSize <= 0) pageSize = 10;

            var predict = ExpressionHelper.True<Models.Merchant>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                predict = x => x.Name.Contains(keyword);
            }

            var result = await _repository.GetPagedListAsync(query => query
                .WithPredict(predict)
                .WithOrderBy(q =>
                        q.OrderByDescending(x => x.Id)), pageNum, pageSize);

            return Ok(ResultModel.Success(result));
        }

        [HttpGet("{id}/managers")]
        public async Task<IActionResult> GetManagers(int id)
        {
            var result = await RepositoryFactory.GetRepository<MerchantManager>()
                .GetResultAsync(x => x.UserId, q => q.WithPredict(m => m.MerchantId == id));

            return Ok(ResultModel.Success(result));
        }

        [HttpDelete("{id}/managers/{userId}")]
        public async Task<IActionResult> DeleteManager(int id, Guid userId)
        {
            var managerRepo = RepositoryFactory.GetRepository<MerchantManager>();
            var manager = await managerRepo.FirstOrDefaultAsync(x =>
                x.WithPredict(m => m.MerchantId == id && m.UserId == userId)
                    );
            if (null == manager)
            {
                return BadRequest(ResultModel.Fail<MerchantManager>("manager 不存在"));
            }

            var result = await managerRepo.DeleteAsync(manager);
            return Ok(ResultModel.Success(manager));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetManagers(Guid userId)
        {
            var merchantIds = await RepositoryFactory.GetRepository<MerchantManager>()
                .GetResultAsync(x => x.MerchantId, q => q.WithPredict(m => m.UserId == userId));
            if (merchantIds.Count == 0)
            {
                return Ok(ResultModel.Success(Array.Empty<Models.Merchant>()));
            }

            var result = await _repository.GetAsync(q => q.WithPredict(m => merchantIds.Contains(m.Id)));
            return Ok(ResultModel.Success(result));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Models.Merchant category)
        {
            await _repository.InsertAsync(category);
            return Ok(ResultModel.Success(category));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Models.Merchant model)
        {
            if (id <= 0)
            {
                return BadRequest(ResultModel.Fail<Models.Merchant>("请求参数异常"));
            }
            var before = await _repository.FindAsync(id);
            if (null == before)
            {
                return BadRequest(ResultModel.Fail<Models.Merchant>($"分类不存在,id:{id}"));
            }

            model.Id = id;
            await _repository.UpdateAsync(model);
            return Ok(ResultModel.Success(model));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(ResultModel.Fail<Models.Merchant>("请求参数异常"));
            }

            var result = await _repository.DeleteAsync(new Models.Merchant()
            {
                Id = id
            });
            return Ok(ResultModel.Success(result));
        }
    }
}