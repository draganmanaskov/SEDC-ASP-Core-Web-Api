using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Models;

namespace SEDC.Loto3000App.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {

        private Loto3000DbContext _context;

        public UserRepository(Loto3000DbContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();

        }

        public User AddAndReturn(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _context.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower()
            && x.Password == hashedPassword);
        }
    }
}
