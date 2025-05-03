using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTrackingSystem.Application.Common.DTOs;

public class UpdateMediaDto
{
    public int EventId { get; set; }
    public ICollection<IFormFile> NewFiles { get; set; } = null!;
    public ICollection<int> MediaIdsToDelete { get; set; } = new List<int>();
}

