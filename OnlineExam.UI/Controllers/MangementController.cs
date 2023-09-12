using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using OnlineExam.UI.Custom;
using OnlineExam.UI.Helper;
using OnlineExam.UI.Models;
using System.Reflection;

namespace OnlineExam.UI.Controllers;

[AdminAuth]
public class MangementController : Controller
{
    private readonly ILogger<MangementController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MangementController(ILogger<MangementController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult CreateExam()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateExam(ExamViewModel model)
    {
        if (ModelState.IsValid)
        {
            Examination exam = new Examination
            {
                ExamTitle = model.ExamTitle,
                ExamDescription = model.ExamDescription,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                QuestionCount = model.QuestionCount,
                DurationInHours = model.DurationInHours
            };
            await _unitOfWork.Examinations.AddAsync(exam);
            _logger.LogInformation("Exam created Successfully.");
            ViewData["SuccessMessage"] = "Exam created successfully.";
            return RedirectToAction("AddQuestion", new { examId = exam.Id });
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AddQuestion(Guid examId)
    {
        TempData["ExamId"] = examId;
        ViewBag.QuestionTypes = new SelectList(await _unitOfWork.QuestionTypes.GetAllAsync());
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddQuestion(QuestionViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.QuestionTypes = new SelectList(await _unitOfWork.QuestionTypes.GetAllAsync());
            return View(model);
        }

        var question = new Question { QuestionText = model.QuestionText, QuestionTypeId = model.QuestionType, IsActive = true, ExamId = (Guid)TempData["ExamId"] };
        await _unitOfWork.Questions.AddAsync(question);
        _logger.LogInformation("Question created successfully.");
        ViewData["SuccessMessage"] = "Question created successfully.";
        if(question.QuestionTypeId== (int)QuestionTypeEnum.Essay || question.QuestionTypeId== (int)QuestionTypeEnum.Complete)
            return RedirectToAction("AddQuestion", new { examId = question.ExamId });
        else
            return RedirectToAction("AddChoice", new { examId = question.ExamId, questionId = question.Id });
    }

    [HttpGet]
    public async Task<IActionResult> AddChoice(Guid questionId, Guid examId)
    {
        TempData["QuestionId"] = questionId;
        ViewBag.ExamId = examId;
        return View();

    }

    [HttpPost]
    public async Task<IActionResult> AddChoice(Choice choice)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.Choices.AddAsync(choice);
            var questionChoice = new QuestionChoices { ChoiceId = choice.Id, QuestionId = (Guid)TempData["QuestionId"] };
            await _unitOfWork.QuestionChoices.AddAsync( questionChoice);
            _logger.LogInformation("Choice created successfully.");
            ViewData["SuccessMessage"] = "Choice created successfully.";

            return RedirectToAction("AddChoice", new { questionId = questionChoice.QuestionId, examId = ViewBag.ExamId });
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> BackToQuestions(Guid examId)
    {
        ViewBag.ExamId = examId;
        return RedirectToAction("AddQuestion", new { examId = (Guid)TempData["ExamId"] });
    }

}
