using SurveyApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }
        public string? Description { get; set; }
        public Guid SurveyId { get; set; }
        public Survey? Survey { get; set; }
        public ICollection<Choice>? Choices { get; set; }
    }
}
