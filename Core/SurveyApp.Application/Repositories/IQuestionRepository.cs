using SurveyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.Repositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
    }   
}
