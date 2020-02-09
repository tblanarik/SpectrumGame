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
using System.Collections.Generic;

namespace Spectrum
{
    public static class Spectrum
    {
        public static string connectionString = Environment.GetEnvironmentVariable("connectionString");
        
        [FunctionName("NewGame")]
        public static async Task<IActionResult> NewGame(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ClaimsPrincipal principal,
            ILogger log)
        {
            var auth = await AuthUtil.IsAuthorized(req, principal);
            if (!auth.IsAuthenticated) return new ForbidResult();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("games");
            
            var category = GetRandomCategory();

            GameEntity game = new GameEntity(RandUtil.RandomString(5), name)
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
        public static async Task<IActionResult> HttpGetRandomCategory(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ClaimsPrincipal principal,
        ILogger log)
        {
            var auth = await AuthUtil.IsAuthorized(req, principal);
            if (!auth.IsAuthenticated) return new ForbidResult();

            var res = GetRandomCategory();
            return new OkObjectResult(res);
        }

        [FunctionName("AuthTest")]
        public static async Task<IActionResult> AuthTest(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
        ClaimsPrincipal principal,
        ILogger log)
        {
            var auth = await AuthUtil.IsAuthorized(req, principal);
            if (!auth.IsAuthenticated) return new ForbidResult();

            var user = auth.Username;
            var msg = $"Authorized: {user}";
            return new OkObjectResult(new { message = msg});
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ClaimsPrincipal principal,
        ILogger log)
        {
            var auth = await AuthUtil.IsAuthorized(req, principal);
            if (!auth.IsAuthenticated) return new ForbidResult();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            var left = data?.left;
            var right = data?.right;
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("clues");

            ClueEntity clue = new ClueEntity(RandUtil.RandomString(10))
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
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ClaimsPrincipal principal,
        ILogger log)
        {
            var auth = await AuthUtil.IsAuthorized(req, principal);
            if (!auth.IsAuthenticated) return new ForbidResult();

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
                [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
                ClaimsPrincipal principal,
                ILogger log)
        {
            var auth = await AuthUtil.IsAuthorized(req, principal);
            if (!auth.IsAuthenticated) return new ForbidResult();

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
    }
}
