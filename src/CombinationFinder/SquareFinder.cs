using Nancy.Simple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple.CombinationFinder
{
	class SquareFinder
	{
		public double FindSquare(Card[] cards)
		{
			var distCards = cards.GroupBy(c => c.RankValue.RankId).Where(g => g.Count() > 3);
			if(distCards.Count() != 0)
			{
				return 0.1 * distCards.First().Key;
			}
			return 0;
		}
	}
}
