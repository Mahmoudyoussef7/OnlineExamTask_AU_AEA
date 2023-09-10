using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Interfaces;
using OnlineExam.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>()
        .AddTransient<IExaminationRepository, ExaminationRepository>()
        .AddTransient<IChoiceRepository, ChoiceRepository>()
        .AddTransient<IQuestionRepository, QuestionRepository>()
        .AddTransient<IQuestionTypeRepository, QuestionTypeRepository>()
        .AddScoped<IUserRoleRepository, UserRoleRepository>()
        .AddTransient<IStudentProgressRepository, StudentProgressRepository>()
        .AddTransient<IAnswerRepository, AnswerRepository>()
        .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
