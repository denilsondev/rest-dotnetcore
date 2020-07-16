using System;
using System.Collections.Generic;
using System.Linq;
using RestWithAspNet.Model;
using RestWithAspNet.Model.Context;

namespace RestWithAspNet.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;

        }

        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login.Equals(login));

        }
    }
}
