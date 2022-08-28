using BalansirApp.Core.Common.DataAccess;
using LinqToDB.Mapping;

namespace BalansirApp.Core.Products
{
    [Table("ProductSet")]
    public class Product : DbRecord
    {
        /// <summary>
        /// Наименование продукта
        /// </summary>
        [Column] public string Name { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        [Column] public string Code { get; set; }

        /// <summary>
        /// Единицы учета продукта (штуки, литры, погонные метры)
        /// </summary>
        [Column] public string Units { get; set; }

        /// <summary>
        /// Описание продукта
        /// </summary>
        [Column] public string Description { get; set; }

        /// <summary>
        /// Текущий баланс (остаток) на складе
        /// </summary>
        [Column] public decimal Balance { get; set; }
    }
}
