using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Queries.GetBrands
{
    public interface IGetBrandsService
    {
        ResultDto<List<ResultBrandsDto>> Execute();
    }
    public class GetBrandsService:IGetBrandsService
    {
        private readonly IDataBaseContext _context;
        public GetBrandsService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<ResultBrandsDto>> Execute()
        {
                return new ResultDto<List<ResultBrandsDto>>
                {
                    Data=_context.ProductBrands.Select(b=>new ResultBrandsDto
                    {
                        BrandId=b.BrandId,
                        BrandName=b.Brand,
                    }).ToList(),
                    IsSuccess=true
                };
        }
    }
    public class ResultBrandsDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
