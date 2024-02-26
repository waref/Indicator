using Depences.Infrastructure.Interfaces.IDataModel;
using Depences.Infrastructure.Static;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Depences.Infrastructure.Implementation.DataModels
{
    [Table(name: DbConstants.UserTableName, Schema = DbConstants.DepenceSchemaName)]
    public class UserEntity : IEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? NomFamille { get; set; }
        public string? Prenom { get; set; }
        [Required]
        public int DeviseId { get; set; }
        public virtual DeviseEntity? Devise { get; set; }
    }
}
