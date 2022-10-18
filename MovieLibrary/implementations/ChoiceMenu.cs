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
            Console.WriteLine("4. Search by Title");
            Choice = InputUtility.GetInt32WithPrompt(
                prompt: "Which choice do you want?", 
                expected: new int[] { 1, 2, 3, 4 });
        }
    }
}
