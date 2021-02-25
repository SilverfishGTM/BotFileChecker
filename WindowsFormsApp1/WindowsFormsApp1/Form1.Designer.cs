namespace WindowsFormsApp1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.StartButton = new System.Windows.Forms.Button();
            this.RA2DirectoryBox = new System.Windows.Forms.TextBox();
            this.RA2DirectoryLabel = new System.Windows.Forms.Label();
            this.RA2BrowseButton = new System.Windows.Forms.Button();
            this.BotDirectoryBox = new System.Windows.Forms.TextBox();
            this.BotBrowseButton = new System.Windows.Forms.Button();
            this.BotPath = new System.Windows.Forms.Label();
            this.ExtenderbotBox = new System.Windows.Forms.CheckBox();
            this.ComponentListBox = new System.Windows.Forms.CheckBox();
            this.OutputPathBox = new System.Windows.Forms.TextBox();
            this.OutputPathLabel = new System.Windows.Forms.Label();
            this.OutputBrowseButton = new System.Windows.Forms.Button();
            this.ArmorPathButton = new System.Windows.Forms.Button();
            this.ArmorPathLabel = new System.Windows.Forms.Label();
            this.ArmorPathBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(8, 321);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(285, 48);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Check File";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // RA2DirectoryBox
            // 
            this.RA2DirectoryBox.Location = new System.Drawing.Point(8, 98);
            this.RA2DirectoryBox.Name = "RA2DirectoryBox";
            this.RA2DirectoryBox.Size = new System.Drawing.Size(187, 22);
            this.RA2DirectoryBox.TabIndex = 1;
            this.RA2DirectoryBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // RA2DirectoryLabel
            // 
            this.RA2DirectoryLabel.AutoSize = true;
            this.RA2DirectoryLabel.Location = new System.Drawing.Point(9, 75);
            this.RA2DirectoryLabel.Name = "RA2DirectoryLabel";
            this.RA2DirectoryLabel.Size = new System.Drawing.Size(100, 17);
            this.RA2DirectoryLabel.TabIndex = 2;
            this.RA2DirectoryLabel.Text = "RA2 Directory:";
            this.RA2DirectoryLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // RA2BrowseButton
            // 
            this.RA2BrowseButton.Location = new System.Drawing.Point(202, 98);
            this.RA2BrowseButton.Name = "RA2BrowseButton";
            this.RA2BrowseButton.Size = new System.Drawing.Size(75, 23);
            this.RA2BrowseButton.TabIndex = 3;
            this.RA2BrowseButton.Text = "Browse";
            this.RA2BrowseButton.UseVisualStyleBackColor = true;
            this.RA2BrowseButton.Click += new System.EventHandler(this.RA2BrowseButton_Click);
            // 
            // BotDirectoryBox
            // 
            this.BotDirectoryBox.Location = new System.Drawing.Point(12, 30);
            this.BotDirectoryBox.Name = "BotDirectoryBox";
            this.BotDirectoryBox.Size = new System.Drawing.Size(183, 22);
            this.BotDirectoryBox.TabIndex = 5;
            this.BotDirectoryBox.TextChanged += new System.EventHandler(this.BotDirectoryBox_TextChanged);
            // 
            // BotBrowseButton
            // 
            this.BotBrowseButton.Location = new System.Drawing.Point(202, 30);
            this.BotBrowseButton.Name = "BotBrowseButton";
            this.BotBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.BotBrowseButton.TabIndex = 6;
            this.BotBrowseButton.Text = "Browse";
            this.BotBrowseButton.UseVisualStyleBackColor = true;
            this.BotBrowseButton.Click += new System.EventHandler(this.BotBrowseButton_Click);
            // 
            // BotPath
            // 
            this.BotPath.AutoSize = true;
            this.BotPath.Location = new System.Drawing.Point(9, 10);
            this.BotPath.Name = "BotPath";
            this.BotPath.Size = new System.Drawing.Size(87, 17);
            this.BotPath.TabIndex = 7;
            this.BotPath.Text = "Bot file path:";
            // 
            // ExtenderbotBox
            // 
            this.ExtenderbotBox.AutoSize = true;
            this.ExtenderbotBox.Location = new System.Drawing.Point(8, 259);
            this.ExtenderbotBox.Name = "ExtenderbotBox";
            this.ExtenderbotBox.Size = new System.Drawing.Size(173, 21);
            this.ExtenderbotBox.TabIndex = 8;
            this.ExtenderbotBox.Text = "Is this an extenderbot?";
            this.ExtenderbotBox.UseVisualStyleBackColor = true;
            this.ExtenderbotBox.CheckedChanged += new System.EventHandler(this.ExtenderbotBox_CheckedChanged);
            // 
            // ComponentListBox
            // 
            this.ComponentListBox.AutoSize = true;
            this.ComponentListBox.Location = new System.Drawing.Point(8, 293);
            this.ComponentListBox.Name = "ComponentListBox";
            this.ComponentListBox.Size = new System.Drawing.Size(192, 21);
            this.ComponentListBox.TabIndex = 10;
            this.ComponentListBox.Text = "Generate Component List";
            this.ComponentListBox.UseVisualStyleBackColor = true;
            this.ComponentListBox.CheckedChanged += new System.EventHandler(this.ComponentListBox_CheckedChanged);
            // 
            // OutputPathBox
            // 
            this.OutputPathBox.Location = new System.Drawing.Point(8, 161);
            this.OutputPathBox.Name = "OutputPathBox";
            this.OutputPathBox.Size = new System.Drawing.Size(187, 22);
            this.OutputPathBox.TabIndex = 11;
            this.OutputPathBox.TextChanged += new System.EventHandler(this.OutputPathBox_TextChanged);
            // 
            // OutputPathLabel
            // 
            this.OutputPathLabel.AutoSize = true;
            this.OutputPathLabel.Location = new System.Drawing.Point(8, 138);
            this.OutputPathLabel.Name = "OutputPathLabel";
            this.OutputPathLabel.Size = new System.Drawing.Size(88, 17);
            this.OutputPathLabel.TabIndex = 12;
            this.OutputPathLabel.Text = "Output Path:";
            // 
            // OutputBrowseButton
            // 
            this.OutputBrowseButton.Location = new System.Drawing.Point(202, 161);
            this.OutputBrowseButton.Name = "OutputBrowseButton";
            this.OutputBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.OutputBrowseButton.TabIndex = 13;
            this.OutputBrowseButton.Text = "Browse";
            this.OutputBrowseButton.UseVisualStyleBackColor = true;
            this.OutputBrowseButton.Click += new System.EventHandler(this.OutputBrowseButton_Click);
            // 
            // ArmorPathButton
            // 
            this.ArmorPathButton.Location = new System.Drawing.Point(202, 224);
            this.ArmorPathButton.Name = "ArmorPathButton";
            this.ArmorPathButton.Size = new System.Drawing.Size(75, 23);
            this.ArmorPathButton.TabIndex = 16;
            this.ArmorPathButton.Text = "Browse";
            this.ArmorPathButton.UseVisualStyleBackColor = true;
            this.ArmorPathButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ArmorPathLabel
            // 
            this.ArmorPathLabel.AutoSize = true;
            this.ArmorPathLabel.Location = new System.Drawing.Point(8, 201);
            this.ArmorPathLabel.Name = "ArmorPathLabel";
            this.ArmorPathLabel.Size = new System.Drawing.Size(174, 17);
            this.ArmorPathLabel.TabIndex = 15;
            this.ArmorPathLabel.Text = "Armor Definitions Filepath:";
            // 
            // ArmorPathBox
            // 
            this.ArmorPathBox.Location = new System.Drawing.Point(8, 224);
            this.ArmorPathBox.Name = "ArmorPathBox";
            this.ArmorPathBox.Size = new System.Drawing.Size(187, 22);
            this.ArmorPathBox.TabIndex = 14;
            this.ArmorPathBox.TextChanged += new System.EventHandler(this.ArmorPathBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 376);
            this.Controls.Add(this.ArmorPathButton);
            this.Controls.Add(this.ArmorPathLabel);
            this.Controls.Add(this.ArmorPathBox);
            this.Controls.Add(this.OutputBrowseButton);
            this.Controls.Add(this.OutputPathLabel);
            this.Controls.Add(this.OutputPathBox);
            this.Controls.Add(this.ComponentListBox);
            this.Controls.Add(this.ExtenderbotBox);
            this.Controls.Add(this.BotPath);
            this.Controls.Add(this.BotBrowseButton);
            this.Controls.Add(this.BotDirectoryBox);
            this.Controls.Add(this.RA2BrowseButton);
            this.Controls.Add(this.RA2DirectoryLabel);
            this.Controls.Add(this.RA2DirectoryBox);
            this.Controls.Add(this.StartButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(335, 423);
            this.Name = "Form1";
            this.Text = "RA2 Bot File Checker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox RA2DirectoryBox;
        private System.Windows.Forms.Label RA2DirectoryLabel;
        private System.Windows.Forms.Button RA2BrowseButton;
        private System.Windows.Forms.TextBox BotDirectoryBox;
        private System.Windows.Forms.Button BotBrowseButton;
        private System.Windows.Forms.Label BotPath;
        private System.Windows.Forms.CheckBox ExtenderbotBox;
        private System.Windows.Forms.CheckBox ComponentListBox;
        private System.Windows.Forms.TextBox OutputPathBox;
        private System.Windows.Forms.Label OutputPathLabel;
        private System.Windows.Forms.Button OutputBrowseButton;
        private System.Windows.Forms.Button ArmorPathButton;
        private System.Windows.Forms.Label ArmorPathLabel;
        private System.Windows.Forms.TextBox ArmorPathBox;
    }
}

