using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using SurveyApp.Application.UnitOfWork;
using SurveyApp.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace SurveyApp.API.Controllers
{
    public abstract class BaseController<T> : ControllerBase where T : class
    {
        protected Logger logger;
        protected readonly IUnitOfWork service;

        public BaseController(IUnitOfWork service)
        {
            logger = new LoggerConfiguration()
                            .WriteTo.File("logs/log.txt",
                            rollingInterval: RollingInterval.Day,
                            retainedFileCountLimit: null,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                            .CreateLogger();
            this.service = service;
        }

    }
}
