using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.PsychologyTest
{
    public class PsychologyTestSubmissionDto
    {
        public int TestId { get; set; }
        public Guid CustomerId { get; set; }

        public List<QuestionAnswerDto> Answers { get; set; }

        public string CustomerIdString
        {
            set
            {
                if (Guid.TryParse(value, out var guid))
                {
                    CustomerId = guid;
                }
                else
                {
                    CustomerId = Guid.Empty; // Or throw an exception
                }
            }
        }
    }
}
