using SurveyApp.Application.Repositories;
using SurveyApp.Application.UnitOfWork;
using SurveyApp.Domain.Entities;
using SurveyApp.Persistence.Contexts;
using SurveyApp.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApp.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        SurveyAppContext context;
        //Fields
        public IAnswerRepository? _answerRepository;
        public IChoiceRepository? _choiceRepository;
        public ICountryRepository? _countryRepository;
        public IParticipantRepository? _participantRepository;
        public IProfileRepository? _profileRepository;
        public IQuestionRepository? _questionRepository;
        public IRoleRepository? _roleRepository;
        public ISurveyRepository? _surveyRepository;
        public IUserRepository? _userRepository;
        private bool disposedValue;

        public UnitOfWork(SurveyAppContext _context)
        {
            context = _context;
        }

        //Properties
        public IAnswerRepository AnswerRepository => _answerRepository ?? (_answerRepository = new AnswerRepository(context));

        public IChoiceRepository ChoiceRepository => _choiceRepository ?? (_choiceRepository = new ChoiceRepository(context));

        public ICountryRepository CountryRepository => _countryRepository ?? (_countryRepository = new CountryRepository(context));

        public IParticipantRepository ParticipantRepository => _participantRepository ?? (_participantRepository = new ParticipantRepository(context));

        public IProfileRepository ProfileRepository => _profileRepository ?? (_profileRepository = new ProfileRepository(context));

        public IQuestionRepository QuestionRepository => _questionRepository ?? (_questionRepository = new QuestionRepository(context));

        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(context));

        public ISurveyRepository SurveyRepository => _surveyRepository ?? (_surveyRepository = new SurveyRepository(context));

        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(context));

        public int SaveChanges()
        {
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    dbTransaction.Commit();
                    return context.SaveChanges();
                }
                catch (Exception)
                {
                    dbTransaction?.Rollback();
                    return 0;
                }
            }

        }

        public async Task<int> SaveChangesAsync()
        {
            using (var dbTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    dbTransaction.Commit();
                    return await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    dbTransaction?.Rollback();
                    return 0;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: yönetilen durumu (yönetilen nesneleri) atın
                }

                // TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve sonlandırıcıyı geçersiz kılın
                // TODO: büyük alanları null olarak ayarlayın
                disposedValue = true;
            }
        }

        // // TODO: sonlandırıcıyı yalnızca 'Dispose(bool disposing)' içinde yönetilmeyen kaynakları serbest bırakacak kod varsa geçersiz kılın
        // ~UnitOfWork()
        // {
        //     // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
