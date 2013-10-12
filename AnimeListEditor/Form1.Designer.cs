namespace AnimeListEditor
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Save = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.animeDS = new AnimeListEditor.AnimeDS();
            this.animeDSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.animeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.animeTableAdapter = new AnimeListEditor.AnimeDSTableAdapters.AnimeTableAdapter();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.timeZoneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchTermDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animeDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animeDSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.animeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Save);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 221);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 41);
            this.panel1.TabIndex = 0;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(13, 6);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 0;
            this.Save.Text = "button1";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.timeDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.timeZoneDataGridViewTextBoxColumn,
            this.searchTermDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.animeBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(284, 221);
            this.dataGridView1.TabIndex = 1;
            // 
            // animeDS
            // 
            this.animeDS.DataSetName = "AnimeDS";
            this.animeDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // animeDSBindingSource
            // 
            this.animeDSBindingSource.DataSource = this.animeDS;
            this.animeDSBindingSource.Position = 0;
            // 
            // animeBindingSource
            // 
            this.animeBindingSource.DataMember = "Anime";
            this.animeBindingSource.DataSource = this.animeDSBindingSource;
            // 
            // animeTableAdapter
            // 
            this.animeTableAdapter.ClearBeforeFill = true;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            // 
            // timeZoneDataGridViewTextBoxColumn
            // 
            this.timeZoneDataGridViewTextBoxColumn.DataPropertyName = "TimeZone";
            this.timeZoneDataGridViewTextBoxColumn.HeaderText = "TimeZone";
            this.timeZoneDataGridViewTextBoxColumn.Name = "timeZoneDataGridViewTextBoxColumn";
            // 
            // searchTermDataGridViewTextBoxColumn
            // 
            this.searchTermDataGridViewTextBoxColumn.DataPropertyName = "SearchTerm";
            this.searchTermDataGridViewTextBoxColumn.HeaderText = "SearchTerm";
            this.searchTermDataGridViewTextBoxColumn.Name = "searchTermDataGridViewTextBoxColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animeDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animeDSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.animeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource animeDSBindingSource;
        private AnimeDS animeDS;
        private System.Windows.Forms.BindingSource animeBindingSource;
        private AnimeDSTableAdapters.AnimeTableAdapter animeTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeZoneDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn searchTermDataGridViewTextBoxColumn;
    }
}