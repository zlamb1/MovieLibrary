using MovieLibrary.interfaces;
using FS.FilterExpressionCreator.Enums;
using FS.FilterExpressionCreator.Extensions;
using FS.FilterExpressionCreator.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieLibrary.movieutility
{
    internal class MovieFilter : IFilter
    {
        private readonly ILogger<IFilter> logger;
        public bool ExceptionOccured { get; set; }
        public List<object> Input { get; set; }
        public List<object> Output { get; private set; }
        public MovieFilter(ILogger<IFilter> _logger)
        {
            logger = _logger;
        }
        public void Filter(params string[] args)
        {
            if (Input != null)
            {
                logger.Log(LogLevel.Information, "Filtering movies!");
                string idFilter = args.Length > 0 ? args[0] : null;
                string titleFilter = args.Length > 1 ? args[1] : null;
                string genreFilter = args.Length > 2 ? args[2] : null;
                List<Movie> _Input = Input.Cast<Movie>().ToList();
                var filter = new EntityFilter<Movie>();
                if (idFilter.Length > 0)
                {
                    FilterOperator oper = GetFilterOperator(idFilter);
                    string StringFilter = idFilter.Length > 1 ?
                        idFilter.Remove(0, 1) : idFilter;
                    int comp;
                    try
                    {
                        comp = int.Parse(StringFilter);
                    }
                    catch (Exception)
                    {
                        logger.Log(LogLevel.Error, "MovieID filter was not an integer.");
                        // If we encounter an exception we want to make sure no movies get filtered by this field.
                        comp = 0;
                        oper = FilterOperator.GreaterThan;
                    }
                    if (oper == FilterOperator.Contains)
                        filter.Add(x => x.IdString, oper, StringFilter);
                    else filter.Add(x => x.Id, oper, comp);
                }
                if (titleFilter.Length > 0)
                {
                    FilterOperator oper = GetFilterOperator(titleFilter);
                    string StringFilter = titleFilter.Length > 1 ?
                        titleFilter.Remove(0, 1) : titleFilter;
                    filter.Add(x => x.Title, oper, StringFilter);
                }
                if (genreFilter.Length > 0)
                {
                    string[] _Genres = genreFilter.Split('|');
                    _Input = _Input.Where(x => 
                        x.Genres.Any(Genre => _Genres.Contains(Genre))).ToList();
                }
                Output = _Input.Where(filter).Cast<object>().ToList();
            }
            else logger.Log(LogLevel.Error, "Movie filter has no input!");
        }
        private FilterOperator GetFilterOperator(string filterString)
        {
            FilterOperator filterOperator;
            var filterChar = filterString.Substring(0, 1);
            switch (filterChar)
            {
                case "=":
                    filterOperator = FilterOperator.EqualCaseSensitive;
                    break;
                case ">":
                    filterOperator = FilterOperator.GreaterThan;
                    break;
                case "<":
                    filterOperator = FilterOperator.LessThan;
                    break;
                default:
                    filterOperator = FilterOperator.Contains;
                    break;
            }
            return filterOperator;
        }
    }
}
