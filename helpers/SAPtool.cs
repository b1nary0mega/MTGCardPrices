using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using MTGCardPrices.Models;
using System.Text.RegularExpressions;

namespace MTGCardPrices.helpers
{
    class SAPtool
    {

        public List<Card> MasterOutput = new List<Card>();
        //LIST<string> of set names
        public List<string> STDsetList = new List<string> { "dgm", "gtc", "rtr", "m13", "avr", "dka", "isd" };
        public List<string> EXTsetList = new List<string> { "zen", "wwk", "roe", "m11", "som", "mbs", "nph", "m12", "isd", "dka", 
                "avr", "m13", "rtr", "gtc", "dgm"};
        public List<string> MODsetList = new List<string> { "8ed", "mrd", "dst", "5dn", "chk", "bok", "sok", "rav", "gpt", "dis",
                "tsp", "tsb", "plc", "fut", "lrw", "mor", "shm", "eve", "ala", "con", "arb", "m10", "zen", "wwk", "roe",
                "m11", "som", "mbs", "nph", "m12", "isd", "dka", "avr", "m13", "rtr", "gtc", "dgm"};

        /// <summary>
        /// Default Constructor for SAPTool, all price modifiers are +0.000%
        /// </summary>
        public SAPtool()
        {
            MasterOutput = new List<Card>();
        }

        /// <summary>
        /// Method to grab a website and return html content as a string
        /// </summary>
        /// <param name="siteAddress">The address of the website to grab</param>
        /// <returns>A String value of the souce code of site</returns>
        private static string getSource(string siteAddress)
        {
            WebRequest req = HttpWebRequest.Create(siteAddress);
            req.Method = "GET";

            string source;
            using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                source = reader.ReadToEnd();
            }

            return source;
        }

        /// <summary>
        /// Method to download and save a file from the web
        /// </summary>
        /// <param name="serverAddress">The address of the server to download files from</param>
        /// <param name="DFgetname">The name of the file to download</param>
        /// <param name="SaveDirectory">The directory where the file will be stored</param>
        /// <param name="DFsavename">The name to save grabbed file as</param>
        private static void downloadFile(string serverAddress, string DFgetname, string DFsavename, string SaveDirectory)
        {
            Console.Write("\t...downloading " + serverAddress.ToString() + DFgetname.ToString() + " to " + SaveDirectory.ToString() + DFsavename + "\n");

            WebClient webClient = new WebClient();
            String getFileLocation = serverAddress + DFgetname;
            String saveFileLocation = SaveDirectory + @"\" + DFsavename;
            webClient.DownloadFile(getFileLocation, saveFileLocation);
        }

        /// <summary>
        /// Test a set to see if it's part of Standard
        /// </summary>
        /// <param name="setName">The set name to test</param>
        /// <returns>The result of the test</returns>
        public bool IsStandard(string setName)
        {
            return STDsetList.Contains(setName.ToLower());
        }

        /// <summary>
        /// Test a set to see if it's part of Extented
        /// </summary>
        /// <param name="setName">The set name to test</param>
        /// <returns>The result of the test</returns>
        public bool IsExtended(string setName)
        {
            return EXTsetList.Contains(setName.ToLower());
        }

        /// <summary>
        /// Test a set to see if it's part of Modern
        /// </summary>
        /// <param name="setName">The set name to test</param>
        /// <returns>The result of the test</returns>
        public bool isModern(string setName)
        {
            return MODsetList.Contains(setName.ToLower());
        }

        /// <summary>
        /// Set the buying quantities for a set of Cards
        /// </summary>
        /// <param name="theCardList">The List of Cards to modify buy quantites for</param>
        /// <param name="BQR">The Buy Qty Regular number</param>
        /// <param name="BQF">The Buy Qty Foil number</param>
        /// <param name="BBQR">The Buy Qty Booster number</param>
        /// <returns>An updated version of the passed List of cards</returns>
        public void SetBuyQuantites(List<Card> cardList, List<string> setNamesToUse, int BQR, int BQF, int BBQR)
        {
            foreach (Card c in cardList)
            {
                if (setNamesToUse.Contains(c.Set.ToLower()))
                {
                    //remove all lands...leave this up to the bot configuration (i.e. user may select to accept zendikar lands)
                    if (c.Name.ToString().Equals("Mountain") || c.Name.ToString().Equals("Plains") || c.Name.ToString().Equals("Island")
                        || c.Name.ToString().Equals("Forest") || c.Name.ToString().Equals("Swamp"))
                    {
                        cardList.RemoveAll(x => x.Name.Equals(c.Name));
                    }
                    else
                    {
                        c.BuyQtyRegular = BQR;
                        c.BuyQtyFoil = BQF;
                    }
                }
                else if (c.Set.Equals("BOO"))
                {
                    if (c.SellPriceRegular >= 2.95m && c.SellPriceRegular <= 3.95m)
                    {
                        c.BuyQtyRegular = BBQR;
                        c.BuyQtyFoil = 0;
                    }
                    else
                    {
                        c.BuyQtyRegular = 0;
                        c.BuyQtyFoil = 0;
                    }

                }
                else
                {
                    c.BuyQtyRegular = 0;
                    c.BuyQtyFoil = 0;
                }
            }
        }

