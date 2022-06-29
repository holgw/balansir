using BalansirApp.Core.Common.DataAccess;
using SQLite;
using System;

namespace BalansirApp.Core.Acts
{
    [Table("ActSet")]
    public class Act : DbRecord
    {
        /// <summary>
        /// Дата и время создания акта
        /// </summary>
        [Indexed] public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Ссылка на продукт
        /// </summary>
        [Indexed] public int ProductId { get; set; }

        /// <summary>
        /// Кол-во единиц продукции прихода\расхода 
        /// </summary>
        public decimal Delta { get; set; }

        /// <summary>
        /// Комментарий к акту
        /// </summary>
        public string Comment { get; set; }
    }
}
