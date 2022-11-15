using Microsoft.Extensions.Logging;
using MovieLibrary.Interfaces;
using System;
using System.Threading;

namespace MovieLibrary.Menus
{
    internal class Menu : IMenu
    {
        public int Result { get; set; }
        
        protected int numberOfRestarts = 0;
        protected int allowedRestarts = 5;

        private readonly ILogger<IMenu> logger;
        private string msg = "Starting...";
        private LogLevel level = LogLevel.Information;

        public Menu(ILogger<IMenu> _logger)
        {
            logger = _logger;
        }

        public virtual void Start()
        {
            Console.Clear();

            logger.Log(level, msg);

            // sleep to log before anything else
            Thread.Sleep(3);

            Console.WriteLine(DateTime.Now + " | Menu | " + level + " | " + msg);
            Console.WriteLine();
        }

        protected void Restart(string _msg, LogLevel _level = LogLevel.Warning)
        {
            if (numberOfRestarts >= allowedRestarts)
            {
                logger.Log(LogLevel.Error, "The menu cannot restart more than " + allowedRestarts + " times!");
                Thread.Sleep(3);
                Console.WriteLine();
                WaitForInput();
                return;
            }

            msg = _msg;
            level = _level;
            numberOfRestarts++;

            Start();
        }

        protected void WaitForInput(string _msg="Press enter to return. ")
        {
            Console.Write(_msg);
            Console.ReadLine();
        }

    }
}
