using System;

namespace Nancy.Simple.Models
{
    public class Card: IComparable<Card>
	{
        public string rank;

        public string suit;

		public Rank RankValue;

		public Card(string _rank, string _suit)
		{
			rank = _rank;
			suit = _suit;
			RankValue = new Rank(rank);
		}

		public int CompareTo(Card other)
		{
			return RankValue.CompareTo(other.RankValue);
		}
	}
}
