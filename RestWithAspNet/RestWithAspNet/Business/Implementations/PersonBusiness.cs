using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.Generic;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private IRepository<Person> _repository;

        public PersonBusiness(IRepository<Person> repository)
        {
            _repository = repository;

        }
        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);

        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

    }
}
