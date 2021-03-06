using Gov.Jag.Embc.Public.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Embc.Public.DataInterfaces
{
    public partial class DataInterface
    {
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return (await db.Countries
                .Where(c => c.Active)
                .OrderBy(c => c.CountryCode)
                .ToArrayAsync())
                .Select(mapper.Map<Country>);
        }

        public async Task<IEnumerable<Region>> GetRegionsAsync()
        {
            return (await db.Regions.ToArrayAsync())
                .Where(r => r.Active)
                .OrderBy(r => r.Name)
                .Select(mapper.Map<Region>);
        }

        public async Task<IEnumerable<Community>> GetCommunitiesAsync()
        {
            return (await db.Communities
                .Where(c => c.Active)
                .Include(d => d.Region)
                .OrderBy(c => c.Name)
                .ToArrayAsync())
                .Select(mapper.Map<Community>);
        }

        public async Task<IEnumerable<FamilyRelationshipType>> GetFamilyRelationshipTypesAsync()
        {
            return (await db.FamilyRelationshipTypes
                .Where(t => t.Active)
                .ToArrayAsync())
                .Select(mapper.Map<FamilyRelationshipType>);
        }
    }
}
