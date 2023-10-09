using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexEF.Models;
using Microsoft.EntityFrameworkCore;

namespace SweatFlexEF.DBClasses
{
    public class UserHandler
    {
        SweatFlexContext _context;

        public UserHandler(SweatFlexContext context)
        {
            _context = context;
        }

        public async Task<IList<UserDTO>> GetUsersAsync()
        {
                var users = await _context.Users.ToListAsync();
                return users.Select(Mapping.Mapper.Map<UserDTO>).ToList();            
        }
        public async Task<IList<UserDTO>?> GetUsersByCoachIdAsync(string coachId)
        {
            if(coachId != null)
            {
                var users = await _context.Users.Where(u => u.CoachId == coachId).ToListAsync();
                return users.Select(Mapping.Mapper.Map<UserDTO>).ToList();
            }
            else
            {
                return null;
            }
        }
        public async Task<UserDTO?> GetUserByIdAsync(string id)
        {
            if(id != null)
            {
                var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
                return Mapping.Mapper.Map<UserDTO>(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserDTO> GetUserByMailAsync(string eMail)
        {
            var user = await _context.Users.Where(u => u.Email == eMail).FirstOrDefaultAsync();
            return Mapping.Mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO?> UpdateUserAsync(string id, UserUpdateDTO updateDTO)
        {
            if(id != null && updateDTO != null)
            {
                var user = _context.Users
                    .Include(u => u.Password)
                    .Where(u => u.Id == id).FirstOrDefault();

                if (user == null)
                {
                    return null;
                }

                user.FirstName = updateDTO.FirstName;
                user.LastName = updateDTO.LastName;
                user.Email = updateDTO.Email;
                user.IsActive = updateDTO.IsActive;
                user.CoachId = updateDTO.Coach;
                user.Password.Password = updateDTO.Password;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
                return Mapping.Mapper.Map<UserDTO>(user);
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> DeleteUserAsync(string id)
        {
            if(id != null)
            {
                var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();

                if (user == null)
                {
                    return false;
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<UserDTO?> CreateUserAsync(UserCreateDTO createDTO)
        {
            var spContext = new SweatFlexContextProcedures(_context);
            var outputParam = new OutputParameter<int>();

            var cts = new CancellationTokenSource();
            var ct = cts.Token;

            var task = spContext.CreateUserAsync(
                createDTO.Id,
                createDTO.Role,
                createDTO.FirstName,
                createDTO.LastName,
                createDTO.Email,
                createDTO.Password,
                createDTO.CoachId,
                outputParam,
                ct
                );

            if (!task.Wait(10000))
            {
                cts.Cancel();
                return null;
            }
            //else if(!outputParam.)

            var user = _context.Users.Where(u => u.Id == createDTO.Id).FirstOrDefault();

            return Mapping.Mapper.Map<UserDTO>(user);         
        }

        public async Task<UserDTO> Login(string eMail, string password)
        {
            var user = await _context.fn_ValidatLogin(eMail, password).FirstOrDefaultAsync();

            var dtoUser = Mapping.Mapper.Map<UserDTO>(user);            

            var coach = await _context.Users.Where(u => u.Id == user.CoachId).FirstOrDefaultAsync();
            dtoUser.Coach = Mapping.Mapper.Map<UserDTO>(coach);

            return dtoUser;
        }
    }
}
