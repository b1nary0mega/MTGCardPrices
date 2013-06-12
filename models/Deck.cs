using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTGCardPrices.Models
{
    public class Deck
    {
        public string Name { get; set; }
        public string Format { get; set; }
        public List<Card> MainDeck { get; set; }
        public List<Card> Sideboard { get; set; }
    }
}
