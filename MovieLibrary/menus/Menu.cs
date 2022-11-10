using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace MovieLibrary.menus
{
    internal class Menu : IMenu
    {
        private readonly ILogger<IMenu> logger;

        private string msg = "Starting...";
        private LogLevel level = LogLevel.Information;

        protected int numberOfRestarts = 0;
        protected int allowedRestarts = 5;

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
        }

        protected void Restart(string _msg, LogLevel _level = LogLevel.Warning)
        {
            if (numberOfRestarts >= allowedRestarts)
            {
                logger.Log(LogLevel.Error, "The menu cannot restart more than " + allowedRestarts + " times!");
                return;
            }
            msg = _msg;
            level = _level;
            numberOfRestarts++;
            Start();
        }

    }
}
