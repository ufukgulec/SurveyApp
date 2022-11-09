using SurveyApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Answer:BaseEntity
    {
        public Guid ParticipantId { get; set; }
        public Participant? Participant { get; set; }
        public Guid ChoiceId { get; set; }
        public Choice? Choice { get; set; }
    }
}
