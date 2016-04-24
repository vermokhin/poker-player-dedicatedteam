using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Simple.Models;

namespace Nancy.Simple
{
	public class MainStrategy : IStrategy
	{
		int minCardRank = 7;

		public int CalculateBet(GameState state)
		{
			var player = state.players.FirstOrDefault(p => p.hole_cards.Length > 0);

			var cards = state.community_cards.Concat(player.hole_cards);

			var doubleBet = state.current_buy_in * 2;
			var minRaiseBet = state.current_buy_in + state.minimum_raise;
			if (cards != null)
			{
				if (cards.Count() == 5)
				{
					//use rank api
				}
				
				//get cards combination
				if (cards.Count() < 3)
				{
					return minRaiseBet;
				}

				//no pairs in cards
				if((cards.Select(c=>c.rank).Distinct().Count() == cards.Count()) ||
					(cards.Select(c=>c.suit).Distinct().Count() == cards.Count()))
				{
					return doubleBet > player.stack ? player.stack : doubleBet;
				}
				else
				{
					return minRaiseBet > player.stack ? player.stack : minRaiseBet;
				}
			}
			return state.minimum_raise;
		}

		private enum HandsType
		{
			Pair = 0,
			SameSuit = 1,
		}
		public Card[] GetSortedCard(Card[] cards)
		{
			return cards.OrderBy(c => c.rank).ToArray();
		}

		public string[] GetRankArray(Card[] cards)
		{
			return cards.Select(c=>c.rank).ToArray();
		}

		public string[] GetSiuteArray(Card[] cards)
		{
			return cards.Select(c => c.suit).ToArray();
		}

	    bool IsOnePair(IEnumerable<Card> cards)
	    {
	        var distinctCards = cards
	            .GroupBy(card => card.rank)
	            .Select(group => group.First());
	        return distinctCards.Count() < cards.Count();
	    }
	}
}
