using System;
using System.Linq;
using Nancy.Simple.Models;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "0.3.1";

		public static int BetRequest(JObject gameState)
		{
			var state = gameState.ToObject<GameState>();
            var player = state.players.FirstOrDefault(p => p.hole_cards.Length > 0);
            return new Random().Next(0, player.stack);
			return GetStrategy().CalculateBet(state);
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}

		public static IStrategy GetStrategy()
		{
			return new MainStrategy();
		}
	}
}

