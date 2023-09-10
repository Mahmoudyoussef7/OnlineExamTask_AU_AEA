using OnlineExam.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExam.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(IAnswerRepository answerRepository, IUserRepository userRepository , IChoiceRepository choiceRepository, IUserRoleRepository userRoleRepository, IQuestionRepository questionRepository, IQuestionTypeRepository questionTypeRepository, IExaminationRepository examinationRepository, IStudentProgressRepository studentProgressRepository)
    {
        Answers = answerRepository;
        Users = userRepository;
        UserRoles = userRoleRepository;
        Choices = choiceRepository;
        Questions = questionRepository;
        QuestionTypes = questionTypeRepository;
        Examinations = examinationRepository;
        StudentProgress = studentProgressRepository;
    }

    public IAnswerRepository Answers { get; set; }

    public IChoiceRepository Choices { get; set; }

    public IQuestionRepository Questions { get; set; }

    public IUserRepository Users { get; set; }

    public IQuestionTypeRepository QuestionTypes { get; set; }

    public IExaminationRepository Examinations { get; set; }

    public IStudentProgressRepository StudentProgress { get; set; }

    public IUserRoleRepository UserRoles { get; set; }
}
