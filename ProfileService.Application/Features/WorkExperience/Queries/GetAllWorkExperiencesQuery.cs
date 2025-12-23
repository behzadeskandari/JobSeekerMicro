using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Features.WorkExperience.Queries
{
    public record GetAllWorkExperiencesQuery : IRequest<List<ProfileService.Domain.Entities.WorkExperience>>;

}
