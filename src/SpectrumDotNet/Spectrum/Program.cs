using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table.Queryable;
using System.Security.Claims;

namespace Spectrum
{
    public static class Spectrum
    {
        public static string connectionString = Environment.GetEnvironmentVariable("connectionString");
        
        [FunctionName("NewGame")]
        public static async Task<IActionResult> NewGame(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("games");
            
            var category = GetRandomCategory();

            GameEntity game = new GameEntity(RandomString(5), name)
            {
                CategoryLeft = category.CategoryLeft,
                CategoryRight = category.CategoryRight,
                Location = Math.Round(new Random().NextDouble() * 100, 3)
            };

            TableOperation insertOperation = TableOperation.Insert(game);
            table.Execute(insertOperation);

            return new OkObjectResult(game);
        }

        [FunctionName("GetRandomCategory")]
        public static IActionResult HttpGetRandomCategory(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
        {
            var res = GetRandomCategory();
            return new OkObjectResult(res);
        }

        [FunctionName("AuthTest")]
        public static IActionResult AuthTest(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ClaimsPrincipal principal,
        ILogger log)
        {
            var name = "TEST: ";
            try
            {
                var user = req.HttpContext.User.Identity.Name;
                name = $"{name} | SUCCESS | {principal.Identity.IsAuthenticated} | {user}";
            }
            catch(Exception ex)
            {
                name = $"{name} | Exception | {ex.Message}";
            }
            return new OkObjectResult(name);
        }

        public static ClueEntity GetRandomCategory()
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("clues");
            
            TableQuery<ClueEntity> clueQuery = table.CreateQuery<ClueEntity>();
            var query = (from clue in clueQuery
                         select clue).AsTableQuery();

            var clueList = query.Execute().ToList();
            var ix = new Random().Next(0, clueList.Count);
            var row = clueList[ix];
            return row;
        }

        [FunctionName("AddCategory")]
        public static async Task<IActionResult> AddCategory(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
    ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var left = data?.left;
            var right = data?.right;
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("clues");

            ClueEntity clue = new ClueEntity(RandomString(10))
            {
                CategoryLeft = left,
                CategoryRight = right
            };

            TableOperation insertOperation = TableOperation.Insert(clue);
            table.Execute(insertOperation);

            return new OkResult();
        }

        [FunctionName("SetGuess")]
        public static async Task<IActionResult> SetGuess(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            double guess = data?.guess;
            string gameId = data?.gameId;
            string guesserName = data?.guesserName;

            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("guesses");

            GuessEntity guessRow = new GuessEntity(gameId, guesserName)
            {
                Guess = guess
            };

            TableOperation insertOperation = TableOperation.Insert(guessRow);
            table.Execute(insertOperation);

            return new OkResult();
        }

        [FunctionName("GetGuesses")]
        public static async Task<IActionResult> GetGuesses(
                [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
                ILogger log)
        {
            string gameId = req.Query["gameId"];

            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("guesses");
            

            TableQuery<GuessEntity> guessQuery = table.CreateQuery<GuessEntity>();
            var query = (from guess in guessQuery
                         where guess.PartitionKey == gameId
                         select guess).AsTableQuery();

            var guessList = query.Execute().ToList();

            return new OkObjectResult(guessList);
        }

        public static string RandomString(int length)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
