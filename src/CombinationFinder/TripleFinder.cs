using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple.CombinationFinder
{
	class TripleFinder
	{
	    double GetTriple(IEnumerable<int> ranks)
	    {
	        var triple = ranks.GroupBy(r => r).FirstOrDefault(g => g.Count() == 3);
	        if (triple != null)
	        {
	            return 0.1 * triple.First();
	        }
	        return 0;
	    }
	}
}
