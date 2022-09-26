using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Context;
using Store.Application.Services.Products.Commands.AddProduct;
using Store.Common.Dto;
using Store.Domain.Entities.Products;
using Store.Application.Validations.Product;

namespace Store.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto Execute(RequestEditProductDto request);
    }
    public class RequestEditProductDto : RequestProductDto
    {
        public long ProductId { get; set; }
    }
    public class EditProductService : IEditProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EditProductService(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        private UploadDto RemoveImages(string filepath)
        {
            if (filepath != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }

                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, filepath);

                if (!File.Exists(filePath))
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }


                File.Delete(filePath);

                return new UploadDto()
                {
                    FileNameAddress = folder + filepath,
                    Status = true,
                };
            }
            return null;

        }
        private UploadDto UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images\ProductImages\";
                var uploadsRootFolder = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }


                if (file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = "",
                    };
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true,
                };
            }
            return null;
        }

        public ResultDto Execute(RequestEditProductDto request)
        {
            var Validation = new RequestEditProductDtoValidation();
            var Valid = Validation.Validate(request);
            if (!Valid.IsValid)
            {
                return new ResultDto { Message = Valid.Errors[0].ErrorMessage };
            }
            try
            {
                var product = _context.Products.Where(p => p.ProductId == request.ProductId)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductFeatures)
                    .Include(p => p.Category)
                    .Include(p=>p.Brand)
                    .FirstOrDefault();
                // Update Operation
                
                product.ProductTitle = request.ProductTitle;
                product.Brand = _context.ProductBrands.Find(request.BrandId);
                product.Category = _context.Categories.Find(request.CategoryId);
                product.Inventory = request.Inventory;
                product.Price = request.Price;
                product.Description = request.Description;
                product.Displayed = request.Displayed;
                product.UpdateTime = DateTime.Now;

                // images Update

                foreach (var item in product.ProductImages)//delete olds
                {
                    if (RemoveImages(item.Src).Status)
                        _context.ProductImages.Remove(item);
                }
                var images = new List<ProductImages>();
                foreach (var item in request.Images)//add new
                {
                    var srcimg = UploadFile(item);
                    if (srcimg.Status)
                        images.Add(new ProductImages
                        {
                            Product = product,
                            Src = srcimg.FileNameAddress,
                        });
                }
                _context.ProductImages.AddRange(images);

                // feature Update 
                if (product.ProductFeatures.Any())
                    _context.ProductFeatures.RemoveRange(product.ProductFeatures);

                if (request.ProductFeatures.Any())
                    _context.ProductFeatures.AddRange(request.ProductFeatures.ToList().Select(f => new ProductFeatures
                    {
                        Feature = f.Feature,
                        FeatureValue = f.Value,
                        Product = product
                    }
                    ));

                //save
                _context.SaveChanges();
                return new ResultDto { IsSuccess = true, Message = $"{product.ProductTitle} با موفقیت ویرایش شد !" };
            }
            catch (Exception)
            {
                return new ResultDtoError();
            }
        }
    }
}
