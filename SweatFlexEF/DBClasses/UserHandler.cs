using Microsoft.Extensions.Logging;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs;
using SweatFlexEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweatFlexData.DTOs.Update;
using Azure.Core;

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
                var users = _context.Users;
                return users.Select(Mapping.Mapper.Map<UserDTO>).ToList();            
        }
        public async Task<IList<UserDTO>> GetUsersByCoachIdAsync(string coachId)
        {
            if(coachId != null)
            {
                var users = _context.Users.Where(u => u.Coach == coachId);
                return users.Select(Mapping.Mapper.Map<UserDTO>).ToList();
            }
            else
            {
                return null;
            }
        }
        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            if(id != null)
            {
                var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
                return Mapping.Mapper.Map<UserDTO>(user);
            }
            else
            {
                return null;
            }
        }
        public async Task<UserDTO> UpdateUserAsync(string id, UserUpdateDTO updateDTO)
        {
            if(id != null && updateDTO != null)
            {
                var user = Mapping.Mapper.Map<User>(updateDTO);
                user.Id = id;
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
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<UserDTO> CreateUserAsync(UserCreateDTO createDTO)
        {
            if(createDTO != null)
            {
                var user = Mapping.Mapper.Map<User>(createDTO);
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return Mapping.Mapper.Map<UserDTO>(user);
            }
            else
            {
                return null;
            }
        }
    }
}
