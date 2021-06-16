namespace ComparativeClustering
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtIndexA = new System.Windows.Forms.TextBox();
            this.txtIndexB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCentroid = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.RandomColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DissimilarityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCluster = new System.Windows.Forms.Button();
            this.txtExcelBox = new System.Windows.Forms.TextBox();
            this.btnExcelFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblCentroidIndex = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCentroidIndex);
            this.panel1.Controls.Add(this.txtIndexA);
            this.panel1.Controls.Add(this.txtIndexB);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblCentroid);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.btnCluster);
            this.panel1.Controls.Add(this.txtExcelBox);
            this.panel1.Controls.Add(this.btnExcelFile);
            this.panel1.Location = new System.Drawing.Point(3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1283, 640);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtIndexA
            // 
            this.txtIndexA.Location = new System.Drawing.Point(78, 319);
            this.txtIndexA.Name = "txtIndexA";
            this.txtIndexA.Size = new System.Drawing.Size(74, 26);
            this.txtIndexA.TabIndex = 7;
            // 
            // txtIndexB
            // 
            this.txtIndexB.Location = new System.Drawing.Point(78, 364);
            this.txtIndexB.Name = "txtIndexB";
            this.txtIndexB.Size = new System.Drawing.Size(74, 26);
            this.txtIndexB.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 364);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Index B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Get Centroids from index";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 319);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Index A";
            // 
            // lblCentroid
            // 
            this.lblCentroid.AutoSize = true;
            this.lblCentroid.Location = new System.Drawing.Point(246, 66);
            this.lblCentroid.Name = "lblCentroid";
            this.lblCentroid.Size = new System.Drawing.Size(0, 20);
            this.lblCentroid.TabIndex = 5;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(1076, 80);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(168, 40);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Refresh Table";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "ITERATION";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "TIME";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "METHODS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RandomColumn,
            this.DissimilarityColumn});
            this.dataGridView1.Location = new System.Drawing.Point(241, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(800, 534);
            this.dataGridView1.TabIndex = 3;
            // 
            // RandomColumn
            // 
            this.RandomColumn.HeaderText = "Standard Deviation";
            this.RandomColumn.MinimumWidth = 8;
            this.RandomColumn.Name = "RandomColumn";
            this.RandomColumn.Width = 300;
            // 
            // DissimilarityColumn
            // 
            this.DissimilarityColumn.HeaderText = "Dissimilarity Degree";
            this.DissimilarityColumn.MinimumWidth = 8;
            this.DissimilarityColumn.Name = "DissimilarityColumn";
            this.DissimilarityColumn.Width = 368;
            // 
            // btnCluster
            // 
            this.btnCluster.Location = new System.Drawing.Point(1076, 17);
            this.btnCluster.Name = "btnCluster";
            this.btnCluster.Size = new System.Drawing.Size(168, 35);
            this.btnCluster.TabIndex = 2;
            this.btnCluster.Text = "Cluster";
            this.btnCluster.UseVisualStyleBackColor = true;
            this.btnCluster.Click += new System.EventHandler(this.btnCluster_Click);
            // 
            // txtExcelBox
            // 
            this.txtExcelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelBox.Location = new System.Drawing.Point(241, 21);
            this.txtExcelBox.Name = "txtExcelBox";
            this.txtExcelBox.Size = new System.Drawing.Size(800, 28);
            this.txtExcelBox.TabIndex = 1;
            this.txtExcelBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnExcelFile
            // 
            this.btnExcelFile.Location = new System.Drawing.Point(27, 18);
            this.btnExcelFile.Name = "btnExcelFile";
            this.btnExcelFile.Size = new System.Drawing.Size(183, 35);
            this.btnExcelFile.TabIndex = 0;
            this.btnExcelFile.Text = "Choose Excel File";
            this.btnExcelFile.UseVisualStyleBackColor = true;
            this.btnExcelFile.Click += new System.EventHandler(this.btnExcelFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblCentroidIndex
            // 
            this.lblCentroidIndex.AutoSize = true;
            this.lblCentroidIndex.Location = new System.Drawing.Point(705, 66);
            this.lblCentroidIndex.Name = "lblCentroidIndex";
            this.lblCentroidIndex.Size = new System.Drawing.Size(0, 20);
            this.lblCentroidIndex.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 646);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comparative Clustering";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtExcelBox;
        private System.Windows.Forms.Button btnExcelFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCluster;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblCentroid;
        private System.Windows.Forms.TextBox txtIndexB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIndexA;
        private System.Windows.Forms.DataGridViewTextBoxColumn RandomColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DissimilarityColumn;
        private System.Windows.Forms.Label lblCentroidIndex;
    }
}

