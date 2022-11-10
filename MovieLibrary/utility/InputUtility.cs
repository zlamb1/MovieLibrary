using System;

namespace MovieLibrary.utility
{
    public static class InputUtility
    {
        /*
         *  If a function caller wants retries for their prompts, there needs to be a recursion
         *  limit to avoid possible StackOverflows.
         */
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
            return false;
        }

    }
}
