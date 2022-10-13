using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Context;
using Store.Common.Dto;
using Store.Domain.Entities.Carts;

namespace Store.Application.Services.Carts
{
    public interface ICartService
    {
        ResultDto<CartDto> GetCart(Guid browserId);
        ResultDto<CartDto> GetCart(long userId);
        ResultDto AddToCart(Guid browserId, long productId, short count);
        ResultDto DeleteFromCart(Guid browserId, long productId);
        ResultDto EditProductCart(Guid browserId, long productId, short count);
        void JoinCartToUser(Guid browserId, long userId);
        CartDto CreateCart(Guid browserId, out Cart cart);
    }
    public class CartService : ICartService
    {
        private readonly IDataBaseContext _context;
        public CartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddToCart(Guid browserId, long productId, short count)
        {
            var cart = _context.Carts
                .Include(c => c.ItemsInCart)
                .Where(c => c.BrowserId == browserId)
                .FirstOrDefault(c => c.CurrentCart);
            if (cart == null) // User doesnt have a cart
            {
                CreateCart(browserId, out cart);
            }
            if (cart.ItemsInCart != null && cart.ItemsInCart.Where(i => i.ProductId == productId).Any()) //if product exist adds to it
            {
                var item = _context.ProductsInCarts.Where(i => i.ProductId == productId).First();
                item.ProductCount += count;
            }
            else
                _context.ProductsInCarts.Add(new ProductsInCart
                {
                    ProductCount = count,
                    CartId = cart.CartId,
                    ProductId = productId
                });
            _context.SaveChanges();
            return new ResultDto { IsSuccess = true };
        }

        public CartDto CreateCart(Guid browserId, out Cart cart)
        {
            cart = new Cart// Create a cart for user
            {
                BrowserId = browserId,
                CurrentCart = true,
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();

            return new CartDto
            {
                CartId = cart.CartId,
                TotalPrice = 0,
                ProductDtos = new List<CartProductDto>()
            };
        }

        public ResultDto DeleteFromCart(Guid browserId, long productId)
        {
            var cart = _context.Carts
                .Include(c => c.ItemsInCart)
                .Where(c => c.BrowserId == browserId)
                .FirstOrDefault(c => c.CurrentCart);
            if (cart != null)
            {
                var validproduct = cart.ItemsInCart.Any(item => item.ProductId == productId);
                if (validproduct)
                {
                    var itemincart = cart.ItemsInCart.Where(p => p.ProductId == productId).First();
                    itemincart.IsRemoved = true;
                    itemincart.RemoveTime = DateTime.Now;
                    cart.UpdateTime = DateTime.Now;
                    _context.SaveChanges();
                    return new ResultDto { IsSuccess = true };
                }
                return new ResultDto { Message = "محصول پیدا نشد" };
            }
            return new ResultDto { Message = "سبد پیدا نشد" };
        }

        public ResultDto EditProductCart(Guid browserId, long productId, short count)
        {
            var cart = _context.Carts
                .Include(c => c.ItemsInCart)
                .Where(c => c.BrowserId == browserId)
                .FirstOrDefault(c => c.CurrentCart);
            if (cart != null)
            {
                var validproduct = cart.ItemsInCart.Any(item => item.ProductId == productId);
                if (validproduct)
                {
                    cart.ItemsInCart.Where(p => p.ProductId == productId).First().ProductCount += count;
                    if (cart.ItemsInCart.Where(p => p.ProductId == productId).First().ProductCount < 1)
                        return DeleteFromCart(browserId, productId);
                    cart.UpdateTime = DateTime.Now;
                    _context.SaveChanges();
                    return new ResultDto { IsSuccess = true };
                }
                return new ResultDto { Message = "محصول پیدا نشد" };
            }
            return new ResultDto { Message = "سبد پیدا نشد" };
        }

        public ResultDto<CartDto> GetCart(Guid browserId)
        {
            var cart = _context.Carts
                .Include(c => c.ItemsInCart)
                .ThenInclude(p => p.SelectedProduct)
                .ThenInclude(p => p.ProductImages)
                .Where(c => c.BrowserId == browserId)
                .FirstOrDefault(c => c.CurrentCart);
            if (cart != null)
            {
                return GetCart(cart);
            }
            return new ResultDto<CartDto>
            {
                Message = "کاربر سبد خرید فعالی ندارد !",
                Data = CreateCart(browserId, out _)
            };
        }

        public ResultDto<CartDto> GetCart(long userId)
        {
            var cart = _context.Carts
                 .Include(c => c.ItemsInCart)
                 .ThenInclude(p => p.SelectedProduct)
                 .ThenInclude(p => p.ProductImages)
                 .Where(c => c.UserId == userId)
                 .FirstOrDefault(c => c.CurrentCart);
            if (cart != null)
            {
                return GetCart(cart);
            }
            return new ResultDto<CartDto> { Message = "کاربر سبد خرید فعالی ندارد !" };
        }

        public void JoinCartToUser(Guid browserId, long userId)
        {
            var cart = _context.Carts.Where(c => c.BrowserId == browserId)
                 .FirstOrDefault(c => c.CurrentCart);
            if (cart != null)
            {
                cart.UserId = userId;
                cart.UpdateTime = DateTime.Now;
                _context.SaveChanges();
            }
        }

        ResultDto<CartDto> GetCart(Cart cart)
        {
            return new ResultDto<CartDto>
            {
                Data = new CartDto
                {
                    CartId = cart.CartId,
                    TotalPrice = cart.ItemsInCart.Sum(items => items.SelectedProduct.Price * items.ProductCount),
                    ProductDtos = cart.ItemsInCart.Select(item => new CartProductDto
                    {
                        Count = item.ProductCount,
                        Price = item.SelectedProduct.Price,
                        ProductId = item.ProductId,
                        ProductTitle = item.SelectedProduct.ProductTitle,
                        ProductImg = item.SelectedProduct.ProductImages.FirstOrDefault()?.Src
                    }).ToList()
                },
                IsSuccess = true
            };
        }
    }
    public class CartDto
    {
        public long CartId { get; set; }
        public List<CartProductDto> ProductDtos { get; set; }
        public int TotalPrice { get; set; }

    }
    public class CartProductDto
    {
        public long ProductId { get; set; }
        public string ProductTitle { get; set; }
        public int Price { get; set; }
        public short Count { get; set; }
        public string ProductImg { get; set; }
    }
}
