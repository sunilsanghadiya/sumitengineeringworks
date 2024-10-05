using sew.Database;
using sew.Entities;
using sew.Models.Dtos;

namespace sew.Repository;

public interface IUserRepository
{
    Task<List<Users>> GetUsers();
    Task<Users?> GetUserByID(int id);
    Task<Users> SaveUser(Users users);
    Task<Users> UpdateUser(Users users);
    Task<Users> LoginUser(LoginDto loginDto);
    Task<Users?> Register(Users users);
    Task<Users?> GetUserByEmailId(string emailId);
}

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Users>> GetUsers() 
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<Users?> GetUserByID(int id) 
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
    }

    public async Task<Users> SaveUser(Users users) 
    {
        if(users.ID <= 0) 
        {
            _context.Users.Add(users);
        }
        await _context.SaveChangesAsync();
        return users;
    }

    public async Task<Users> UpdateUser(Users users) 
    {
        if(users.ID > 0) 
        {
            _context.Users.Update(users);
        }
        await _context.SaveChangesAsync();
        return users;
    }

    public async Task<Users> LoginUser(LoginDto loginDto)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
    }

    public async Task<Users?> Register(Users users)
    {
        if(users.ID == 0)
        {
            _context.Users.Add(users);
        }
        else 
        {
            _context.Users.Update(users);
        }

        await _context.SaveChangesAsync();

        return users;
    }

    public async Task<Users?> GetUserByEmailId(string emailId) 
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == emailId && !u.IsDeleted);
    }
}
