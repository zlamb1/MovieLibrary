using Microsoft.Extensions.Logging;
using MovieLibrary.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.menus
{
    internal class SearchMenu : Menu
    {

        public SearchMenu(ILogger<IMenu> _logger) : base(_logger)
        {

        }

        public override void Start()
        {
            base.Start();

            string title = InputUtility.GetStringWithPrompt("What is title of the movie? ");
        }
    }
}
