using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class DispatchService : IDispatchService
    {
        private readonly IDispatchRepository _dispatchRepository;
        private readonly IUserRepository _userRepository;

        public DispatchService(IDispatchRepository dispatchRepository, IUserRepository userRepository)
        {
            _dispatchRepository = dispatchRepository;
            _userRepository = userRepository;

        }


        public async Task<bool> CompleteRegisteration(DispatchDto model)
        {
            var dispatch = await _dispatchRepository.GetAsync(dispatch => dispatch.User.Email == model.UserDto.Email);
            if (dispatch == null)
            {
                return false;
            }

            var user = new User
            {
                Password = model.UserDto.Password,
                PhoneNumber = model.UserDto.PhoneNumber,
            };
            var adduser = await _userRepository.UpdateAsync(user);
            var newDispatch = new Dispatch()
            {
                UserId = adduser.Id,
            };
            var addDispatch = await _dispatchRepository.UpdateAsync(newDispatch);
            if (addDispatch == null)
            {
                return false;
            }
            else
            {

                return true;
            }
        }


        public async Task<DispatchDto> RegisterDispatch(DispatchDto model, int sellerId)
        {
            var dispatch = await _dispatchRepository.GetAsync(dispatch => dispatch.User.Email == model.UserDto.Email);
            if (dispatch != null)
            {
                return null;
            }

            var user = new User
            {
                Email = model.UserDto.Email,
                FirstName = model.UserDto.FirstName,
                LastName = model.UserDto.LastName,
                Role = Role.Dispatch,
            };
            var adduser = await _userRepository.CreateAsync(user);
            var newDispatch = new Dispatch()
            {

                UserId = adduser.Id,

            };
            var addDispatch = await _dispatchRepository.CreateAsync(newDispatch);
            if (addDispatch == null)
            {
                return null;
            }
            else
            {

                return new DispatchDto()
                {
                    Id = addDispatch.Id,
                    UserDto = new UserDto()
                    {
                        Id = addDispatch.User.Id,
                        FirstName = addDispatch.User.FirstName,
                        LastName = addDispatch.User.LastName,
                        Email = addDispatch.User.Email,
                        Role = addDispatch.User.Role
                    }
                };
            }
        }

        public async Task<List<DispatchDto>> GetDispatchesBySellerId(int id)
        {
            var dispatches = await _dispatchRepository.GetDispatches(id);
            if (dispatches == null)
            {
                return null;
            }
            return dispatches.Select(x => new DispatchDto
            {
                Id = x.Id,
                UserDto = new UserDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    PhoneNumber = x.User.PhoneNumber,
                    Role = x.User.Role,
                },
            }).ToList();
        }



        public async Task<bool> DeleteDispatchAsync(int id)
        {
            var dispatch = await _dispatchRepository.GetDispatch(id);
            if (dispatch == null)
            {
                return false;
            }
            await _dispatchRepository.DeleteAsync(dispatch);
            return true;
        }
        public async Task<DispatchDto> GetById(int id)
        {
            var dispatch = await _dispatchRepository.GetDispatch(id);
            if (dispatch == null)
            {
                return null;
            }

            return new DispatchDto()
            {
                UserDto = new UserDto
                {
                    Id = dispatch.User.Id,
                    FirstName = dispatch.User.FirstName,
                    LastName = dispatch.User.LastName,
                    Email = dispatch.User.Email,
                    PhoneNumber = dispatch.User.PhoneNumber,
                    Role = dispatch.User.Role,
                },
                Id = dispatch.Id

            };
        }

        public async Task<DispatchDto> GetByEmail(string email)
        {
            var dispatch = await _dispatchRepository.GetDispatch(email);
            if (dispatch == null)
            {
                return null;
            }

            return new DispatchDto()
            {
                UserDto = new UserDto
                {
                    Id = dispatch.User.Id,
                    FirstName = dispatch.User.FirstName,
                    LastName = dispatch.User.LastName,
                    Email = dispatch.User.Email,
                    PhoneNumber = dispatch.User.PhoneNumber,
                    Role = dispatch.User.Role,
                },
                Id = dispatch.Id
            };
    }
    public async Task<bool> UpdateDispatch(DispatchDto updatedDispatch, int id)
    {
        var dispatch = await _dispatchRepository.GetDispatch(id);
        if (dispatch == null)
        {
            return false;
        }
        dispatch.User.FirstName = updatedDispatch.UserDto.FirstName ?? dispatch.User.FirstName;
        dispatch.User.LastName = updatedDispatch.UserDto.LastName ?? dispatch.User.LastName;
        dispatch.User.PhoneNumber = updatedDispatch.UserDto.PhoneNumber ?? dispatch.User.PhoneNumber;

        await _dispatchRepository.UpdateAsync(dispatch);
        return true;
    }
}
}