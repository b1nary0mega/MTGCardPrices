using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTGCardPrices.Models
{
    public class Card
    {
        public string Name { get; set; }
        public string NamePlain { get; set; }
        public string Rarity { get; set; }
        public string Set { get; set; }
        public string PriceAction { get; set; }
        public decimal BuyPriceRegular { get; set; }
        public decimal SellPriceRegular { get; set; }
        public decimal BuyPriceFoil { get; set; }
        public decimal SellPriceFoil { get; set; }
        public int BuyQtyRegular { get; set; }
        public int BuyQtyFoil { get; set; }
        public decimal PaperHIGH { get; set; }
        public decimal PaperMID { get; set; }
        public decimal PaperLOW { get; set; }
    }
}
