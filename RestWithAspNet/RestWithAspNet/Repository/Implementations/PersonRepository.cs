using System;
using System.Collections.Generic;
using System.Linq;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;

namespace RestWithAspNet.Repository.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private MySQLContext _context;

        public PersonRepository(MySQLContext context)
        {
            _context = context;

        }
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            try
            {
                if(result != null) _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

        }

        public Person Update(Person person)
        {
            if (!Exits(person.Id)) return new Person();

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return person;
        }

        public bool Exits(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}
