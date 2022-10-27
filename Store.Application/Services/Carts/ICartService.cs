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
}
