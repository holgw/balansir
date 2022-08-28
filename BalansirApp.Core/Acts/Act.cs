using BalansirApp.Core.Common.DataAccess;
using LinqToDB.Mapping;
using System;

namespace BalansirApp.Core.Acts
{
    [Table("ActSet")]
    public class Act : DbRecord
    {
        /// <summary>
        /// Дата и время создания акта
        /// </summary>
        [Column] public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Ссылка на продукт
        /// </summary>
        [Column] public int ProductId { get; set; }

        /// <summary>
        /// Кол-во единиц продукции прихода\расхода 
        /// </summary>
        [Column] public decimal Delta { get; set; }

        /// <summary>
        /// Комментарий к акту
        /// </summary>
        [Column] public string Comment { get; set; }
    }
}
