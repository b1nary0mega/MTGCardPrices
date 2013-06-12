namespace MTGCardPrices
{
    partial class MTGCardPrices
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MTGCardPrices));
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadPricesBtn = new System.Windows.Forms.Button();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.selectedCardBox = new System.Windows.Forms.PictureBox();
            this.selectedCardNamelbl = new System.Windows.Forms.Label();
            this.cardNamelbl = new System.Windows.Forms.Label();
            this.selectedCardCostLowlbl = new System.Windows.Forms.Label();
            this.selectedCardCostMedlbl = new System.Windows.Forms.Label();
            this.selectedCardCostHighlbl = new System.Windows.Forms.Label();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RarityColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HighColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MediumColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LowColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PriceListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.selectedCardBox)).BeginInit();
            this.SuspendLayout();
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // downloadPricesBtn
            // 
            this.downloadPricesBtn.Location = new System.Drawing.Point(12, 12);
            this.downloadPricesBtn.Name = "downloadPricesBtn";
            this.downloadPricesBtn.Size = new System.Drawing.Size(760, 41);
            this.downloadPricesBtn.TabIndex = 2;
            this.downloadPricesBtn.Text = "Download Prices";
            this.downloadPricesBtn.UseVisualStyleBackColor = true;
            this.downloadPricesBtn.Click += new System.EventHandler(this.downloadPricesBtn_Click);
            // 
            // StatusLbl
            // 
            this.StatusLbl.AutoSize = true;
            this.StatusLbl.Location = new System.Drawing.Point(9, 540);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Size = new System.Drawing.Size(43, 13);
            this.StatusLbl.TabIndex = 3;
            this.StatusLbl.Text = "Status: ";
            // 
            // selectedCardBox
            // 
            this.selectedCardBox.Image = ((System.Drawing.Image)(resources.GetObject("selectedCardBox.Image")));
            this.selectedCardBox.Location = new System.Drawing.Point(849, 59);
            this.selectedCardBox.Name = "selectedCardBox";
            this.selectedCardBox.Size = new System.Drawing.Size(196, 280);
            this.selectedCardBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.selectedCardBox.TabIndex = 5;
            this.selectedCardBox.TabStop = false;
            // 
            // selectedCardNamelbl
            // 
            this.selectedCardNamelbl.AutoSize = true;
            this.selectedCardNamelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedCardNamelbl.Location = new System.Drawing.Point(846, 358);
            this.selectedCardNamelbl.Name = "selectedCardNamelbl";
            this.selectedCardNamelbl.Size = new System.Drawing.Size(0, 20);
            this.selectedCardNamelbl.TabIndex = 6;
            // 
            // cardNamelbl
            // 
            this.cardNamelbl.AutoSize = true;
            this.cardNamelbl.Location = new System.Drawing.Point(846, 368);
            this.cardNamelbl.Name = "cardNamelbl";
            this.cardNamelbl.Size = new System.Drawing.Size(38, 13);
            this.cardNamelbl.TabIndex = 7;
            this.cardNamelbl.Text = "Name:";
            // 
            // selectedCardCostLowlbl
            // 
            this.selectedCardCostLowlbl.AutoSize = true;
            this.selectedCardCostLowlbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.selectedCardCostLowlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedCardCostLowlbl.Location = new System.Drawing.Point(846, 400);
            this.selectedCardCostLowlbl.Name = "selectedCardCostLowlbl";
            this.selectedCardCostLowlbl.Size = new System.Drawing.Size(0, 20);
            this.selectedCardCostLowlbl.TabIndex = 7;
            // 
            // selectedCardCostMedlbl
            // 
            this.selectedCardCostMedlbl.AutoSize = true;
            this.selectedCardCostMedlbl.BackColor = System.Drawing.Color.DodgerBlue;
            this.selectedCardCostMedlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedCardCostMedlbl.Location = new System.Drawing.Point(927, 400);
            this.selectedCardCostMedlbl.Name = "selectedCardCostMedlbl";
            this.selectedCardCostMedlbl.Size = new System.Drawing.Size(0, 20);
            this.selectedCardCostMedlbl.TabIndex = 8;
            // 
            // selectedCardCostHighlbl
            // 
            this.selectedCardCostHighlbl.AutoSize = true;
            this.selectedCardCostHighlbl.BackColor = System.Drawing.Color.LightGreen;
            this.selectedCardCostHighlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedCardCostHighlbl.Location = new System.Drawing.Point(996, 400);
            this.selectedCardCostHighlbl.Name = "selectedCardCostHighlbl";
            this.selectedCardCostHighlbl.Size = new System.Drawing.Size(0, 20);
            this.selectedCardCostHighlbl.TabIndex = 9;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 380;
            // 
            // RarityColumn
            // 
            this.RarityColumn.Text = "Rarity";
            // 
            // HighColumn
            // 
            this.HighColumn.Text = "High";
            this.HighColumn.Width = 100;
            // 
            // MediumColumn
            // 
            this.MediumColumn.Text = "Medium";
            this.MediumColumn.Width = 100;
            // 
            // LowColumn
            // 
            this.LowColumn.Text = "Low";
            this.LowColumn.Width = 100;
            // 
            // PriceListView
            // 
            this.PriceListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.RarityColumn,
            this.HighColumn,
            this.MediumColumn,
            this.LowColumn});
            this.PriceListView.FullRowSelect = true;
            this.PriceListView.GridLines = true;
            this.PriceListView.Location = new System.Drawing.Point(12, 59);
            this.PriceListView.Name = "PriceListView";
            this.PriceListView.Size = new System.Drawing.Size(760, 478);
            this.PriceListView.TabIndex = 4;
            this.PriceListView.UseCompatibleStateImageBehavior = false;
            this.PriceListView.View = System.Windows.Forms.View.Details;
            this.PriceListView.SelectedIndexChanged += new System.EventHandler(this.PriceListView_SelectedIndexChanged);
            // 
            // MTGCardPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 562);
            this.Controls.Add(this.selectedCardCostHighlbl);
            this.Controls.Add(this.selectedCardCostMedlbl);
            this.Controls.Add(this.selectedCardCostLowlbl);
            this.Controls.Add(this.selectedCardNamelbl);
            this.Controls.Add(this.selectedCardBox);
            this.Controls.Add(this.PriceListView);
            this.Controls.Add(this.StatusLbl);
            this.Controls.Add(this.downloadPricesBtn);
            this.Name = "MTGCardPrices";
            this.Text = "MTG Card Prices";
            ((System.ComponentModel.ISupportInitialize)(this.selectedCardBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button downloadPricesBtn;
        private System.Windows.Forms.Label StatusLbl;
        private System.Windows.Forms.PictureBox selectedCardBox;
        private System.Windows.Forms.Label selectedCardNamelbl;
        private System.Windows.Forms.Label cardNamelbl;
        private System.Windows.Forms.Label selectedCardCostLowlbl;
        private System.Windows.Forms.Label selectedCardCostMedlbl;
        private System.Windows.Forms.Label selectedCardCostHighlbl;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader RarityColumn;
        private System.Windows.Forms.ColumnHeader HighColumn;
        private System.Windows.Forms.ColumnHeader MediumColumn;
        private System.Windows.Forms.ColumnHeader LowColumn;
        private System.Windows.Forms.ListView PriceListView;
    }
}

