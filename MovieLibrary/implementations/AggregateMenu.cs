using MovieLibrary.interfaces;
using System;

namespace MovieLibrary.implementations
{
    internal class AggregateMenu : IMenu
    {
        private IFactory factory;
        public AggregateMenu(IFactory _factory)
        {
            factory = _factory;
        }

        public void Start()
        {
            var choiceMenu = (IMenu)factory.Create(0);
            choiceMenu?.Start();
            var nextMenu = (IMenu)factory.Create(((ChoiceMenu)choiceMenu).Choice);
            nextMenu?.Start();
        }
    }
}
