using System.Collections.Generic;
using RestWithAspNet.Data.Converters;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository.Generic;
using RestWithAspNet.Repository.Implementations;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();

        }
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);           
             _repository.Create(personEntity);
            var personVO = _converter.Parse(personEntity);
            return personVO;
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));

        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            _repository.Update(personEntity);
            var personVO = _converter.Parse(personEntity);
            return personVO;
        }

    }
}
