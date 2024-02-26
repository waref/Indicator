using Depences.Infrastructure.Interfaces.IDataModel;
using Depences.Infrastructure.Static;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depences.Infrastructure.Implementation.DataModels
{
    [Table(name: DbConstants.UserDevisTableName, Schema = DbConstants.DepenceSchemaName)]
    public  class UserDeviseEntity : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserDeviseId { get; set; }
        public int? UserId { get; set; }
        public int? DeviseId { get; set; }
    }
}
