using Nancy.Simple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple.CombinationFinder
{
	class PairFinder
	{
		const int max = 14;
		double result = 0;

		public double GetPairPower(Card[] cards) 
		{
			var gruopCount = 0;
			var distCards = cards.GroupBy(c => c.RankValue.RankId).Where(g => g.Count() > 1);
			foreach(var group in distCards)
			{
				gruopCount++;

				if (group.Key > 7)
				{
					var newResult = gruopCount * 0.1 * group.Key;
					if (newResult > result)
					{
						result = newResult;
					}
				}
			}
			return result;
		}
	}
}
