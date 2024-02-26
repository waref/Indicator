using Depences.Infrastructure.Interfaces.IDataModel;
using Depences.Infrastructure.Static;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depences.Infrastructure.Implementation.DataModels
{
    [Table(name: DbConstants.DeviseTableName, Schema = DbConstants.DepenceSchemaName)]
    public class DeviseEntity : IEntity
    {
        public int DeviseId { get; set; }
        [Required]
        public string? Code { get; set; }

    }
}
