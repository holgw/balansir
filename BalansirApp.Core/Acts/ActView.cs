using BalansirApp.Core.Common.DataAccess;
using BalansirApp.Core.Products;
using System;

namespace BalansirApp.Core.Acts
{
    public class ActView : IIdentifier
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Delta { get; set; }
        public string DeltaString => $"{(Delta >= 0 ? "+" : string.Empty)}{Delta} {ProductUnits}";
        public string Comment { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductUnits { get; set; }
        public string ProductDescription { get; set; }

        public static ActView GetNew(ProductView product)
        {
            return new ActView()
            {
                Delta = 1,
                TimeStamp = DateTime.Now,
                ProductCode = product.Code,
                ProductId = product.Id,
                ProductDescription = product.Description,
                ProductName = product.Name,
                ProductUnits = product.Units,
            };
        }
        public static ActView Map(Act act, Product product)
        {
            return new ActView()
            {
                Id = act?.Id ?? 0,
                TimeStamp = act?.TimeStamp ?? DateTime.MinValue,
                Delta = act?.Delta ?? 0,
                Comment = act?.Comment,
                ProductId = product?.Id ?? 0,
                ProductName = product?.Name,
                ProductCode = product?.Code,
                ProductUnits = product?.Units,
                ProductDescription = product?.Description,
            };
        }
    }
}
