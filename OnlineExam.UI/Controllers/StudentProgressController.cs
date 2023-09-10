using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using OnlineExam.UI.Models;

namespace OnlineExam.UI.Controllers;

public class StudentProgressController : Controller
{
    private readonly ILogger<StudentProgressController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public StudentProgressController(ILogger<StudentProgressController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    [HttpGet]
    public async Task<IActionResult> GetExamGrade(Guid examId)
    {
        decimal Score = 0;
        var userId = default(Guid);
        Guid.TryParse(HttpContext.Request.Cookies["UserId"], out userId);

        var examQuestions = await _unitOfWork.Questions.GetByExamIdAsync(examId);

        var answersExamUser = await  _unitOfWork.Answers.GetAnswerExamUser(userId, examId);

        foreach (var answer in answersExamUser)
        {
            if (answer.SelectedChoiceId != null)
            {
                var selectedChoice = await _unitOfWork.Choices.GetByIdAsync(answer.SelectedChoiceId.Value);

                if (selectedChoice != null)
                {
                    Score += 10;
                }
            }
        }
        var progress = new StudentProgress
        {
            ExamId = examId,
            TimeStamp = DateTime.UtcNow,
            UserId = userId,
            Score = Score
        };

        await _unitOfWork.StudentProgress.AddAsync(progress);

        var studentProgrssVM = new StudentProgressVM
        {
            Score = Score,
            TimeStamp = progress.TimeStamp,
            IsCompleted = progress.IsCompleted,
            ExamTitle = _unitOfWork.Examinations.GetByIdAsync(examId).Result.ExamTitle,
            UserName = _unitOfWork.Users.GetByIdAsync(examId).Result.UserName
        };

        return View(studentProgrssVM);
    }
}
