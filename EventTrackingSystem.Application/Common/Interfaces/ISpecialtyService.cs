using EventTrackingSystem.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTrackingSystem.Application.Common.Interfaces;

public interface ISpecialtyService
{
    Task<IEnumerable<SpecialtyDto>> GetAllAsync();
    Task<SpecialtyDto?> GetByIdAsync(int id);
    Task AddAsync(CreateSpecialtyDto dto);
    Task UpdateAsync(UpdateSpecialtyDto dto);
    Task DeleteAsync(int id);
}
