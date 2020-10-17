using Dom;
using MongoDB.Entities;
using System.Threading.Tasks;

namespace Migrations
{
    public class _001_seed_establishment_types : IMigration
    {
        public Task UpgradeAsync()
        {
            var types = new[] {
                new EstablishmentType{ Name = "Bank"},
                new EstablishmentType{ Name = "Supermarket"},
                new EstablishmentType{ Name = "Hospital"},
                new EstablishmentType{ Name = "School"},
                new EstablishmentType{ Name = "University"},
                new EstablishmentType{ Name = "Shopping Mall"},
                new EstablishmentType{ Name = "Restaurant"},
                new EstablishmentType{ Name = "Government Office"},
                new EstablishmentType{ Name = "Private Office"},
            };

            foreach (var x in types)
                x.PopulateID();

            return types.SaveAsync();
        }
    }
}
