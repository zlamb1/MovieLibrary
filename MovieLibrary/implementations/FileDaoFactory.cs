using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibrary.interfaces;
using System;

namespace MovieLibrary.implementations
{
    internal class FileDaoFactory : IFactory
    {
        private readonly IServiceProvider provider;
        public FileDaoFactory(IServiceProvider _provider)
        {
            provider = _provider;
        }
        public object Create(params object[] args)
        {
            switch(args?[0])
            {
                case 0:
                    return new MovieFileDao(provider.GetService<ILogger<IFileDao>>());
                case 1:
                    return new ShowFileDao(provider.GetService<ILogger<IFileDao>>());
                case 2:
                    return new VideoFileDao(provider.GetService<ILogger<IFileDao>>());
                default:
                    return null;
            }
        }

    }
}
