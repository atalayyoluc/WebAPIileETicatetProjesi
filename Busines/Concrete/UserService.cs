using Busines.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOS.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines.Concrete
{
    public class UserService :IUserService
    {
        private readonly IUserDal _userDal;
        public UserService(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<UserDto> AddAsync(UserAddDTO userAddDTO)
        {

            User user = new User()
            {
                UserName = userAddDTO.UserName,
                Email = userAddDTO.Email,
                Address = userAddDTO.Address,
                FirstName = userAddDTO.FirstName,
                LastName = userAddDTO.LastName,
                Password = userAddDTO.Password,
                Gender = userAddDTO.Gender,
                DateOfBirth = userAddDTO.DateOfBirth,
                CreatedUserId = 1,
                CreatedDate = DateTime.Now
                
            };

            var userAdd = await _userDal.AddAsync(user);
            UserDto userDto = new UserDto()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Id = user.Id,
                
            };
            return userDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _userDal.GetAsync(x => x.Id == id);
            if (response != null)
            {
                return await _userDal.DeleteAsync(id);
            }
            return false;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var response = await _userDal.GetAsync(x=>x.Id==id);
            if (response != null)
            {
                UserDto user = new UserDto()
                {
                    Id = id,
                    LastName = response.LastName,
                    FirstName = response.FirstName,
                    Email = response.Email,
                    Address = response.Address,
                    Gender = response.Gender,
                    DateOfBirth = response.DateOfBirth,
                    UserName = response.UserName,
                };
                return user;
            }
            return null;
        }

        public async Task<IEnumerable<UserDetailDto>> GetListAsync()
        {
            List<UserDetailDto> list = new List<UserDetailDto>();
            var response = await _userDal.GetListAsync();
            if (response != null)
            {
                foreach (var item in response.ToList())
                {
                    list.Add(new UserDetailDto()
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Gender = item.Gender == true ? "Erkek" : "Kadın",
                        DateOfBirth = item.DateOfBirth,
                        UserName = item.UserName,
                        Address = item.Address,
                        Email = item.Email,
                        Id = item.Id,

                    });
                }
                return list;
            }
            return null;
        }

        public async Task<UserUpdateDto> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var getUser=await _userDal.GetAsync(x=>x.Id==userUpdateDto.Id);
            if (getUser != null)
            {
                User user = new()
                {
                    FirstName = userUpdateDto.FirstName,
                    LastName = userUpdateDto.LastName,
                    Password = userUpdateDto.Password,
                    Email = userUpdateDto.Email,
                    Address = userUpdateDto.Address,
                    UserName = userUpdateDto.UserName,
                    Id = userUpdateDto.Id,
                    CreatedDate = getUser.CreatedDate,
                    CreatedUserId = getUser.CreatedUserId,
                    DateOfBirth = getUser.DateOfBirth,
                    Gender = userUpdateDto.Gender,
                    UpdateDate = DateTime.Now,
                    UpdateUserId = 1,
                };

                var userUpdate = await _userDal.UpdateAsync(user);
                UserUpdateDto newUserupdate = new()
                {

                    FirstName = userUpdate.FirstName,
                    LastName = userUpdate.LastName,
                    Password = userUpdate.Password,
                    Email = userUpdate.Email,
                    Address = userUpdate.Address,
                    UserName = userUpdate.UserName,
                    Id = userUpdate.Id,

                    DateOfBirth = userUpdate.DateOfBirth,
                    Gender = userUpdate.Gender,

                };
                return newUserupdate;
            }
            return null;
        }
    } }