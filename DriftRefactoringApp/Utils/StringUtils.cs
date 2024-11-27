using System;
using System.Linq;
using System.Web.Configuration;

namespace DriftRefactoringApp.Utils
{
    public class StringUtils
    {
        private readonly Random random;

        public StringUtils()
        {
            this.random = new Random();
        }

        public string ReverseString(string input)
        {
            var chars = input.ToCharArray();
            Array.Reverse(chars);
            var reversedString = new string(chars);
            return reversedString;
        }

        public string RandomString(int length)
        {
            string confString = WebConfigurationManager.AppSettings["nestedWebConfKey"];
            int depInt = new SimpleDepProj.DepClass().UsefulMethod();
            string depString = "Here is some util method goodness: "
                               + SimpleDepProj.UtilClass.UtilMethod()
                               + " and here is the number 99 from an outside dll: "
                               + new ClassLibraryOutsideSln.Class1().Always99();

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ : " + confString + " : " + depInt + depString;
            String randString = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return ConvertToLower(randString);
        }

        public string DisplayParameters(int intParam, string stringParam)
        {
            String response = "You sent {" + intParam + ", " + stringParam + "}.";
            return response;
        }

        private string ConvertToLower(string str)
        {
            return str.ToLower();
        }
    }
}