using Gov.Jag.Embc.Public.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Embc.Public.DataInterfaces
{
    public class SqliteDataInterface : IDataInterface
    {
        public SqliteContext Db;

        public SqliteDataInterface(string connectionString)
        {

            DbContextOptionsBuilder<SqliteContext> builder = new DbContextOptionsBuilder<SqliteContext>();

            builder.UseSqlite(connectionString);

            // init the database.
            Db = new SqliteContext(builder.Options);

            Db.Database.OpenConnection();



        }

        public Person CreatePerson(Person person)
        {
            // TODO: Implement
            throw new NotImplementedException();
            //return person;
        }

        public Task<Registration> CreateRegistration(Registration registration)
        {
            var model = registration.ToModel();
            Db.Registrations.Add(model);
            Db.SaveChanges();
            return Task.FromResult(model.ToViewModel());
        }

        public Organization GetOrganizationByBceidGuid(string bceidGuid)
        {
            // TODO: Implement
            Organization result = new Organization();
            return result;
        }

        public Person GetPersonByBceidGuid(string bceidGuid)
        {
            // TODO: Implement
            throw new NotImplementedException();
            //Person result = new Person();
            //return result;
        }

        public Volunteer GetVolunteerByName(string firstName, string lastName)
        {
            Volunteer result = null;
            var item = Db.Volunteers.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
            if (item != null)
            {
                result = item.ToViewModel();
            }
            return result;
        }



        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();
            var countryList = Db.Countries.ToList();
            foreach (var country in countryList)
            {
                countries.Add(country.ToViewModel());
            }
            return countries;
        }

        public Organization GetOrganizationByLegalName(string name)
        {
            Organization result = null;
            var item = Db.Organizations.FirstOrDefault(x => x.Name == name);
            if (item != null)
            {
                result = item.ToViewModel();
            }
            return result;
        }

        public Organization GetOrganizationByExternalId(string externalId)
        {
            Organization result = null;
            var item = Db.Organizations.FirstOrDefault(x => x.Externaluseridentifier == externalId);
            if (item != null)
            {
                result = item.ToViewModel();
            }
            return result;
        }

        public List<Region> GetRegions()
        {
            List<Region> regions = new List<Region>();
            var regionList = Db.Regions.ToList();
            foreach (var region in regionList)
            {
                regions.Add(region.ToViewModel());
            }
            return regions;
        }

        public Task<List<Registration>> GetRegistrations()
        {
            List<Registration> regions = new List<Registration>();
            var registrationList = Db.Registrations.ToList();
            foreach (var registration in registrationList)
            {
                regions.Add(registration.ToViewModel());
            }

            return Task.FromResult(regions);
        }

        public List<RegionalDistrict> GetRegionalDistricts()
        {
            List<RegionalDistrict> regions = new List<RegionalDistrict>();
            var regionalDistrictList = Db.RegionalDistricts.ToList();
            foreach (var regionalDistrict in regionalDistrictList)
            {
                regions.Add(regionalDistrict.ToViewModel());
            }
            return regions;
        }

        public List<Community> GetCommunities()
        {
            List<Community> regions = new List<Community>();
            var communityList = Db.Communities.ToList();
            foreach (var community in communityList)
            {
                regions.Add(community.ToViewModel());
            }
            return regions;
        }

        public List<FamilyRelationshipType> GetFamilyRelationshipTypes()
        {
            var all = Db.FamilyRelationshipTypes.Select(x => x.ToViewModel()).ToList();
            return all;
        }

        public Task<Registration> GetRegistration(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var entity = Db.Registrations.FirstOrDefault(reg => reg.Id == guid);
                return Task.FromResult(entity?.ToViewModel());
            }
            return Task.FromResult<Registration>(null);
        }

        //
        // Incident Tasks
        //

        public Task<List<IncidentTask>> GetIncidentTasks()
        {
            var all = Db.IncidentTasks.Select(task => task.ToViewModel()).ToListAsync();
            return all;
        }

        public Task<IncidentTask> GetIncidentTask(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var entity = Db.IncidentTasks.FirstOrDefault(task => task.Id == guid);
                return Task.FromResult(entity?.ToViewModel());
            }
            return Task.FromResult<IncidentTask>(null);
        }

        public Task<IncidentTask> CreateIncidentTask(IncidentTask task)
        {
            var entity = task.ToModel();
            Db.IncidentTasks.Add(entity);
            Db.SaveChanges();
            return Task.FromResult(entity.ToViewModel());
        }

        public Task<IncidentTask> UpdateIncidentTask(IncidentTask task)
        {
            var entity = Db.IncidentTasks.FirstOrDefault(item => item.Id == new Guid(task.Id));
            entity.PatchValues(task);
            Db.IncidentTasks.Update(entity);
            Db.SaveChanges();
            return Task.FromResult(entity.ToViewModel());
        }

        public Volunteer GetVolunteerByExternalId(string externalId)
        {
            Volunteer result = null;
            var item = Db.Volunteers.FirstOrDefault(x => x.Externaluseridentifier == externalId);
            if (item != null)
            {
                result = item.ToViewModel();
            }
            return result;
        }

        public Volunteer GetVolunteerById(string id)
        {
            Volunteer result = null;
            Guid guid = new Guid(id);
            var item = Db.Volunteers.FirstOrDefault(x => x.Id == guid);
            if (item != null)
            {
                result = item.ToViewModel();
            }
            return result;
        }
    }
}
