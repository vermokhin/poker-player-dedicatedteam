using System;
using System.Linq;
using Nancy.Simple.Models;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "0.1.2";

		public static int BetRequest(JObject gameState)
		{
		    var state = gameState.ToObject<GameState>();
            var strategy = new SimpleStrategy();
		    return strategy.CalculateBet(state);
		}

        public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

