using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using OnlineExam.UI.Custom;
using OnlineExam.UI.Helper;
using OnlineExam.UI.Models;

namespace OnlineExam.UI.Controllers;

[StudentAuth]
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
        const int pageSize = 1;

        var userId = Guid.TryParse(HttpContext.Request.Cookies["UserId"], out var parsedUserId) ? parsedUserId : default;

        var questionsList = await _unitOfWork.Questions.GetByExamIdAsync(examId);
        var questionsCount = questionsList.Count();
        var question = GetQuestionsPerPage(questionsList, pageNumber, pageSize);
        var answer = await _unitOfWork.Answers.GetAnswerStudentForQuestion(question.Id, userId, examId);
        var questionChoices = await _unitOfWork.QuestionChoices.GetChoiceIdsByQuestionIdAsync(question.Id);
        var choicesList = new List<Choice>();

        foreach (var id in questionChoices)
        {
            var choice = await _unitOfWork.Choices.GetByIdAsync(id);
            if (choice != null)
                choicesList.Add(choice);
        }

        var totalPages = (int)Math.Ceiling(questionsCount / (double)pageSize);

        var model = new QuestionAnswerViewModel
        {
            Question = question,
            Count = questionsCount,
            TotalPages = totalPages,
            PageNumber = pageNumber,
            ExamId = examId,
            Choices = choicesList,
            Answer = answer ?? new Answer(),
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitAnswer(QuestionAnswerViewModel model)
    {
        var userId = Guid.TryParse(HttpContext.Request.Cookies["UserId"], out var parsedUserId) ? parsedUserId : default;

        model.Answer.QuestionId = model.Question.Id;
        model.Answer.UserId = userId;
        model.Answer.ExamId = model.ExamId;

        await _unitOfWork.Answers.AddAsync(model.Answer);

        return RedirectToAction("SubmitAnswer", "Mangment", new { ExamId = model.ExamId, pageNumber = model.PageNumber + 1 });
    }

    [HttpPost]
    public async Task<IActionResult> FinishExam(Guid examId)
    {
        var userId = Guid.TryParse(HttpContext.Request.Cookies["UserId"], out var parsedUserId)? parsedUserId : default;

        var progress = new StudentProgress
        {
            ExamId = examId,
            Timestamp = DateTime.UtcNow,
            UserId = userId
        };

        var examQuestions = await _unitOfWork.Questions.GetByExamIdAsync(examId);

        foreach (var question in examQuestions)
        {
            var studentAnswerOfQuestion = await _unitOfWork.Answers.GetAnswerStudentForQuestion(question.Id, userId, examId);

            if (question.QuestionTypeId == (int)QuestionTypeEnum.Essay || question.QuestionTypeId == (int)QuestionTypeEnum.Complete)
            {
                if (question.CorrectAnswer == studentAnswerOfQuestion.AnswerText)
                    progress.Score += question.Points;
            }
            else
            {
                var selectedChoice = await _unitOfWork.Choices.GetByIdAsync(studentAnswerOfQuestion.SelectedChoiceId.Value);

                if (question.CorrectAnswer == selectedChoice.ChoiceText)
                    progress.Score += question.Points;
            }
        }

        return RedirectToAction("GetProgressDetails", "StudentProgress", new { ExamId = examId });
    }

    private Question GetQuestionsPerPage(IEnumerable<Question> allQuestions, int pageNumber, int pageSize)
    {
        int startIndex = (pageNumber - 1) * pageSize;

        var pageQuestions = allQuestions.Skip(startIndex).Take(pageSize);

        return pageQuestions.FirstOrDefault();
    }
}
