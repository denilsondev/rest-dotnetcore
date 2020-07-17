﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;
using RestWithAspNet.Repository.Generic;

namespace RestWithAspNet.Repository.Implementations
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context)
        {

        }
        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Equals(firstName) && p.LastName.Equals(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Equals(firstName)).ToList();
            }
            else if (string.IsNullOrEmpty(firstName) && !(string.IsNullOrEmpty(lastName)))
            {
                return _context.Persons.Where(p => p.LastName.Equals(lastName)).ToList();
            }
            else
            {
                return _context.Persons.ToList();
            }
        }
    }
}