        /// <summary>
        /// Method to parse the CardsMTGO3.txt file
        /// </summary>
        /// <param name="fileLocation">The directory where CardsMTGO3.txt is located</param>
        /// <returns>A List of Cards</returns>
        public List<Card> makeCardsMTGO3List(string fileLocation)
        {
            Console.Write("*** CardsMTGO3 ***\n");
            Console.Write("\t...parsing CardsMTGO3.txt file\n");
            List<Card> cardList = new List<Card>();
            string filePath = fileLocation + @"/" + "CardsMTGO3.txt";
            string[] coreSets = { "2010", "2011", "2012", "2013", "2014" };

            foreach (string line in File.ReadAllLines(filePath))
            {
                Card c = new Card();
                String[] lineChunks = line.Split();
                c.Set = lineChunks[5];
                c.Rarity = lineChunks[6];
                bool nameNext = false;
                bool nameDone = false;
                bool booster = false;
                bool coresetTest = false;

                if (c.Rarity.Equals("U") || c.Rarity.Equals("C")) continue;

                if (line.Contains("Magic"))
                {
                    coresetTest = true;
                }
                else
                {
                    coresetTest = false;
                }

                foreach (string chunk in lineChunks)
                {
                    if (!nameDone)
                    {
                        decimal crap;

                        if (c.Name == null && decimal.TryParse(chunk, out crap)) continue;

                        if (nameDone && booster && coreSets.Contains(chunk.ToString())) continue;

                        if (c.Name != null && decimal.TryParse(chunk, out crap))
                        {
                            if (coresetTest && (chunk.Equals("2010") ||
                                chunk.Equals("2011") || chunk.Equals("2012") ||
                                chunk.Equals("2013") || chunk.Equals("2014")))
                            {
                                for (int i = Array.IndexOf(lineChunks, "Magic") + 1; i < lineChunks.Length; i++)
                                {
                                    c.Name += " " + lineChunks[i].ToString();
                                    //hit the end of the file name, break out of loop
                                    if (c.Name.Contains("#"))
                                    {
                                        break;
                                    }
                                }
                                booster = true;
                            }
                            else
                            {
                                booster = false;
                            }

                            c.Name = c.Name.Replace("#", "");
                            c.NamePlain = c.Name.Replace("'", "").Replace(",", "").ToLower() + "(" + c.Set.ToLower() + ")";
                            nameDone = true;
                            booster = false;
                            if (!coreSets.Contains(chunk.ToString())) c.SellPriceRegular = decimal.Round(crap, 3);
                            continue;
                        }

                        if (c.Set.Equals(chunk)) continue;
                        if (c.Rarity.Equals(chunk))
                        {
                            nameNext = true;
                            continue;
                        }

                        if (nameNext)
                        {
                            if (c.Name == null)
                            {
                                c.Name = chunk;
                                continue;
                            }
                            else
                            {
                                c.Name = c.Name + " " + chunk;
                                continue;
                            }
                        }
                    }
                    if (nameDone && !coreSets.Contains(chunk.ToString()))
                    {
                        decimal aPrice;
                        decimal.TryParse(chunk, out aPrice);

                        //the name is done, now parse the values
                        if (c.SellPriceRegular == 0)
                        {
                            c.SellPriceRegular = decimal.Round(aPrice, 3);
                            continue;
                        }
                        else if (c.SellPriceFoil == 0)
                        {
                            c.SellPriceFoil = decimal.Round(aPrice, 3);
                            continue;
                        }
                        else if (c.BuyPriceRegular == 0)
                        {
                            c.BuyPriceRegular = decimal.Round(aPrice, 3);
                            continue;
                        }
                        else if (c.BuyPriceFoil == 0)
                        {
                            c.BuyPriceFoil = decimal.Round(aPrice, 3);
                            continue;
                        }
                    }
                }
                cardList.Add(c);
            }
            Console.Write("\t...ordering and returing list\n");
            cardList = cardList.OrderBy(x => x.Set).ThenBy(x => x.Name).ToList();
            Console.WriteLine("\t...creating one list to rule them all!!!");
            MasterOutput = cardList;

            return cardList;
        }

