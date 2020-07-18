﻿using System.Collections.Generic;
using RestWithAspNet.Data.Converters;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository;
using RestWithAspNet.Repository.Implementations;
using Tapioca.HATEOAS.Utils;

namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusiness : IPersonBusiness
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository)
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

        public PagedSearchDTO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pagesize, int page)
        {
            page = page > 0 ? page = 1 : 0;
            var query = @"select * from Persons p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) query = query + $"and p.firstName like '%{name}%'";
            query = query + $"order by p.firstName {sortDirection} limit {pagesize} offset {page}";

            string countQuery = @"select count(*) from Persons p where 1 = 1 ";
            if (!string.IsNullOrEmpty(name)) countQuery = countQuery + $"and p.firstName like '%{name}%'";

            var persons =_repository.FindWithPagedSearch(query);
            int totalResults = _repository.GetCount(countQuery);

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = _converter.ParseList(persons),
                PageSize = pagesize,
                SortDirections = sortDirection,
                TotalResults = totalResults
            };
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

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName, lastName));
        }
    }
}
