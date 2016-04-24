using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Simple.Models
{
	public class Rank: IComparable<Rank>
	{
		public int RankId { get; set; }

		public Rank(string rank)
		{
			var result = 0;
			if (int.TryParse(rank, out result))
			{
				RankId = result;
			}
			else
			{
				RankId = GenerateRank(rank);
			}
		}

		public int GenerateRank(string rank)
		{
			switch (rank)
			{
				case "J":
					return 11;
				case "Q":
					return 12;
				case "K":
					return 13;
				case "A":
					return 14;
			}
			return 0;
		}

		public int CompareTo(Rank other)
		{
			return RankId.CompareTo(other.RankId);
		}
	}
}
