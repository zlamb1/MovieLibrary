using MovieLibrary.interfaces;
using MovieLibrary.menus;
using System;

namespace MovieLibrary.implementations
{
    internal class ChoiceMenu : IMenu
    {
        public int Choice { get; set; }
        public ChoiceMenu()
        {
            Choice = 1; 
        }
        public void Start()
        {
            Console.WriteLine("<---Media Displayer--->");
            Console.WriteLine("1. Movies");
            Console.WriteLine("2. Shows");
            Console.WriteLine("3. Videos");
            Choice = InputUtility.GetInt32WithPrompt(
                prompt: "Which media do you want to display?", 
                expected: new int[] { 1, 2, 3 });
        }
    }
}
