using MovieLibrary.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.menus
{
    internal class AggregateMenu : IMenu
    {
        private IFactory<IMenu> factory;
        public AggregateMenu(IFactory<IMenu> _factory)
        {
            factory = _factory;
        }
        public int GetResults() { return 0; }
        public void Start()
        {
            var main = factory.Create(0);
            main.Start();
            switch(main.GetResults())
            {
                case 1:
                    factory.Create(1).Start();
                    break;
                case 2: 
                    factory.Create(2).Start();
                    break;
                default:
                    Console.WriteLine("Exiting...");
                    return;
            }
        }
    }
}
