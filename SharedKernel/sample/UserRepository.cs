//using SharedKernel.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SharedKernel.sample
//{
//    public class UserRepository : IRepository<User>
//    {
//        private readonly DbContext _context;

//        public UserRepository(DbContext context)
//        {
//            _context = context;
//        }

//        public async Task<User?> GetByIdAsync(Guid id)
//        {
//            return await _context.Users.FindAsync(id);
//        }

//        public async Task<IEnumerable<User>> GetAllAsync()
//        {
//            return await _context.Users.ToListAsync();
//        }

//        public async Task AddAsync(User entity)
//        {
//            await _context.Users.AddAsync(entity);
//            await _context.SaveChangesAsync();
//        }

//        public void Update(User entity)
//        {
//            _context.Users.Update(entity);
//            _context.SaveChanges();
//        }

//        public void Delete(User entity)
//        {
//            _context.Users.Remove(entity);
//            _context.SaveChanges();
//        }

//        Task<User?> IRepository<User>.GetByIdAsync(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<User>> GetByFilterAsync(Func<User, bool> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<User>> GetPagedAsync(int pageNumber, int pageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public Task ExecuteTransactionAsync(Func<Task> operations)
//        {
//            throw new NotImplementedException();
//        }

//        public IQueryable<User> Query()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
