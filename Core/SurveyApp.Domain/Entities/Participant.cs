using SurveyApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Participant : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid SurveyId { get; set; }
        public Survey? Survey { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
