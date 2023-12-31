﻿using Microsoft.EntityFrameworkCore;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexEF.Models;

namespace SweatFlexEF.DBClasses
{
    public class UserHandler
    {
        SweatFlexContext _context;

        public UserHandler(SweatFlexContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Reads all Users from the Database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UserDTO>> GetUsersAsync()
        {
                var users = await _context.Users.ToListAsync();
                return users.Select(Mapping.Mapper.Map<UserDTO>).ToList();            
        }

        /// <summary>
        /// Reads a List of Users from the Database, depending on the CoachId
        /// </summary>
        /// <param name="coachId">CoachId</param>
        /// <returns></returns>
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

        /// <summary>
        /// Reads 1 User from the Database, depending on the UserId
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
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

        /// <summary>
        /// Reads 1 User from the Database, depending on the Users EMail
        /// </summary>
        /// <param name="eMail">User EMail</param>
        /// <returns></returns>
        public async Task<UserDTO> GetUserByMailAsync(string eMail)
        {
            var user = await _context.Users.Where(u => u.Email == eMail).FirstOrDefaultAsync();
            return Mapping.Mapper.Map<UserDTO>(user);
        }

        /// <summary>
        /// Updates 1 User in the Database, depending on the UserId
        /// </summary>
        /// <param name="id">UserId</param>
        /// <param name="updateDTO">Data for update</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes 1 User from the Database
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates 1 User in the Database with corresponding PasswordDepot
        /// </summary>
        /// <param name="createDTO">Data for creation</param>
        /// <returns></returns>
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
                createDTO.Email.ToLower(),
                createDTO.Password,
                createDTO.CoachId,
                createDTO.Salt,
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

        /// <summary>
        /// Validates a UserLogin against the Database
        /// </summary>
        /// <param name="loginDTO">Data for Login validation</param>
        /// <returns></returns>
        public async Task<LoginDTO> Login(LoginDTO loginDTO)
        {
            User user = await _context.Users.Where(u => u.Email == loginDTO.Email.ToLower()).FirstOrDefaultAsync();

            return new LoginDTO()
            {
                Email = loginDTO.Email.ToLower(),
                Password = user.Password.Password,
                Salt = user.Password.Salt
            };
        }

        /// <summary>
        /// Set 1 User inaktive in the Database
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
        public async Task<bool> SetUserInactive(string id)
        {
            var user = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            user.IsActive = false;
            _context.Users.Update(user);
            _context.SaveChanges();
            var updatedUser = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            return !(bool)updatedUser.IsActive;
        }
    }
}
