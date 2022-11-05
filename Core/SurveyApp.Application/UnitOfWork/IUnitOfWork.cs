using SurveyApp.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAnswerRepository AnswerRepository { get; }
        IChoiceRepository ChoiceRepository { get; }
        ICountryRepository CountryRepository { get; }
        IParticipantRepository ParticipantRepository { get; }
        IProfileRepository ProfileRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISurveyRepository SurveyRepository { get; }
        IUserRepository UserRepository { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
