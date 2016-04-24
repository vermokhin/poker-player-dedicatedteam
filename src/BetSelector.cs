using Nancy.Simple.CombinationFinder;
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

	    public BetSelector(Player player, GameState state)
	    {
	        _player = player;
	        _state = state;
	    }

	    public int SelectBet()
		{
			var cards = _state.community_cards.Concat(_player.hole_cards).ToArray();

			switch (cards.Count())
			{
				case 0:
				case 1:
				case 2:
					return GetCallBet(_player.stack, _state.current_buy_in, _player.bet);
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
			var triple = new TripleFinder().GetTriple(cards);
			if (triple > 0)
			{
				return (int)(2 * triple * _state.current_buy_in);
			}

			var pairFinder = new PairFinder();
			//update to  call for < 0.5
			return pairFinder.GetPairPower(cards) > 0.5 ? (int)(pairFinder.GetPairPower(cards) * 2 * _state.current_buy_in) : GetCallBet(_player.stack, _state.current_buy_in, _player.bet);;
		}

		public int SelectBetFor4Cards(Card[] cards)
		{
			var square = new SquareFinder().FindSquare(cards);
			if(square > 0)
			{
				return (int)(2 * square * _state.current_buy_in);
			}

			return 0;
		}

		public int SelectBetFor5Cards(Card[] cards)
		{
			var square = new SquareFinder().FindSquare(cards);
			if (square > 0)
			{
				return (int)(2 * square * _state.current_buy_in);
			}

			return 0;
		}

        int GetCallBet(int stack, int currentBet, int ourBet)
        {
            var needToAdd = currentBet > ourBet ? currentBet - ourBet : 0;
            if (stack - needToAdd < 300)
            {
                return 0;
            }
            return needToAdd;
        }
    }
}
