using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using OnlineExam.Infrastructure.Repositories;
using OnlineExam.UI.Models;
using System.Drawing;
using System.Reflection;

namespace OnlineExam.UI.Controllers;

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
                DurationInHours = model.DurationInHours,
                IsActive = true
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
        if (question.QuestionTypeId != 2 && question.QuestionTypeId != 3)
            return RedirectToAction("AddChoice", new { examId = question.ExamId, questionId = question.Id });
        else
            return RedirectToAction("AddQuestion", new { examId = question.ExamId });
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
            choice.QuestionId = (Guid)TempData["QuestionId"];
            await _unitOfWork.Choices.AddAsync(choice);
            _logger.LogInformation("Choice created successfully.");
            ViewData["SuccessMessage"] = "Choice created successfully.";

            return RedirectToAction("AddChoice", new { questionId = choice.QuestionId, examId = ViewBag.ExamId });
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
