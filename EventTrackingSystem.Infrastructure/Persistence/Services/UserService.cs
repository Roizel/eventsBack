using AutoMapper;
using EventTrackingSystem.Application.Common.DTOs;
using EventTrackingSystem.Application.Common.Interfaces;
using EventTrackingSystem.Domain.Constants;
using EventTrackingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EventTrackingSystem.Infrastructure.Persistence.Services
{
    public class UserService(
         IUserRepository repository,
         IMapper mapper,
         IImageService imageService,
         IJwtTokenService jwtTokenService,
         UserManager<UserEntity> userManager,
         RoleManager<RoleEntity> roleManager
    ) : IUserService
    {
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var entity = await repository.GetByIdAsync(id);

            return mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var entities = await repository.GetAllAsync();

            return mapper.Map<List<UserDto>>(entities);
        }

        public async Task DeleteUserAsync(int id)
        {
            UserEntity? user = await userManager.FindByIdAsync(id.ToString());

            if (user is null)
                throw new Exception("User not found");

            var result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception("Failed to delete user");
        }

        public async Task<string> AddUserAsync(SignUpDto user)
        {
            UserEntity newUser = mapper.Map<UserEntity>(user);

            if (user.Image != null)
                newUser.Photo = await imageService.SaveImageAsync(user.Image);

            try
            {
                var identityResult = await userManager.CreateAsync(newUser, user.Password);
                if (!identityResult.Succeeded)
                    throw new Exception("Failed to create user");
            }
            catch (Exception)
            {
                throw new Exception("Failed to create user");
            }
            // Отримуємо роль за переданим ID
            var role = user.RoleId != null
                ? await roleManager.FindByIdAsync(user.RoleId)
                : null;

            var roleName = role?.Name ?? Roles.Guest;

            var roleResult = await userManager.AddToRoleAsync(newUser, roleName);
            if (!roleResult.Succeeded)
                throw new Exception("Failed to assign role to user");

            return await jwtTokenService.CreateTokenAsync(newUser);
        }

        public async Task<string> SignInAsync(SignInDto model)
        {
            UserEntity? user = await userManager.FindByEmailAsync(model.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new UnauthorizedAccessException("Wrong authentication data");

            if (user.LockoutEnd > DateTimeOffset.Now)
            {
                throw new UnauthorizedAccessException("Your account is currently blocked. Please try again later.");
            }

            return await jwtTokenService.CreateTokenAsync(user);
        }
    }
}
