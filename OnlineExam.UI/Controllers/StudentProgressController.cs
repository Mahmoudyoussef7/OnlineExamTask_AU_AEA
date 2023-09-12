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
        var exams = await _unitOfWork.Examinations.GetExaminationsByUserId(userId);
        var ProgressList = new List<StudentProgressVM>();
        foreach (var item in exams)
        {
            var totalScore = _unitOfWork.Questions.GetByExamIdAsync(item.Id).Result.Sum(q => q.Points);
            var progress = await _unitOfWork.StudentProgress.GetExamProgressByUserId(userId, item.Id);

            var progressVM = new StudentProgressVM
            {
                ExamId = item.Id,
                ExamTitle = item.ExamTitle,
                StudentScore = progress.Score,
                Timestamp = progress.Timestamp,
                UserName = user.FirstName + " " + user.LastName,
                TotalScore = totalScore,
            };
            ProgressList.Append(progressVM);
        }
        return View(ProgressList);
    }
}
