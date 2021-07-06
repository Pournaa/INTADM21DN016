using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlineexam.Models;

namespace onlineexam.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Index2()
        {
            var _ctx = new OESEntities1();
            ViewBag.Tests = _ctx.Tests.Where(x => x.IsActive == true).Select(x => new { x.Id, x.Name }).ToList();

            SessionModel _model = null;
            if (Session["SessionModel"] == null)
                _model = new SessionModel();
            else
                _model = (SessionModel)Session["SessionModel"];
            return View(_model);
        }

        public ActionResult Instruction(SessionModel model)
        {
            if (model != null)
            {
                var _ctx = new OESEntities1();
                var test = _ctx.Tests.Where(x => x.IsActive == true && x.Id == model.TestId).FirstOrDefault();
                if (test != null)
                {
                    ViewBag.TestName = test.Name;
                    ViewBag.TestDescription = test.Description;
                    ViewBag.QuestionCount = test.TestXQuestions.Count;
                    ViewBag.TestDuration = test.DurationInMinute;
                }
            }
            return View(model);
        }
        public ActionResult Register(SessionModel model)
        {
            if (model != null)
                Session["SessionModel"] = model;
            if (model == null || string.IsNullOrEmpty(model.UserName) || model.TestId < 1)
            {
                TempData["message"] = "Invalid Registration details.Please try again";
                return RedirectToAction("Index");
            }
            var _ctx = new OESEntities1();
            Student _user = _ctx.Students.Where(x => x.UserName.Equals(model.UserName, StringComparison.InvariantCultureIgnoreCase)
              && ((string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(x.Email)) || (x.Email == model.Email))
             && ((string.IsNullOrEmpty(model.Mobile_no) && string.IsNullOrEmpty(x.Mobile_no)) || (x.Mobile_no == model.Mobile_no))).FirstOrDefault();
            if (_user == null)
            {
                _user = new Student()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    //Mobile_no = model.Mobile_no,
                    //EntryDate = DateTime.UtcNow,
                    //AccessLevel = 100
                };
                _ctx.Students.Add(_user);
                _ctx.SaveChanges();
            }
            Registration registration = _ctx.Registrations.Where(x => x.StudentId == _user.Id
            && x.TestId == model.TestId
            && x.TokenExpireTime > DateTime.UtcNow).FirstOrDefault();
            if (registration != null)
            {
                this.Session["TOKEN"] = registration.Token;
                this.Session["TOKENEXPIRE"] = registration.TokenExpireTime;
            }
            else
            {
                Test test = _ctx.Tests.Where(x => x.IsActive && x.Id == model.TestId).FirstOrDefault();
                if (test != null)
                {
                    Registration newRegistration = new Registration()
                    {
                        RegistrationDate = DateTime.UtcNow,
                        TestId = model.TestId,
                        Token = Guid.NewGuid(),
                        TokenExpireTime = DateTime.UtcNow.AddMinutes(test.DurationInMinute)
                    };
                    _user.Registrations.Add(newRegistration);
                    _ctx.Registrations.Add(newRegistration);
                    _ctx.SaveChanges();
                    this.Session["TOKEN"] = newRegistration.Token;
                    this.Session["TOKENEXPIRE"] = newRegistration.TokenExpireTime;
                }
            }
            return RedirectToAction("EvalPage", new { @token = Session["TOKEN"] });
        }
        public ActionResult EvalPage(Guid token, int? qno)
        {
            if (token == null)
            {
                TempData["message"] = "You have an invalid token. Please re-register and try again";
                return RedirectToAction("Index");
            }
            var _ctx = new OESEntities1();
            var registration = _ctx.Registrations.Where(x => x.Token.Equals(token)).FirstOrDefault();
            if (registration == null)
            {
                TempData["message"] = "This token is invalid";
                return RedirectToAction("Index");
            }
            if (registration.TokenExpireTime < DateTime.UtcNow)
            {
                TempData["message"] = "The exam duration has expired at " + registration.TokenExpireTime.ToString();
                return RedirectToAction("Index");
            }
            if (qno.GetValueOrDefault() < 1)
                qno = 1;
            var testQuestionId = _ctx.TestXQuestions
                 .Where(x => x.TestId == registration.TestId && x.QuestionNumber == qno)
                 .Select(x => x.Id).FirstOrDefault();
            if (testQuestionId > 0)
            {
                var _model = _ctx.TestXQuestions.Where(x => x.Id == testQuestionId).Select(x => new QuestionModel()
                {
                    QuestionType = x.Question.QuestionType,
                    QuestionNumber = (int)x.QuestionNumber,
                    Question = x.Question.Question1,
                    Point = (int)x.Question.Points,
                    TestId = (int)x.TestId,
                    TestName = x.Test.Name,
                    Options = x.Question.Choices.Where(y => y.IsActive == true).Select(y => new QXOModel()
                    {
                        ChoiceId = y.Id,
                        Label = y.Label,
                    }).ToList()
                }).FirstOrDefault();
                var savedAnswers = _ctx.TestXPapers.Where(x => x.TestXQuestionId == testQuestionId && x.RegistrationId == registration.Id && x.Choice.IsActive == true)
                    .Select(x => new { x.ChoiceId, x.Answer }).ToList();

                foreach (var savedAnswer in savedAnswers)
                {
                    _model.Options.Where(x => x.ChoiceId == savedAnswer.ChoiceId).FirstOrDefault().Answer = savedAnswer.Answer;
                }
                _model.TotalQuestionInSet = _ctx.TestXQuestions.Where(x => x.Question.IsActive == true && x.TestId == registration.TestId).Count();
                return View(_model);
            }
            else
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult PostAnswer(AnswerModel choices)
        {
            var _ctx = new OESEntities1();
            var registration = _ctx.Registrations.Where(x => x.Token.Equals(choices.Token)).FirstOrDefault();
            if (registration == null)
            {
                TempData["message"] = "This token is invalid";
                return RedirectToAction("Index");
            }
            if (registration.TokenExpireTime < DateTime.UtcNow)
            {
                TempData["message"] = "The exam duration has expired at " + registration.TokenExpireTime.ToString();
                return RedirectToAction("Index");
            }
            var testQuestionInfo = _ctx.TestXQuestions.Where(x => x.TestId == registration.TestId
            && x.QuestionNumber == choices.QuestionId)
                .Select(x => new
                {
                    TQId = x.Id,
                    QT = x.Question.QuestionType,
                    QID = x.Id,
                    POINT = (decimal)x.Question.Points
                }).FirstOrDefault();
            if (testQuestionInfo != null)
            {
                if (choices.UserChoices.Count > 1)
                {
                    var allPointValueOfChoices =
                        (
                        from a in _ctx.Choices.Where(x => x.IsActive)
                        join b in choices.UserSelectedId on a.Id equals b
                        select new { a.Id, Points = (decimal)a.Points }).AsEnumerable()
                        .Select(x => new TestXPaper()
                        {
                            RegistrationId = registration.Id,
                            TestXQuestionId = testQuestionInfo.QID,
                            ChoiceId = x.Id,
                            Answer = "CHECKED",
                            MarkScored = Math.Floor(testQuestionInfo.POINT / 100.00M) * x.Points
                        }
                        ).ToList();
                    _ctx.TestXPapers.AddRange(allPointValueOfChoices);
                }
                else
                {
                    _ctx.TestXPapers.Add(new TestXPaper()
                    {
                        RegistrationId = registration.Id,
                        TestXQuestionId = testQuestionInfo.QID,
                        ChoiceId = choices.UserChoices.FirstOrDefault().ChoiceId,
                        MarkScored = 1,
                        Answer = choices.Answer
                    });
                }
                _ctx.SaveChanges();
            }
            var nextQuestionNumber = 1;
            if (choices.Direction.Equals("forward", StringComparison.CurrentCultureIgnoreCase))
            {
                nextQuestionNumber = (int)_ctx.TestXQuestions.Where(x => x.TestId == choices.TestId
                && x.QuestionNumber > choices.QuestionId)
                    .OrderBy(x => x.QuestionNumber).Take(1).Select(x => x.QuestionNumber).FirstOrDefault();
            }
            else
            {
                nextQuestionNumber = (int)_ctx.TestXQuestions.Where(x => x.TestId == choices.TestId
                && x.QuestionNumber > choices.QuestionId)
                    .OrderByDescending(x => x.QuestionNumber).Take(1).Select(x => x.QuestionNumber).FirstOrDefault();
            }
            //if (nextQuestionNumber < 1)
            //    nextQuestionNumber = 1;
            //return RedirectToAction("EvalPage", new
            //{
            //    @token = Session["TOKEN"],
            //    @qno = nextQuestionNumber
            //});
            if (nextQuestionNumber <= 1)
                nextQuestionNumber = 1;

            return RedirectToAction("EvalPage", new { @token = Session["TOKEN"], @qno = nextQuestionNumber });


        }

    }
}