using Domains.ViewModels;
using MongoDB.Bson;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class Service<T>:IService<T> where T:class
    {
        private readonly IRepository<T> _repository;
        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<IEnumerable<T>> GetDataByField(string fieldName, string fieldValue)
        {
            return await _repository.GetDataByField(fieldName, fieldValue);
        }
    }
}
