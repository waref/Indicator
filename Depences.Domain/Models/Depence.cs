using Depences.Domain.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Depences.Domain.Models
{
    public class Depence
    {
        [JsonIgnore]
        public int? DepenceId { get; set; }
        public int? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public string? UserFullName { get; set; }
        public int? NatureId { get; set; }
        [JsonIgnore]
        public virtual Nature? Nature { get; set; }
        public double? Montant { get; set; }
        [Required(ErrorMessage = "Le champs commentaire est obligatoire.")]
        public string Commentaire { get; set; }
        [DepenceDateValidation]
        public DateTime? DepenceDate { get; set; }

        public  int CurrencyId { get; set; }
        public Depence(int? depenceId, User? user, Nature? nature, double? montant, string commentaire, 
            DateTime? depenceDate,int currencyId)
        {
            DepenceId = depenceId;
            UserId = user?.UserId;
            NatureId = nature?.NatureId;
            Montant = montant;
            Commentaire = commentaire;
            DepenceDate = depenceDate;
            UserFullName = user?.NomFamille + ' ' + user?.Prenom;
            CurrencyId = currencyId;
        }
       

    }
}
