using BalansirApp.Core.Common.DataAccess;

namespace BalansirApp.Core.Products
{
    public class ProductView : IIdentifier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }

        public string BalanceText => $"{Balance} {Units}";

        public bool IsDescriptionVisible => !string.IsNullOrEmpty(Description);

        public static ProductView Map(Product record)
        {
            return new ProductView()
            {
                Id = record.Id,
                Name = record.Name,
                Code = record.Code,
                Units = record.Units,
                Description = record.Description,
                Balance = record.Balance,
            };
        }
    }
}
