﻿namespace OnlineExam.Application.Interfaces;

public interface IUnitOfWork
{
    IAnswerRepository Answers { get; }
    IChoiceRepository Choices { get; }
    IQuestionRepository Questions { get; }
    IUserRepository Users { get; }
    IQuestionTypeRepository QuestionTypes { get; }
    IExaminationRepository Examinations { get; }
    IStudentProgressRepository StudentProgress { get; }
    IUserRoleRepository UserRoles { get; }
    IQuestionChoicesRepository QuestionChoices { get; }
}
