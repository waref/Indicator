using Depences.Infrastructure.Interfaces.IDataModel;
using Depences.Infrastructure.Static;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depences.Infrastructure.Implementation.DataModels
{
    [Table(name: DbConstants.NatureTableName, Schema = DbConstants.DepenceSchemaName)]
    public class NatureEntity : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NatureId { get; set; }
        public required string Code { get; set; }
    }
}
