using System;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "0.0.2";

		public static int BetRequest(JObject gameState)
		{
			//TODO: Use this method to return the value You want to bet
			return new Random().Next(200, 300);
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

