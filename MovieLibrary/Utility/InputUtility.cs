using System;

namespace MovieLibrary.utility
{
    public static class InputUtility
    {
        public static string GetStringWithPrompt(string prompt = "", bool addColon = false)
        {
            Console.Write(prompt + (addColon ? ": " : ""));
            return Console.ReadLine();
        }
        public static Tuple<bool, int> GetInt32WithPrompt(string prompt="", bool addColon=false)
        {
            Console.Write(prompt + (addColon is false ? "" : ":"));
            string input = Console.ReadLine();
            try
            {
                int i = int.Parse(input);
                return Tuple.Create(true, i);
            } catch (Exception)
            {
                return Tuple.Create(false, 0);
            }
        }
        public static bool GetBoolWithPrompt(string prompt = "")
        {
            Console.Write(prompt + " ");
            string resp = Console.ReadLine();

            switch (resp.ToLower())
            {
                case "0":
                case "n":
                case "no":
                case "false":
                    return false;
                case "1":
                case "y":
                case "yes":
                case "true":
                    return true;
                default:
                    return false;
            }
        }
    }
}
