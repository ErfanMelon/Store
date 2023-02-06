using MediatR;
using Store.Application.Interfaces.Context;

namespace Store.Application.Services.Products.Commands.VisitProduct;

public class VisitProductCommand : INotification
{
    public VisitProductCommand(long productId)
    {
        ProductId = productId;
    }

    public long ProductId { get; }
    public class Handler : INotificationHandler<VisitProductCommand>
    {
        private readonly IDataBaseContext _context;

        public Handler(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task Handle(VisitProductCommand notification, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(notification.ProductId);
            if (product != null)
            {
                int views = product.Views;
                product.Views = views + 1;
                await _context.SaveChangesAsync(cancellationToken);
            }
            
        }
    }
}
