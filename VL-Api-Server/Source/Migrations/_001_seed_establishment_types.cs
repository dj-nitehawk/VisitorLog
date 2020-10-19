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
                new EstablishmentType("Bank"),
                new EstablishmentType("Supermarket"),
                new EstablishmentType("Hospital"),
                new EstablishmentType("School"),
                new EstablishmentType("University"),
                new EstablishmentType("Shopping Mall"),
                new EstablishmentType("Restaurant"),
                new EstablishmentType("Government Office"),
                new EstablishmentType("Private Office")
            };

            return types.SaveAsync();
        }
    }
}
