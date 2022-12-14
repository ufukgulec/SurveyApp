using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Status { get; set; } //Gösterilsin veya gösterilmesin
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
