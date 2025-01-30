using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public class Game
    {
        private List<Card> cards;
        private List<Card> matchedCards;

        private Random random;

        public Game()
        {
            cards = new List<Card>(); 
            random = new Random();
            StartCards();
        }

        public void StartCards()
        {
            for (int index = 1; index <= 7; index++)
            {
                cards.Add(new Card(index)); 
                cards.Add(new Card(index));
            }
            ShuffleCards();
        }


        public void ShuffleCards()
        {
            cards = cards.OrderBy(card => random.Next()).ToList(); 
        }

        public List<Card> GetCards()
        {
            return cards;
        }

        public bool CheckMatch(Card firstCard, Card secondCard)
        {
            return firstCard.Value == secondCard.Value; 
        }

        public bool CheckFlipped()
        {
            foreach (Card card in cards)
            {
                if (!card.Flipped)
                {
                    return false;
                }
            }
            return true;
        }
        
        public void Reset()
        {
            foreach(Card card in cards)
            {
                card.Flipped = false;
            }
            ShuffleCards();
        }
    }
}