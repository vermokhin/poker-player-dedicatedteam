using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.Simple.Models;

namespace Nancy.Simple
{
	public class MainStrategy : IStrategy
	{
		public int CalculateBet(GameState state)
		{
			var player = state.players.FirstOrDefault(p => p.hole_cards.Length > 0);
			var cards = player.hole_cards;
			if (cards != null)
			{
				//get cards combination
				var hand = cards.Select(c => c.rank).ToString().ToLower();
				if (hand.First().Equals(hand.Last()))
				{
					var bet = state.current_buy_in * 2;
					return player.stack > bet ? bet : player.stack;
				}
			}
			return 0;
		}
	}
}
