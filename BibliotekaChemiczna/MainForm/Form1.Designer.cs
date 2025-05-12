using System;
using System.Drawing;
using System.Windows.Forms;

namespace MainForm
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private ListBox listBoxSubstancje;
        private ComboBox comboBoxFiltr;
        private Label labelEfekt;
        private Button buttonPolacz;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem zapiszToolStripMenuItem;
        private ToolStripMenuItem wczytajToolStripMenuItem;
        private DataGridView dataGridMieszanki;
        private ToolTip toolTip1;
        private Panel panelTablica;

        private DataGridViewTextBoxColumn kolSkladniki;
        private DataGridViewTextBoxColumn kolEfekt;
        private DataGridViewTextBoxColumn kolKolor;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            listBoxSubstancje = new ListBox();
            comboBoxFiltr = new ComboBox();
            labelEfekt = new Label();
            buttonPolacz = new Button();
            menuStrip1 = new MenuStrip();
            zapiszToolStripMenuItem = new ToolStripMenuItem();
            wczytajToolStripMenuItem = new ToolStripMenuItem();
            dataGridMieszanki = new DataGridView();
            kolSkladniki = new DataGridViewTextBoxColumn();
            kolEfekt = new DataGridViewTextBoxColumn();
            kolKolor = new DataGridViewTextBoxColumn();
            toolTip1 = new ToolTip(components);
            panelTablica = new Panel();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridMieszanki).BeginInit();
            SuspendLayout();
            // 
            // listBoxSubstancje
            // 
            listBoxSubstancje.BackColor = Color.White;
            listBoxSubstancje.BorderStyle = BorderStyle.FixedSingle;
            listBoxSubstancje.Font = new Font("Segoe UI", 10F);
            listBoxSubstancje.ItemHeight = 23;
            listBoxSubstancje.Location = new Point(30, 90);
            listBoxSubstancje.Name = "listBoxSubstancje";
            listBoxSubstancje.SelectionMode = SelectionMode.MultiExtended;
            listBoxSubstancje.Size = new Size(240, 186);
            listBoxSubstancje.TabIndex = 2;
            // 
            // comboBoxFiltr
            // 
            comboBoxFiltr.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxFiltr.Font = new Font("Segoe UI", 10F);
            comboBoxFiltr.Location = new Point(30, 50);
            comboBoxFiltr.Name = "comboBoxFiltr";
            comboBoxFiltr.Size = new Size(240, 31);
            comboBoxFiltr.TabIndex = 1;
            comboBoxFiltr.SelectedIndexChanged += comboBoxFiltr_SelectedIndexChanged;
            // 
            // labelEfekt
            // 
            labelEfekt.BackColor = Color.Transparent;
            labelEfekt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelEfekt.Location = new Point(30, 293);
            labelEfekt.Name = "labelEfekt";
            labelEfekt.Size = new Size(930, 30);
            labelEfekt.TabIndex = 5;
            labelEfekt.Text = "Efekt:";
            // 
            // buttonPolacz
            // 
            buttonPolacz.BackColor = Color.MediumSlateBlue;
            buttonPolacz.Cursor = Cursors.Hand;
            buttonPolacz.FlatAppearance.BorderSize = 0;
            buttonPolacz.FlatStyle = FlatStyle.Flat;
            buttonPolacz.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonPolacz.ForeColor = Color.White;
            buttonPolacz.Location = new Point(290, 140);
            buttonPolacz.Name = "buttonPolacz";
            buttonPolacz.Size = new Size(100, 40);
            buttonPolacz.TabIndex = 3;
            buttonPolacz.Text = "Połącz";
            buttonPolacz.UseVisualStyleBackColor = false;
            buttonPolacz.Click += buttonPolacz_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.WhiteSmoke;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { zapiszToolStripMenuItem, wczytajToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1000, 28);
            menuStrip1.TabIndex = 0;
            // 
            // zapiszToolStripMenuItem
            // 
            zapiszToolStripMenuItem.Name = "zapiszToolStripMenuItem";
            zapiszToolStripMenuItem.Size = new Size(66, 24);
            zapiszToolStripMenuItem.Text = "Zapisz";
            zapiszToolStripMenuItem.Click += zapiszToolStripMenuItem_Click;
            // 
            // wczytajToolStripMenuItem
            // 
            wczytajToolStripMenuItem.Name = "wczytajToolStripMenuItem";
            wczytajToolStripMenuItem.Size = new Size(74, 24);
            wczytajToolStripMenuItem.Text = "Wczytaj";
            wczytajToolStripMenuItem.Click += wczytajToolStripMenuItem_Click;
            // 
            // dataGridMieszanki
            // 
            dataGridMieszanki.AllowUserToAddRows = false;
            dataGridMieszanki.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.SteelBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridMieszanki.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridMieszanki.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridMieszanki.Columns.AddRange(new DataGridViewColumn[] { kolSkladniki, kolEfekt, kolKolor });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridMieszanki.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridMieszanki.EnableHeadersVisualStyles = false;
            dataGridMieszanki.Font = new Font("Segoe UI", 10F);
            dataGridMieszanki.GridColor = Color.LightGray;
            dataGridMieszanki.Location = new Point(400, 50);
            dataGridMieszanki.Name = "dataGridMieszanki";
            dataGridMieszanki.ReadOnly = true;
            dataGridMieszanki.RowHeadersWidth = 51;
            dataGridMieszanki.Size = new Size(560, 240);
            dataGridMieszanki.TabIndex = 4;
            // 
            // kolSkladniki
            // 
            kolSkladniki.HeaderText = "Składniki";
            kolSkladniki.MinimumWidth = 6;
            kolSkladniki.Name = "kolSkladniki";
            kolSkladniki.ReadOnly = true;
            // 
            // kolEfekt
            // 
            kolEfekt.HeaderText = "Efekt";
            kolEfekt.MinimumWidth = 6;
            kolEfekt.Name = "kolEfekt";
            kolEfekt.ReadOnly = true;
            // 
            // kolKolor
            // 
            kolKolor.HeaderText = "Kolor";
            kolKolor.MinimumWidth = 6;
            kolKolor.Name = "kolKolor";
            kolKolor.ReadOnly = true;
            // 
            // panelTablica
            // 
            panelTablica.AutoScroll = true;
            panelTablica.BackColor = Color.Gainsboro;
            panelTablica.BorderStyle = BorderStyle.FixedSingle;
            panelTablica.Location = new Point(57, 326);
            panelTablica.Name = "panelTablica";
            panelTablica.Padding = new Padding(10);
            panelTablica.Size = new Size(880, 460);
            panelTablica.TabIndex = 6;
            // 
            // Form1
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1000, 850);
            Controls.Add(menuStrip1);
            Controls.Add(comboBoxFiltr);
            Controls.Add(listBoxSubstancje);
            Controls.Add(buttonPolacz);
            Controls.Add(dataGridMieszanki);
            Controls.Add(labelEfekt);
            Controls.Add(panelTablica);
            Font = new Font("Segoe UI", 10F);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "\U0001f9ea Eksperymentator Chemiczny";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridMieszanki).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
    
}
