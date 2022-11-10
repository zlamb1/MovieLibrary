using Microsoft.Extensions.Logging;
using MovieLibrary.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.menus
{
    internal class Menu : IMenu
    {
        private readonly ILogger<IMenu> logger;

        private string msg;
        private LogLevel level;

        protected int numberOfRestarts = 0;
        protected int allowedRestarts = 5;
        protected string statusMsg;

        public Menu(ILogger<IMenu> _logger)
        {
            logger = _logger;
        }

        public virtual void Start()
        {
            Console.Clear();
            logger.Log(level, msg);
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
            Start();
        }

    }
}
