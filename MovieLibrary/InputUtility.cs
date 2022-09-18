using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.menus
{
    public static class InputUtility
    {
        /*
         *  If a function caller wants retries for their prompts, there needs to be a recursion
         *  limit to avoid possible StackOverflows.
         */
        public static readonly int maxRecursionDepth = 3;
        public static string GetStringWithPrompt(string prompt = "", bool addColon = true)
        {
            Console.Write(prompt + (addColon ? ": " : ""));
            return Console.ReadLine();
        }
        public static int GetInt32WithPrompt(string prompt = "", bool addColon = true, 
            bool retry = true, int recursion = 1, int[] expected = null)
        {
            Console.Write(prompt + (addColon ? ": " : ""));
            int output = 0;
            try
            {
                output = int.Parse(Console.ReadLine());
                if (!expected.Contains(output) && retry && recursion < maxRecursionDepth)
                    output = GetInt32WithPrompt(prompt, addColon, 
                        retry, recursion, expected);
            }
            catch (Exception)
            {
                if (retry && recursion < maxRecursionDepth)
                    output = GetInt32WithPrompt(prompt, addColon, 
                        retry, recursion + 1, expected);
                else return 0;
            }
            return output;
        }
        public static bool GetBoolWithPrompt(string prompt = "", bool ignoreCase = true, 
            bool addColon = true, bool retry = true, int recursion = 1)
        {
            Console.Write(prompt + " (Y/N)" + (addColon ? ": " : ""));
            try
            {
                string resp = Console.ReadLine();
                if (ignoreCase) resp = resp.ToUpper();
                return bool.Parse(resp.Replace("Y", "true").Replace("N", "false"));
            }
            catch (Exception)
            {
                if (retry && recursion < maxRecursionDepth)
                    return GetBoolWithPrompt(prompt, ignoreCase, 
                        addColon, retry, recursion + 1);
                return false;
            }
        }

    }
}
