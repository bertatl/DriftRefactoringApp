using System;
using DriftRefactoringApp.Utils;
using DriftRefactoringApp.Models;
using SimpleDepProj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;


namespace DriftRefactoringApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly int charLimit = 8;
        private static readonly int seed = 100;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //private IColorUtilsEndpoint myColorUtils = ColorUtilsEndpointFactory.GetEndpointAdapter(seed, "something");
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is an exciting About message!";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Always3()
        {
            var depClass = new DepClass();
            ViewBag.Message = "here is a number from DepClass.UsefulMethod: " + depClass.UsefulMethod();
            return View();
        }

        public ActionResult PickRandomNumber()
        {
            ColorUtils myColorUtils = new ColorUtils(seed, "something");
            int randPersonId = myColorUtils.PickRandomNumberRange(1, charLimit);
            int randPersonIdOverload = randPersonId + myColorUtils.PickRandomNumberRange(1, charLimit, "something");
            ViewBag.Message = "Random Number: " + randPersonId;
            return View();
        }

        public ActionResult AddRandomPerson()
        {
            // First generate a random [charLimit]-char name
            StringUtils myStringUtils = new StringUtils(_configuration);
            String randPersonName = myStringUtils.RandomString(charLimit);
            // Next get the Last PersonId
            DBConnector myConnector = new DBConnector(_configuration);
            int lastPersonId = myConnector.GetPersonCount();
            int newPersonId = lastPersonId + 1;
            // Next reverse the name to get user id
            String randPersonHandle = myStringUtils.ReverseString(randPersonName);
            // Add this new Person to the DB
            Person randPerson = new Person();
            randPerson.Name = randPersonName;
            randPerson.PersonId = newPersonId;
            randPerson.UserHandle = randPersonHandle;
            bool addSuccessful = myConnector.AddPerson(randPerson);
            if (addSuccessful)
            {
                ViewBag.Message = "Random Person: " + randPerson.ToString() + " successfully added to the UsersDB.";
            }
            else
            {
                ViewBag.Message = "Error. Random Person: " + randPerson.ToString() + " could not be added to the UsersDB";
            }

            return View();
        }

        public ActionResult GetRandomPerson()
        {
            TempData["name"] = "Bill";
            HttpContext.Session.SetString("City", "city");
            // First get the Last PersonId
            DBConnector myConnector = new DBConnector(_configuration);
            int lastPersonId = myConnector.GetPersonCount();
            // Next generate a random Id within this limit
            ColorUtils myColorUtils = new ColorUtils(seed);
            int randPersonId = myColorUtils.PickRandomNumber(charLimit);
            // Retrieve the Person Record from DB
            Person randPerson = myConnector.GetPerson(randPersonId);
            if (randPerson != null)
            {
                ViewBag.Message = "Found Random Person: " + randPerson.ToString() + " with Id: " + randPersonId;
            }
            else
            {
                ViewBag.Message = "Error. Could not find Random Person with Id: " + randPersonId;
            }

            return View();
        }
    }
}