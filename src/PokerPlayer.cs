using System;
using System.Linq;
using Nancy.Simple.Models;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "0.1.1";

		public static int BetRequest(JObject gameState)
		{
		    return 80;
		}

        public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