        /// <summary>
        /// Method to get, parse and return MTGGOldfish website prices.
        /// </summary>
        /// <param name="website">The web address of the MTGGoldfish page you would like to parse</param>
        /// <param name="ROOTDIRECTORY">The Directory where the source will be downloaded to.</param>
        /// <returns>A List of Cards</returns>
        public List<Card> SaPTCGpaper(string website, string setAbbrv, string ROOTDIRECTORY)
        {

            Console.Write("Scraping " + website.ToString() + "...\n");

            string filePath = ROOTDIRECTORY + @"/" + "LastScrapedSite-Data.txt";
            bool startParse = false;
            string webData = getSource(website);
            char[] delimiterChars = { '<', '>' };
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
            file.WriteLine(webData);
            file.Close();
            var cardList = new List<Card>();
            Card c = new Card();

            Console.Write("Parsing " + website.ToString() + " scrape...\n");

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (line.Contains("TR height=20"))
                {
                    startParse = true;
                }

                //If startParse is not TRUE or the line is empty, carry on
                if (!startParse || string.IsNullOrEmpty(line)) continue;

                String[] lineChunks = Regex.Split(line, "TR height=20");
                foreach (string chunk in lineChunks)
                {
                    string[] ChunkBits = chunk.Split(delimiterChars);
                    foreach (string bit in ChunkBits)
                    {
                        if (ChunkBits.Length > 13)
                        {
                            decimal buyHigh;
                            decimal buyMid;
                            decimal buyLow;
                            c.Set = setAbbrv;
                            c.Name = ChunkBits[5].Substring(6).Replace(" // ", "/");
                            c.NamePlain = c.Name.Replace("'", "").Replace(",", "").ToLower() + "(" + c.Set.ToLower() + ")";
                            c.Rarity = ChunkBits[37].Substring(6);
                            decimal.TryParse((ChunkBits[45].Replace("&nbsp;", "")).Substring(1), out buyHigh);
                            c.PaperHIGH = buyHigh;
                            decimal.TryParse((ChunkBits[53].Replace("&nbsp;", "")).Substring(1), out buyMid);
                            c.PaperMID = buyMid;
                            decimal.TryParse((ChunkBits[61].Replace("&nbsp;", "")).Substring(1), out buyLow);
                            c.PaperLOW = buyLow;
                            if (!c.NamePlain.Equals(""))
                            {
                                cardList.Add(c);
                                c = new Card();
                                break;
                            }
                        }
                    }

                }
            }

