using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace AssessmentService.Application.Features.MBTIQuestions.Command
{
    public class DeleteMBTIQuestionCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
