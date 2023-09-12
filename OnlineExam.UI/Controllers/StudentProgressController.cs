using Microsoft.AspNetCore.Mvc;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using OnlineExam.UI.Custom;
using OnlineExam.UI.Models;

namespace OnlineExam.UI.Controllers;

[StudentAuth]
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
    public async Task<IActionResult> GetProgressDetails(Guid ExamId)
    {
        var userId = Guid.TryParse(HttpContext.Request.Cookies["UserId"], out var parsedUserId) ? parsedUserId : default;
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        var exam = await _unitOfWork.Examinations.GetByIdAsync(ExamId);
        var totalScore = _unitOfWork.Questions.GetByExamIdAsync(ExamId).Result.Sum(q => q.Points);
        var progress = await _unitOfWork.StudentProgress.GetExamProgressByUserId(userId, ExamId);

        var progressVM = new StudentProgressVM
        {
            ExamId = exam.Id,
            ExamTitle = exam.ExamTitle,
            StudentScore = progress.Score,
            Timestamp = progress.Timestamp,
            UserName = user.FirstName + " " + user.LastName,
            TotalScore = totalScore,
        };
        return View(progressVM);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProgressList()
    {
        var userId = Guid.TryParse(HttpContext.Request.Cookies["UserId"], out var parsedUserId) ? parsedUserId : default;
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        var exams = (await _unitOfWork.Answers.GetAllAsync()).Select(a => a.ExamId).ToList()
            .Select(async item => await _unitOfWork.Examinations.GetByIdAsync(item))
            .Select(task => task.Result)
            .ToList();

        var progressList = new List<StudentProgressVM>();

        foreach (var exam in exams)
        {
            var totalScore = (await _unitOfWork.Questions.GetByExamIdAsync(exam.Id)).Sum(q => q.Points);
            var progress = await _unitOfWork.StudentProgress.GetExamProgressByUserId(userId, exam.Id);

            var progressVM = new StudentProgressVM
            {
                ExamId = exam.Id,
                ExamTitle = exam.ExamTitle,
                StudentScore = progress.Score,
                Timestamp = progress.Timestamp,
                UserName = $"{user.FirstName} {user.LastName}",
                TotalScore = totalScore
            };

            progressList.Add(progressVM);
        }

        return View(progressList);
    }
}
