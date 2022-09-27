using MovieLibrary.interfaces;
using System;

namespace MovieLibrary.implementations
{
    internal class AggregateMenu : IMenu
    {
        private Func<int, IFactory> factory;

        public AggregateMenu(Func<int, IFactory> _factory)
        {
            factory = _factory;
        }

        public void Start()
        {
            var choiceMenu = (IMenu)factory(0).Create(0);
            choiceMenu?.Start();
            var nextMenu = (IMenu)factory(0).Create(((ChoiceMenu)choiceMenu).Choice);
            nextMenu?.Start();
        }
    }
}
