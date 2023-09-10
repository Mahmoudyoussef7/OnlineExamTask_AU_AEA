using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using OnlineExam.UI.Models;

namespace OnlineExam.UI.Controllers;

public class AnswerController : Controller
{
    private readonly ILogger<AnswerController> _logger;
    private readonly IUnitOfWork _unitOfWork;
  
    public AnswerController(ILogger<AnswerController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> ListExams()
    {
        ViewBag.Exams = new SelectList(await _unitOfWork.Examinations.GetAllAsync());
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetExam(Guid examId, int pageNumber = 1)
    {
        var userId = default(Guid);
        const int size = 1;

        Guid.TryParse(HttpContext.Request.Cookies["UserId"], out userId);

        var QuestionsList = await _unitOfWork.Questions.GetByExamIdAsync(examId);
        var QuestionsCount = QuestionsList.Count();
        var question = GetQuestionsPerPage(QuestionsList, pageNumber, size);
        var answer = await _unitOfWork.Answers.GetAnswerStudentForQuestion(userId, examId, question.Id);
        var choices = await _unitOfWork.Choices.GetChoicesOfQuestion(question.Id);
        var TotalPages = (int)Math.Ceiling(QuestionsCount / (double)size);


        var model = new QuestionExamAnswerViewModel()
        {
            Question = question,
            Answer = answer,
            Count = QuestionsCount,
            TotalPages = TotalPages,
            PageNumber = pageNumber,
            ExamId = examId,
            Choices = choices,
            CorrectChoiceId = choices.FirstOrDefault(c => c.IsCorrect).Id,
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitAnswer(QuestionExamAnswerViewModel model)
    {
        var userId = default(Guid);
        Guid.TryParse(HttpContext.Request.Cookies["userId"], out userId);

        var Question = await _unitOfWork.Questions.GetByIdAsync(model.Question.Id);

        model.Answer.QuestionId = Question.Id;
        model.Answer.UserId = userId;
        model.Answer.ExamId = model.ExamId;

        await _unitOfWork.Answers.AddAsync(model.Answer);

        return RedirectToAction("SubmitAnswer", "Mangment", new { ExamId = model.ExamId, pageNumber = model.PageNumber + 1 });
    }





    private Question GetQuestionsPerPage(IEnumerable<Question> allQuestions, int pageNumber, int pageSize)
    {
        int startIndex = (pageNumber - 1) * pageSize;

        var pageQuestions = allQuestions.Skip(startIndex).Take(pageSize);

        return pageQuestions.FirstOrDefault();
    }
}
