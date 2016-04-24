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
				//get cards combination
				var hand = cards.Select(c => c.rank).ToString().ToLower();
				if(cards.Count() < 3)
				{
					if(state.current_buy_in > player.bet)
					{
						return state.current_buy_in - player.bet;
					}
					return 0;
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

				if(cards.Count() == 5)
				{
					//use rank api
				}
				
			}
			return state.minimum_raise;
		}

		private enum HandsType
		{
			Pair = 0,
			SameSuit = 1,
		}
	}
}
