using System;
using System.Text.RegularExpressions;

namespace CaloriesTracker.Domain.Utils
{
    public class RegexCompiler
    {
        private static readonly Lazy<RegexCompiler> _lazy = new Lazy<RegexCompiler>(() => new RegexCompiler());
        public static RegexCompiler Current => _lazy.Value;

        private RegexCompiler()
        {

        }

        public Regex Email = new Regex(@"^([\w\.\-\+]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.Compiled);
    }
}
