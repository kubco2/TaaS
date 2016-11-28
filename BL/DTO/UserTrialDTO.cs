using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class UserTrialDTO
    {
        public int Id { get; set; }
        [Required]
        public TestTemplateDTO Test { get; set; }
        public UserDTO User { get; set; }
        public List<QuestionDTO> Questions { get; set; }
        public double Score { get; set; }
        [Required]
        public DateTime Generated { get; set; }
        public DateTime? Taken { get; set; }
        public DateTime? Closed { get; set; }
        public List<AnswerDTO> Answers { get; set; }

        public UserTrialDTO()
        {
            Generated = DateTime.Now;
            Questions = new List<QuestionDTO>();
            Answers = new List<AnswerDTO>();
        }

        public int CheckAnswer(AnswerDTO answer)
        {
            if(Answers.Contains(answer))
            {
                if(answer.Score > 0)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
