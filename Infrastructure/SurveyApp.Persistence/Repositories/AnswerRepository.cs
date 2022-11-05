using Microsoft.EntityFrameworkCore;
using SurveyApp.Application.Repositories;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistence.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(SurveyAppContext context) : base(context)
        {
        }

    }
}
