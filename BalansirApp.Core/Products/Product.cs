using BalansirApp.Core.Common.DataAccess;
using SQLite;

namespace BalansirApp.Core.Products
{
    [Table("ProductSet")]
    public class Product : DbRecord
    {
        /// <summary>
        /// Наименование продукта
        /// </summary>
        [Unique] public string Name { get; set; }

        /// <summary>
        /// Код продукта
        /// </summary>
        [Unique] public string Code { get; set; }

        /// <summary>
        /// Единицы учета продукта (штуки, литры, погонные метры)
        /// </summary>
        public string Units { get; set; }

        /// <summary>
        /// Описание продукта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Текущий баланс (остаток) на складе
        /// </summary>
        public decimal Balance { get; set; }
    }
}
