using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.MbtiTest
{
    public class AnswerDtoMBTI
    {
        public Guid QuestionId { get; set; }
        public int Score { get; set; } // 1 = yes, 0 = no
    }
}
