using Nancy.Simple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple
{
	public class BetSelector
	{
		private Player _player;

		private GameState _state;

		public int SelectBet(GameState state)
		{
			var player = state.players.FirstOrDefault(p => p.hole_cards.Length > 0);
			var cards = state.community_cards.Concat(player.hole_cards).ToArray();

			switch (cards.Count())
			{
				case 0:
				case 1:
				case 2:
					return state.minimum_raise;
				case 3:
					return SelectBetFor3Cards(cards);
				case 4:
					return SelectBetFor4Cards(cards);
				case 5:
					return SelectBetFor5Cards(cards);
			}

			return 0;
		}

		public int SelectBetFor3Cards(Card[] cards)
		{
			// 1 pair
			var distCards = cards.Select(c => c.RankValue).Distinct();
			var groups = distCards.GroupBy(d => d.RankId);
			if (groups.Count() > 1)
			{
				foreach(var group in groups)
				{
					if (group.Count() > 1)
					{
						return _state.current_buy_in * 2;
					}
				}
			}
			return 0;
		}

		public int SelectBetFor4Cards(Card[] cards)
		{
			// 2 pair
			var distCards = cards.Select(c => c.RankValue).Distinct();
			var groups = distCards.GroupBy(d => d.RankId);
			if (groups.Count() < 3)
			{
				foreach (var group in groups)
				{
					if (group.Count() > 1 && group.Count() < 3)
					{
						return _state.current_buy_in * 2;
					}
				}
			}
			return 0;
		}

		public int SelectBetFor5Cards(Card[] cards)
		{
			return 0;
		}

	}
}