            orderCardList(cardList);
            MasterOutput = buildMyMaster(MasterOutput, cardList);
            return cardList;
        }

        /// <summary>
        /// A method to order and return a List of cards
        /// </summary>
        /// <param name="theCardList">The list of cards to order</param>
        /// <returns>A List of cards ordered by set and then name</returns>
        public List<Card> orderCardList(List<Card> theCardList)
        {
            return theCardList.OrderBy(x => x.Set).ThenBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Fix the names of cards based for missing "," etc.
        /// </summary>
        /// <param name="baseList">The list to use names from (normally CardsMTGO3List)</param>
        /// <param name="listToFix">The list to fix names in</param>
        public void fixCardProperties(List<Card> baseList, List<Card> listToFix)
        {
            foreach (Card a in listToFix)
            {
                foreach (Card b in baseList)
                {
                    if (a.NamePlain.Equals(b.NamePlain))
                    {
                        a.Name = b.Name;
                        a.BuyPriceRegular = decimal.Round((a.BuyPriceRegular), 3);
                        a.Rarity = b.Rarity;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// a method to create the union of 2 lists
        /// </summary>
        /// <param name="masterList">A master card list</param>
        /// <param name="mergeList">the list to merge into the master list</param>
        public List<Card> buildMyMaster(List<Card> listA, List<Card> listB)
        {

            List<Card> output = new List<Card>();
            bool cardAdded;

            foreach (Card a in listA)
            {
                Card cCard = new Card();
                cardAdded = false;

                for (int i = 0; i < listB.Count; i++)
                {
                    if (a.NamePlain.Equals(listB[i].NamePlain))
                    {
                        cardAdded = true;

                        cCard.Name = a.Name != null ? a.Name : listB[i].Name;
                        cCard.NamePlain = listB[i].NamePlain != null ? listB[i].NamePlain : a.NamePlain;
                        cCard.Rarity = listB[i].Rarity != null ? listB[i].Rarity : a.Rarity;
                        cCard.Set = listB[i].Set != null ? listB[i].Set : a.Set;
                        cCard.PriceAction = listB[i].PriceAction != null ? listB[i].PriceAction : a.PriceAction;
                        cCard.BuyPriceRegular = listB[i].BuyPriceRegular != 0 ? listB[i].BuyPriceRegular : a.BuyPriceRegular;
                        cCard.SellPriceRegular = listB[i].SellPriceRegular != 0 ? listB[i].SellPriceRegular : a.SellPriceRegular;
                        cCard.BuyPriceFoil = listB[i].BuyPriceFoil != 0 ? listB[i].BuyPriceFoil : a.BuyPriceFoil;
                        cCard.SellPriceFoil = listB[i].SellPriceFoil != 0 ? listB[i].SellPriceFoil : a.SellPriceFoil;
                        cCard.BuyQtyRegular = listB[i].BuyQtyRegular != 0 ? listB[i].BuyQtyRegular : a.BuyQtyRegular;
                        cCard.BuyQtyFoil = listB[i].BuyQtyFoil != 0 ? listB[i].BuyQtyFoil : a.BuyQtyFoil;

                        output.Add(cCard);
                        continue;
                    }
                }

                if (!cardAdded) output.Add(a);
            }

            return output;
        }

        /// <summary>
        /// Remove all NULL cards and provided Rarities
        /// </summary>
        /// <param name="aList">The list to remove cards from</param>
        /// <param name="Rarity">The rarity to remove from the list</param>
        public void removeCardRarity(List<Card> aList, char Rarity)
        {
            aList.RemoveAll(x => x.Rarity == null);
            aList.RemoveAll(x => x.Rarity.Equals(Rarity));
        }

        /// <summary>
        /// A method to remove a list of set names from a list of cards
        /// </summary>
        /// <param name="aList">The list of cards to remove cards from</param>
        /// <param name="setToRemove">A list strings, representing set names to remove</param>
        public void removeCardSets(List<Card> aList, List<string> setToRemove)
        {
            foreach (string setName in setToRemove)
            {
                aList.RemoveAll(x => x.Set.Equals(setName));
            }
        }

        /// <summary>
        /// Method to remove all cards that we are not interested in (not buying either foil or regular)
        /// </summary>
        /// <param name="listToRemoveFrom">A list to pull buying items from</param>
        /// <returns>The returned list of cards we are interested in buying</returns>
        public List<Card> removeNoBuys(List<Card> listToRemoveFrom)
        {
            List<Card> cleanList = new List<Card>();

            foreach (Card c in listToRemoveFrom)
            {
                if (c.BuyQtyFoil > 0 || c.BuyQtyRegular > 0 || isModern(c.Set)) cleanList.Add(c);
            }
            return cleanList;

        }

        /// <summary>
        /// Write out provided LIST of cards in a format that can be read by MTGOLibrary
        /// </summary>
        /// <param name="cardList">The list of cards to write out</param>
        /// <param name="saveFileName">The name to save the file as</param>
        public void CreatePersonalPrices(List<Card> cardList, string saveFileDir, string saveFileName, bool skipZeros)
        {
            Console.Write("\t...writing out " + saveFileDir.ToString() + saveFileName.ToString() + "\n");
            string filePath = saveFileDir + saveFileName;
            File.Create(filePath).Close();
            using (StreamWriter streamWriter = File.CreateText(filePath))
            {
                foreach (Card card in cardList)
                {
                    if (card.Name == null) continue;
                    streamWriter.WriteLine(card.Set + ";"
                        + card.Name + ";"
                        + (skipZeros == true ? (card.SellPriceRegular == 0 ? "" : card.SellPriceRegular.ToString().Replace(',', '.')) : card.SellPriceRegular.ToString().Replace(',', '.')) + ";"
                        + (skipZeros == true ? (card.SellPriceFoil == 0 ? "" : card.SellPriceFoil.ToString().Replace(',', '.')) : card.SellPriceFoil.ToString().Replace(',', '.')) + ";"
                        + (skipZeros == true ? (card.BuyPriceRegular == 0 ? "" : card.BuyPriceRegular.ToString().Replace(',', '.')) : card.BuyPriceRegular.ToString().Replace(',', '.')) + ";"
                        + (skipZeros == true ? (card.BuyPriceFoil == 0 ? "" : card.BuyPriceFoil.ToString().Replace(',', '.')) : card.BuyPriceFoil.ToString().Replace(',', '.')) + ";"
                        + (skipZeros == true ? (card.BuyQtyRegular == -1 ? "" : card.BuyQtyRegular.ToString().Replace(',', '.')) : card.BuyQtyRegular.ToString().Replace(',', '.')) + ";"
                        + (skipZeros == false ? (card.BuyQtyFoil == -1 ? "" : card.BuyQtyFoil.ToString().Replace(',', '.')) : card.BuyQtyFoil.ToString().Replace(',', '.'))
                    );
                }
            }
        }
    }
}