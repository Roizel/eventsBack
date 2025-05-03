using EventTrackingSystem.Domain.Entities;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface IJwtTokenService
{
    Task<string> CreateTokenAsync(UserEntity user);
}
