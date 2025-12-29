using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.MbtiTest;

namespace JobSeeker.Shared.Contracts.MbtiTest
{
    public class SubmitRequestMBTI
    {
        public int TestId { get; set; }
        public string CustomerId { get; set; }
        public List<AnswerDtoMBTI> Answers { get; set; }
    }
}
