using SurveyApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SurveyApp.Domain.Entities
{
    public class Country : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string FlagUrl { get; set; }
        //public string? Latitude { get; set; }
        //public string? Longitude { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
