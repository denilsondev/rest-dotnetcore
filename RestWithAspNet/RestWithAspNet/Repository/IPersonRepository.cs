﻿using RestWithAspNet.Model;
using System.Collections.Generic;

namespace RestWithAspNet.Repository
{
    public interface IPersonRepository
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
        bool Exits(long id);
    }
}
