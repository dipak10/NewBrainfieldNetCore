using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Viewmodels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class ApplyExamController : Controller
    {

        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;
        private readonly ILogger<ApplyExamController> logger;

        private decimal correctquestion = 0;
        private static int gettotalappered = 0;

        private List<Tuple<int, string>> keyValuePairs = new List<Tuple<int, string>>();

        public ApplyExamController(ApplicationContext entity, INotyfService notyf, ILogger<ApplyExamController> logger)
        {
            this.entity = entity;
            this.notyf = notyf;
            this.logger = logger;
        }

        [Route("Exam/{ExamId:int}/Order/{OrderId:int}")]
        public async Task<IActionResult> Index(int ExamId, int OrderId)
        {
            SetGlobalValues(ExamId, OrderId);

            bool isEligible = await CheckEligibilityForExam(ExamId, OrderId);

            if (isEligible)
            {
                //return RedirectToAction("DummyExamSelection");

                return RedirectToAction("StartExam");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> StartExam()
        {
            try
            {
                await AppUpdateExamAppear();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                string constring = "Data Source=DESKTOP-JGV15TO\\NSJSQLEXPRESS;Initial Catalog=Newexamportal;Integrated Security=True";
                DataSet ds = new DataSet("StartExam");

                List<StartExamViewModel> eqa = null;
                if (GlobalVariables.ExamId > 0)
                {
                    using (SqlConnection conn = new SqlConnection(constring))
                    {
                        await conn.OpenAsync();
                        SqlCommand command = new SqlCommand();
                        command.Connection = conn;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "uspGetExamQuestion";
                        command.Parameters.Add(new SqlParameter("@ExamId", GlobalVariables.ExamId));
                        SqlDataAdapter dataAdapter = new SqlDataAdapter();
                        dataAdapter.SelectCommand = command;
                        dataAdapter.Fill(ds);
                        await conn.CloseAsync();
                        await conn.DisposeAsync(); //added
                    }
                }

                eqa = AddToList(ds);
                List<Tuple<int, string>> subjects = GetSubjectWiseIndex(eqa);
                var candidatename = "Malpha";
                var examname = entity.tblExamMaster.Where(x => x.ExamID == GlobalVariables.ExamId).Select(x => x.ExamName).First();

                stopwatch.Stop();
                string time = stopwatch.ElapsedMilliseconds.ToString();

                this.logger.LogInformation($"95 Time to called data {time}");

                //Thread.Sleep(8000);
                return View(new StartExamViewModel(eqa.OrderBy(x => x.SubjectID), subjects, candidatename, examname, GlobalVariables.ExamId, GlobalVariables.UserId));
            }
            catch (Exception e)
            {
                logger.LogError($"95 error occured {e.Message} +  {e.InnerException}");
                return RedirectToAction("Index", "Home");
            }
            finally
            {
                Dispose();
            }

        }


        #region Helpers

        private List<StartExamViewModel> AddToList(DataSet set)
        {
            List<StartExamViewModel> startExamViewModels = new List<StartExamViewModel>();
            startExamViewModels = set.Tables[0].AsEnumerable().Select(
                          dataRow => new StartExamViewModel
                          {
                              ExamID = dataRow.Field<int>("ExamID"),
                              ExamQuestionID = dataRow.Field<int>("ExamQuestionID"),
                              QuestionID = dataRow.Field<int>("QuestionID"),
                              Question = dataRow.Field<string>("Question"),
                              OptionA = dataRow.Field<string>("OptionA"),
                              OptionB = dataRow.Field<string>("OptionB"),
                              OptionC = dataRow.Field<string>("OptionC"),
                              OptionD = dataRow.Field<string>("OptionD"),
                              CorrectAnswer = dataRow.Field<string>("CorrectAnswer"),
                              SubjectID = dataRow.Field<int>("SubjectID"),
                              SubjectName = dataRow.Field<string>("SubjectName")
                          }).ToList();
            return startExamViewModels;
        }


        public List<Tuple<int, string>> GetSubjectWiseIndex(List<StartExamViewModel> examQuestion_Results)
        {
            int index = 0;
            string f_subjname = string.Empty;
            string l_subjname = string.Empty;
            List<StartExamViewModel> spGetExams = examQuestion_Results;
            IEnumerable<StartExamViewModel> examsubjectwise = spGetExams.OrderBy(x => x.SubjectID);
            f_subjname = examsubjectwise.Select(x => x.SubjectName).First();
            foreach (var r in examsubjectwise)
            {
                if (index == 0)
                {
                    keyValuePairs.Add(new Tuple<int, string>(index, f_subjname));
                    l_subjname = f_subjname;
                }
                if (r.SubjectName != l_subjname)
                {

                    keyValuePairs.Add(new Tuple<int, string>(index, r.SubjectName));
                    l_subjname = r.SubjectName;
                }
                index++;
            }

            return keyValuePairs;
        }

        private void SetGlobalValues(int ExamId, int OrderId)
        {
            Helpers.GlobalVariables.ExamId = ExamId;
            Helpers.GlobalVariables.OrderId = OrderId;
            GlobalVariables.UserId = 2;
        }

        public async Task<bool> CheckEligibilityForExam(int? ExamId, int? OrderId)
        {
            int? getrepetation = await entity.tblExamMaster.Where(x => x.ExamID == ExamId).Select(x => x.Repetation).FirstAsync();

            //Optimse this
            gettotalappered = await entity.tblExamAppear.Where(x => x.ExamID == ExamId && x.UserID == GlobalVariables.UserId && x.OrderID == OrderId).Select(x => x.Appear).FirstAsync();

            if (gettotalappered > getrepetation)
            {
                return false;
            }
            return true;
        }

        private async Task AppUpdateExamAppear()
        {
            //Optimse this
            var getdata = await (entity.tblExamAppear.Where(x => x.UserID == GlobalVariables.UserId && x.ExamID == GlobalVariables.ExamId && x.OrderID == GlobalVariables.OrderId)).FirstOrDefaultAsync();
            if (getdata == null)
            {
                tblExamAppear ea = new tblExamAppear();
                ea.UserID = GlobalVariables.UserId;
                ea.ExamID = GlobalVariables.ExamId;
                ea.OrderID = GlobalVariables.OrderId;
                ea.Appear = gettotalappered++;
                ea.LastUpdated = DateTime.Now;
                await entity.tblExamAppear.AddAsync(ea);
                await entity.SaveChangesAsync();
            }
            else
            {
                getdata.Appear = gettotalappered + 1;
                await entity.SaveChangesAsync();
            }
        }

        #endregion Helpers
    }
}
