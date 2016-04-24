using Nancy.Simple.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple.CombinationFinder
{
	class TripleFinder
	{
	    public double GetTriple(Card[] cards)
	    {
	        var triple = cards.GroupBy(c=>c.RankValue.RankId).FirstOrDefault(g => g.Count() == 3);
	        if (triple != null)
	        {
	            return 0.1 * triple.Key;
	        }
	        return 0;
	    }
	}
}
