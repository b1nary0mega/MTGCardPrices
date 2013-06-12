using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGCardPrices.Models;
using MTGCardPrices.helpers;

namespace MTGCardPrices
{

    public partial class MTGCardPrices : Form
    {

        private readonly static string ROOTDIRECTORY = "C:";
        private readonly static string DEBUGDIRECTORY = ROOTDIRECTORY + @"/";
        private readonly static string RELEASEDIRECTORY = ROOTDIRECTORY + @"/Program Files/MTGCardPrices/";
        private readonly static string RELEASEDIRECTORYx64 = ROOTDIRECTORY + @"/Program Files (x86)/MTGCardPrices/";
        public List<Card> TCGpaper;

        /// <summary>
        /// Method to initialize all necessary components
        /// </summary>
        public MTGCardPrices()
        {
            InitializeComponent();
        }

        /// <summary>
        /// provide user with about box message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MTG Card Prices version 1.0\nCopyright (c)2013 James R. Aylesworth\njames.aylesworth@gmail.com");
        }

        /// <summary>
        /// exit out of the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// update the status label
        /// </summary>
        /// <param name="NewStatusMsg">new message for status label</param>
        private void updateStatusLbl(string NewStatusMsg)
        {
            StatusLbl.Text = NewStatusMsg;
            StatusLbl.Refresh();
        }

        /// <summary>
        /// download prices and update list view with them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downloadPricesBtn_Click(object sender, EventArgs e)
        {
            this.updateStatusLbl("Status: Downloading TCG Prices for Dragon's Maze");

            SAPtool myTool = new SAPtool();

            //eventually this will be defined based on a listbox the user can select which sets they want to download prices for
            TCGpaper = myTool.SaPTCGpaper("http://magic.tcgplayer.com/db/price_guide.asp?setname=Dragon's%20Maze", "DGM", ROOTDIRECTORY);

            this.updateStatusLbl("Status: Done");

            this.updatePriceListView(TCGpaper);
        }

        /// <summary>
        /// loop through provided list and add them to list view
        /// </summary>
        /// <param name="aList"></param>
        private void updatePriceListView(List<Card> aList)
        {
            //loop through our list of cards and add them to the list view
            foreach (Card c in aList)
            {
                ListViewItem lvi = new ListViewItem(c.Name);
                lvi.SubItems.Add(c.Rarity);
                lvi.SubItems.Add(c.PaperHIGH.ToString());
                lvi.SubItems.Add(c.PaperMID.ToString());
                lvi.SubItems.Add(c.PaperLOW.ToString());
                PriceListView.Items.Add(lvi);
            }
        }

        /// <summary>
        /// return a list of selected items in a list view
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        private List<ListViewItem> GetSelectedListViewItems(ListView lv)
        {
            if (!lv.InvokeRequired)
            {
                return lv.SelectedItems.Cast<ListViewItem>().ToList();
            }
            else
            {
                return (List<ListViewItem>)lv.Invoke(new Func<ListView, List<ListViewItem>>(GetSelectedListViewItems), lv);
            }
        }

        /// <summary>
        /// update the status message with selected card names when one or more is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PriceListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string statusMsg = "Status: ";
            int counter = 0;

            foreach (ListViewItem item in this.GetSelectedListViewItems(PriceListView))
            {
                if (counter > 0) statusMsg += ", ";
                statusMsg += item.SubItems[0].Text;
                this.selectedCardNamelbl.Text = item.SubItems[0].Text;
                this.selectedCardCostLowlbl.Text = item.SubItems[4].Text;
                this.selectedCardCostMedlbl.Text = item.SubItems[3].Text;
                this.selectedCardCostHighlbl.Text = item.SubItems[2].Text;
                counter++;
            }

            statusMsg += " selected.";

            this.updateStatusLbl(statusMsg);
            
        }
    }
}
