using SurveyApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Survey : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Participant>? Participants { get; set; }
        public ICollection<Question>? Questions { get; set; }

    }
}
