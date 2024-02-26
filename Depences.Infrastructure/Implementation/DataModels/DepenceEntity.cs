using Depences.Infrastructure.Interfaces.IDataModel;
using Depences.Infrastructure.Static;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depences.Infrastructure.Implementation.DataModels
{
    [Table(name: DbConstants.DepenceTableName, Schema = DbConstants.DepenceSchemaName)]
    public class DepenceEntity : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int? DepenceId { get; set; }
        public int? UserId { get; set; }
        public virtual UserEntity? User { get; set; }
        public  int? NatureId { get; set; }
        public virtual NatureEntity? Nature { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public double? Montant { get; set; }
        public required string Commentaire { get; set; }
        public  int CurrencyId { get; set; }
        public DateTime? DepenceDate { get; set; }

    }
}
