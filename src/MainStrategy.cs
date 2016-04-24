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
		    var currentBet = state.current_buy_in;

			if (cards != null)
			{
                var sortedCards = GetSortedCard(cards);
                var rankArray = GetRankArray(sortedCards);
                var suiteArray = GetSiuteArray(sortedCards);
                var suiteCount = GetSiuteCount(sortedCards);

                if (cards.Count() == 5)
				{
					GetBetFor5Cards(player, suiteCount, rankArray, suiteArray);
					//use rank api
				}
				
				//get cards combination
				if (cards.Count() < 4)
				{
				    return GetCallBet(player.stack, currentBet, player.bet);
				}

			    if (IsQuad(rankArray) != "")
			    {
			        return player.stack;
			    }
			    else
			    {
                    return GetCallBet(player.stack, currentBet, player.bet);
                }
            }
            return state.minimum_raise;
		}

		private enum HandsType
		{
			Pair = 0,
			SameSuit = 1,
		}

		public Card[] GetSortedCard(IEnumerable<Card> cards)
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

		public Dictionary<string, int> GetSiuteCount(Card[] cards)
		{
			var result = new Dictionary<string, int>();
			foreach(var card in cards)
			{
				if (result.ContainsKey(card.suit))
				{
					var value = 0;
					result.TryGetValue(card.suit, out value);
					result[card.suit] = value++;
				}
				else
				{
					result.Add(card.suit, 1);
				}
			}

			return result;
		}

		public int GetBetFor5Cards(Player player, Dictionary<string, int> SuiteCardsCount, string[] RankArray, string[] SuiteArray)
		{
            if (IsRoyalFlash(RankArray, SuiteCardsCount) == 12)
            {
                return player.stack;
            }
		    if (IsQuad(RankArray) != "")
		    {
		        return player.stack;
		    }
		    if (IsTwoGroups(SuiteCardsCount))
		    {
		        return player.stack;
		    }
			if(RankArray.Distinct().Count() == 3)
			{
				return player.stack;
			}
			return 200;
		}

	    string IsOnePair(IEnumerable<string> ranks)
	    {
	        var pairs =
	            from rank1 in ranks
	            from rank2 in ranks
	            where rank1 == rank2
	            select rank1;
	        return pairs.First();
	    }

	    int IsRoyalFlash(string[] rankArray, Dictionary<string, int> suiteCardsCount)
	    {
	        if (suiteCardsCount.Count == 1
                && rankArray.Contains("A")
                && rankArray.Contains("K")
                && rankArray.Contains("Q")
                && rankArray.Contains("J")
                && rankArray.Contains("10"))
	        {
	            return 12;
	        }
	        return 0;
	    }

	    string IsQuad(IEnumerable<string> ranks)
	    {
	        var quad = ranks.GroupBy(r => r).FirstOrDefault(g => g.Count() >= 4);
	        if (quad != null)
	        {
	            return quad.First();
	        }
	        return "";
	    }

	    bool IsTwoGroups(Dictionary<string, int> suiteCardsCount)
	    {
	        return suiteCardsCount.Count == 2;
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
