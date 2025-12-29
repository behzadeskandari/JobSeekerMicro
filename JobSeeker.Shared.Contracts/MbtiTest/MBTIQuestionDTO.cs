using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.MbtiTest
{
    public class MBTIQuestionDTO
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string Category { get; set; }
        public bool? IsActive { get; set; }

        public List<AnswerDtoMBTI> Answers { get; set; }

    }

}
