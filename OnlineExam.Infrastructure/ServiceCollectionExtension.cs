using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Interfaces;
using OnlineExam.Infrastructure.Repositories;

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
        .AddTransient<IQuestionChoicesRepository, QuestionChoicesRepository>()
        .AddScoped<IUserRoleRepository, UserRoleRepository>()
        .AddTransient<IStudentProgressRepository, StudentProgressRepository>()
        .AddTransient<IAnswerRepository, AnswerRepository>()
        .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
