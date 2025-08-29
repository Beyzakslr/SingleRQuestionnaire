using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SingleRQuestionnaire.Models;
using SingleRQuestionnaire.Services;
using System.Collections.Concurrent;

namespace SingleRQuestionnaire.Controllers
{
    [Authorize]
    public class QuestionnaireController : Controller
    {

        private readonly MongoDbService _mongoDbService;
        public QuestionnaireController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public IActionResult CreateVote()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVote(string question, string options)
        {
            if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(options))
            {
                throw new Exception("Soru ve seçenekler boş olamaz.");
            }

            var optionList = options.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var newPoll = new Poll
            {
                Question = question,
                Options = new Dictionary<string, List<string>>(
                optionList.ToDictionary(o => o, o => new List<string>())
            )
            };

            await _mongoDbService.CreateAsync(newPoll);

            return RedirectToAction("Detail", new { id = newPoll.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {

            var poll = await _mongoDbService.GetAsync(id);
            if (poll == null)
            {
                throw new Exception("Anket bulunamadı.");
            }

            return View(poll);
        }
    }
}
