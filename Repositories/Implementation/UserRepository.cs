using Domains.Data;
using Domains.ViewModels;
using Domains.ViewModels.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<users> _usersCollections;
        private readonly IMongoCollection<stories> _storiesCollections;
        private readonly IMongoCollection<posts> _collection;
        public UserRepository(MongoDbContext dbContext)
        {
            _usersCollections=dbContext.GetMongoCollection<users>("users");
            _storiesCollections = dbContext.GetMongoCollection<stories>("stories");
            _collection = dbContext.GetMongoCollection<posts>("posts");
        }
        public async Task<LogedUserViewModel> GetLogedUserDetails(string userId)
        {
            //by builder approach
            //get logged user details
            users loggedUser=new users();
            var userFilter = Builders<users>.Filter.Eq("_id", ObjectId.Parse(userId));
            var userProjection = Builders<users>.Projection.Exclude(x => x.Password).Exclude(x => x.Password);
            var projectionOptions = new FindOptions<users> { Projection = userProjection };
            loggedUser =  (await _usersCollections.FindAsync(userFilter,projectionOptions)).SingleOrDefault();

            //Get story users from loged user's following
            var matchFilter = Builders<stories>.Filter.In(x=>x.UserId, loggedUser.Following);
            var storyProjection = Builders<stories>.Projection.Include(x=>x.UserId).Exclude(x=>x.Id);
            var storyProjectionOption = new FindOptions<stories> { Projection = storyProjection };
            var storyUserIds = (await _storiesCollections.Find(matchFilter).Project<stories>(storyProjection).ToListAsync()).Select(x => x.UserId).Distinct().ToList();
            var storyUsers = await _usersCollections.Find(Builders<users>.Filter.In(x => x.Id, storyUserIds)).
                                Project<users>(Builders<users>.Projection.Include(x=>x.FirstName).Include(x=>x.LastName).Include(x => x.ProfilePhoto)).ToListAsync();
            return new LogedUserViewModel { LogedUser=loggedUser,StoryUsers= storyUsers, TotalSotries= storyUsers.Count};
        }

        public List<users> GetSuggestedAllies(string logedUserId)
        {
            Random rm = new Random();
            //by linq approach
            users logedUser = new users();
            List<string> logedUserAllies = new List<string>();
            List<string> allAllies = new List<string>();
            IQueryable<users> allSuggestedAllies;
            logedUser = _usersCollections.AsQueryable().Where(x => x.Id == logedUserId)?.FirstOrDefault() is null? logedUser: _usersCollections.AsQueryable().Where(x => x.Id == logedUserId)?.Select(x=>new users() { Following=x.Following,Followers=x.Followers}).FirstOrDefault();
            logedUserAllies = _usersCollections.AsQueryable().Where(x => x.Following.Contains(logedUserId) || x.Followers.Contains(logedUserId)).Select(x=>x.Id).ToList();
            var allLogedUserAllies_Allies = _usersCollections.AsQueryable().Where(x => logedUserAllies.Contains(x.Id)).Select(x => new { Follwing = x.Following, Followers = x.Followers }).ToList();
            foreach (var ally in allLogedUserAllies_Allies)
            {
                allAllies.AddRange(ally.Followers);
                allAllies.AddRange(ally.Follwing);
            }
            allSuggestedAllies = _usersCollections.AsQueryable().Where(x =>
              (
              (allAllies.Contains(x.Id)&&!logedUser.Following.Contains(x.Id)&&x.Id!=logedUserId) || (!allAllies.Contains(x.Id) && logedUser.Followers.Contains(x.Id) && !logedUser.Following.Contains(x.Id) && x.Id != logedUserId))
              ).Select(x => new users() { Id=x.Id,FirstName = x.FirstName, LastName = x.LastName, ProfilePhoto = x.ProfilePhoto });
            int randomInt = rm.Next(allSuggestedAllies.ToList().Count);
            allSuggestedAllies = allSuggestedAllies.Skip(randomInt > 2 ? randomInt - 1 : randomInt).Take(5);
            List<posts> posts = _collection.AsQueryable().ToList();
            return allSuggestedAllies.ToList().OrderBy(x=> rm.Next(allSuggestedAllies.ToList().Count)).ToList();
        }
    }
}
