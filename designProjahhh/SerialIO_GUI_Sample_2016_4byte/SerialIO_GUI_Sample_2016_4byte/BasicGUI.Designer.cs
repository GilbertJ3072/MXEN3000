using System.Drawing;
using System.Windows.Forms;

namespace SerialGUISample
{
    partial class Form1
    {
        private Button toggleModeButton, resetIntegral, reCallibrate;
        private TextBox KdBox, KdTitle, KpBox, KpTitle, KiBox, KiTitle, Error1, Error2, Integral, LMinBox, LMaxBox, RMinBox, RMaxBox, FlipBox, DelayBox;

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
            this.toggleModeButton = new System.Windows.Forms.Button();
            this.resetIntegral = new System.Windows.Forms.Button();
            this.reCallibrate = new System.Windows.Forms.Button();
            this.serial = new System.IO.Ports.SerialPort(this.components);
            this.getIOtimer = new System.Windows.Forms.Timer(this.components);
            this.InputBox1 = new System.Windows.Forms.TextBox();
            this.OutputBox1 = new System.Windows.Forms.NumericUpDown();
            this.Send1 = new System.Windows.Forms.Button();
            this.Send2 = new System.Windows.Forms.Button();
            this.Get1 = new System.Windows.Forms.Button();
            this.Get2 = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.InputBox2 = new System.Windows.Forms.TextBox();
            this.OutputBox2 = new System.Windows.Forms.NumericUpDown();
            this.KdBox = new System.Windows.Forms.TextBox();
            this.KdTitle = new System.Windows.Forms.TextBox();
            this.KpBox = new System.Windows.Forms.TextBox();
            this.KpTitle = new System.Windows.Forms.TextBox();
            this.KiBox = new System.Windows.Forms.TextBox();
            this.KiTitle = new System.Windows.Forms.TextBox();
            this.Error1 = new System.Windows.Forms.TextBox();
            this.Error2 = new System.Windows.Forms.TextBox();
            this.Integral = new System.Windows.Forms.TextBox();
            this.LMinBox = new System.Windows.Forms.TextBox();
            this.LMaxBox = new System.Windows.Forms.TextBox();
            this.RMinBox = new System.Windows.Forms.TextBox();
            this.RMaxBox = new System.Windows.Forms.TextBox();
            this.FlipBox = new System.Windows.Forms.TextBox();
            this.DelayBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // toggleModeButton
            // 
            this.toggleModeButton.Location = new System.Drawing.Point(67, 14);
            this.toggleModeButton.Name = "toggleModeButton";
            this.toggleModeButton.Size = new System.Drawing.Size(209, 43);
            this.toggleModeButton.TabIndex = 6;
            this.toggleModeButton.Text = "Manual";
            this.toggleModeButton.UseVisualStyleBackColor = true;
            this.toggleModeButton.Click += new System.EventHandler(this.ToggleModeButton_Click);
            // 
            // resetIntegral
            // 
            this.resetIntegral.Location = new System.Drawing.Point(829, 270);
            this.resetIntegral.Name = "resetIntegral";
            this.resetIntegral.Size = new System.Drawing.Size(209, 43);
            this.resetIntegral.TabIndex = 6;
            this.resetIntegral.Text = "RESET INTEGRAL";
            this.resetIntegral.UseVisualStyleBackColor = true;
            this.resetIntegral.Click += new System.EventHandler(this.resetIntegral_Click);
            // 
            // reCallibrate
            // 
            this.reCallibrate.BackColor = System.Drawing.Color.Yellow;
            this.reCallibrate.Location = new System.Drawing.Point(829, 171);
            this.reCallibrate.Name = "reCallibrate";
            this.reCallibrate.Size = new System.Drawing.Size(226, 85);
            this.reCallibrate.TabIndex = 6;
            this.reCallibrate.Text = "recallibrate";
            this.reCallibrate.UseVisualStyleBackColor = false;
            this.reCallibrate.Click += new System.EventHandler(this.reCallibrate_Click);
            // 
            // serial
            // 
            this.serial.PortName = "COM5";
            // 
            // getIOtimer
            // 
            this.getIOtimer.Enabled = true;
            this.getIOtimer.Interval = 10;
            this.getIOtimer.Tick += new System.EventHandler(this.getIOtimer_Tick);
            // 
            // InputBox1
            // 
            this.InputBox1.Location = new System.Drawing.Point(67, 171);
            this.InputBox1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.InputBox1.Name = "InputBox1";
            this.InputBox1.Size = new System.Drawing.Size(207, 30);
            this.InputBox1.TabIndex = 0;
            this.InputBox1.Text = "0";
            // 
            // OutputBox1
            // 
            this.OutputBox1.DecimalPlaces = 1;
            this.OutputBox1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.OutputBox1.Location = new System.Drawing.Point(67, 75);
            this.OutputBox1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.OutputBox1.Name = "OutputBox1";
            this.OutputBox1.Size = new System.Drawing.Size(209, 30);
            this.OutputBox1.TabIndex = 3;
            this.OutputBox1.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.OutputBox1.ValueChanged += new System.EventHandler(this.OutputBox1_ValueChanged);
            // 
            // Send1
            // 
            this.Send1.Location = new System.Drawing.Point(284, 69);
            this.Send1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Send1.Name = "Send1";
            this.Send1.Size = new System.Drawing.Size(124, 40);
            this.Send1.TabIndex = 4;
            this.Send1.Text = "Output 1";
            this.Send1.UseVisualStyleBackColor = true;
            this.Send1.Click += new System.EventHandler(this.Send1_Click);
            // 
            // Send2
            // 
            this.Send2.Location = new System.Drawing.Point(284, 121);
            this.Send2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Send2.Name = "Send2";
            this.Send2.Size = new System.Drawing.Size(124, 40);
            this.Send2.TabIndex = 4;
            this.Send2.Text = "Output 2";
            this.Send2.UseVisualStyleBackColor = true;
            this.Send2.Click += new System.EventHandler(this.Send2_Click);
            // 
            // Get1
            // 
            this.Get1.Location = new System.Drawing.Point(283, 168);
            this.Get1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Get1.Name = "Get1";
            this.Get1.Size = new System.Drawing.Size(124, 40);
            this.Get1.TabIndex = 4;
            this.Get1.Text = "Input 1";
            this.Get1.UseVisualStyleBackColor = true;
            this.Get1.Click += new System.EventHandler(this.Get1_Click);
            // 
            // Get2
            // 
            this.Get2.Location = new System.Drawing.Point(283, 220);
            this.Get2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Get2.Name = "Get2";
            this.Get2.Size = new System.Drawing.Size(124, 40);
            this.Get2.TabIndex = 4;
            this.Get2.Text = "Input 2";
            this.Get2.UseVisualStyleBackColor = true;
            this.Get2.Click += new System.EventHandler(this.Get2_Click);
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(67, 322);
            this.statusBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(207, 30);
            this.statusBox.TabIndex = 5;
            // 
            // InputBox2
            // 
            this.InputBox2.Location = new System.Drawing.Point(67, 220);
            this.InputBox2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.InputBox2.Name = "InputBox2";
            this.InputBox2.Size = new System.Drawing.Size(207, 30);
            this.InputBox2.TabIndex = 0;
            this.InputBox2.Text = "0";
            // 
            // OutputBox2
            // 
            this.OutputBox2.DecimalPlaces = 1;
            this.OutputBox2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.OutputBox2.Location = new System.Drawing.Point(67, 121);
            this.OutputBox2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.OutputBox2.Name = "OutputBox2";
            this.OutputBox2.Size = new System.Drawing.Size(209, 30);
            this.OutputBox2.TabIndex = 3;
            this.OutputBox2.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // KdBox
            // 
            this.KdBox.Location = new System.Drawing.Point(576, 131);
            this.KdBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.KdBox.Name = "KdBox";
            this.KdBox.Size = new System.Drawing.Size(207, 30);
            this.KdBox.TabIndex = 0;
            this.KdBox.Text = "0.0";
            this.KdBox.TextChanged += new System.EventHandler(this.KdBox_TextChanged);
            // 
            // KdTitle
            // 
            this.KdTitle.Location = new System.Drawing.Point(502, 131);
            this.KdTitle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.KdTitle.Name = "KdTitle";
            this.KdTitle.ReadOnly = true;
            this.KdTitle.Size = new System.Drawing.Size(45, 30);
            this.KdTitle.TabIndex = 7;
            this.KdTitle.Text = "Kd";
            // 
            // KpBox
            // 
            this.KpBox.Location = new System.Drawing.Point(576, 69);
            this.KpBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.KpBox.Name = "KpBox";
            this.KpBox.Size = new System.Drawing.Size(207, 30);
            this.KpBox.TabIndex = 0;
            this.KpBox.Text = "0.0";
            this.KpBox.TextChanged += new System.EventHandler(this.KdBox_TextChanged);
            // 
            // KpTitle
            // 
            this.KpTitle.Location = new System.Drawing.Point(502, 69);
            this.KpTitle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.KpTitle.Name = "KpTitle";
            this.KpTitle.ReadOnly = true;
            this.KpTitle.Size = new System.Drawing.Size(45, 30);
            this.KpTitle.TabIndex = 7;
            this.KpTitle.Text = "Kp";
            // 
            // KiBox
            // 
            this.KiBox.Location = new System.Drawing.Point(576, 193);
            this.KiBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.KiBox.Name = "KiBox";
            this.KiBox.Size = new System.Drawing.Size(207, 30);
            this.KiBox.TabIndex = 0;
            this.KiBox.Text = "0.0";
            this.KiBox.TextChanged += new System.EventHandler(this.KdBox_TextChanged);
            // 
            // KiTitle
            // 
            this.KiTitle.Location = new System.Drawing.Point(502, 193);
            this.KiTitle.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.KiTitle.Name = "KiTitle";
            this.KiTitle.ReadOnly = true;
            this.KiTitle.Size = new System.Drawing.Size(45, 30);
            this.KiTitle.TabIndex = 7;
            this.KiTitle.Text = "Ki";
            // 
            // Error1
            // 
            this.Error1.BackColor = System.Drawing.SystemColors.Window;
            this.Error1.Location = new System.Drawing.Point(929, 74);
            this.Error1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Error1.Name = "Error1";
            this.Error1.ReadOnly = true;
            this.Error1.Size = new System.Drawing.Size(331, 30);
            this.Error1.TabIndex = 7;
            this.Error1.Text = "Error 1";
            this.Error1.TextChanged += new System.EventHandler(this.Error1_TextChanged);
            // 
            // Error2
            // 
            this.Error2.Location = new System.Drawing.Point(829, 75);
            this.Error2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Error2.Name = "Error2";
            this.Error2.ReadOnly = true;
            this.Error2.Size = new System.Drawing.Size(91, 30);
            this.Error2.TabIndex = 7;
            this.Error2.Text = "Error:";
            // 
            // Integral
            // 
            this.Integral.Location = new System.Drawing.Point(1060, 276);
            this.Integral.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Integral.Name = "Integral";
            this.Integral.ReadOnly = true;
            this.Integral.Size = new System.Drawing.Size(316, 30);
            this.Integral.TabIndex = 7;
            this.Integral.Text = "Integral:";
            // 
            // LMinBox
            // 
            this.LMinBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LMinBox.Location = new System.Drawing.Point(1099, 131);
            this.LMinBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.LMinBox.Name = "LMinBox";
            this.LMinBox.ReadOnly = true;
            this.LMinBox.Size = new System.Drawing.Size(106, 30);
            this.LMinBox.TabIndex = 7;
            this.LMinBox.Text = "Lmin";
            // 
            // LMaxBox
            // 
            this.LMaxBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LMaxBox.Location = new System.Drawing.Point(1099, 193);
            this.LMaxBox.Name = "LMaxBox";
            this.LMaxBox.Size = new System.Drawing.Size(111, 30);
            this.LMaxBox.TabIndex = 0;
            this.LMaxBox.Text = "Lmax";
            // 
            // RMinBox
            // 
            this.RMinBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RMinBox.Location = new System.Drawing.Point(1252, 131);
            this.RMinBox.Name = "RMinBox";
            this.RMinBox.Size = new System.Drawing.Size(111, 30);
            this.RMinBox.TabIndex = 0;
            this.RMinBox.Text = "Rmin";
            // 
            // RMaxBox
            // 
            this.RMaxBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.RMaxBox.Location = new System.Drawing.Point(1252, 193);
            this.RMaxBox.Name = "RMaxBox";
            this.RMaxBox.Size = new System.Drawing.Size(111, 30);
            this.RMaxBox.TabIndex = 0;
            this.RMaxBox.Text = "Rmax";
            // 
            // FlipBox
            // 
            this.FlipBox.BackColor = System.Drawing.Color.Lime;
            this.FlipBox.Location = new System.Drawing.Point(67, 276);
            this.FlipBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.FlipBox.Name = "FlipBox";
            this.FlipBox.Size = new System.Drawing.Size(208, 30);
            this.FlipBox.TabIndex = 0;
            this.FlipBox.Text = "flipFlop";
            // 
            // DelayBox
            // 
            this.DelayBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.DelayBox.Location = new System.Drawing.Point(309, 276);
            this.DelayBox.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.DelayBox.Name = "DelayBox";
            this.DelayBox.Size = new System.Drawing.Size(238, 30);
            this.DelayBox.TabIndex = 0;
            this.DelayBox.TextChanged += new System.EventHandler(this.DelayBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1501, 559);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.Get2);
            this.Controls.Add(this.Get1);
            this.Controls.Add(this.Send2);
            this.Controls.Add(this.Send1);
            this.Controls.Add(this.OutputBox2);
            this.Controls.Add(this.OutputBox1);
            this.Controls.Add(this.InputBox2);
            this.Controls.Add(this.InputBox1);
            this.Controls.Add(this.toggleModeButton);
            this.Controls.Add(this.KdBox);
            this.Controls.Add(this.KdTitle);
            this.Controls.Add(this.KpBox);
            this.Controls.Add(this.KpTitle);
            this.Controls.Add(this.KiBox);
            this.Controls.Add(this.KiTitle);
            this.Controls.Add(this.Error1);
            this.Controls.Add(this.Error2);
            this.Controls.Add(this.resetIntegral);
            this.Controls.Add(this.Integral);
            this.Controls.Add(this.reCallibrate);
            this.Controls.Add(this.LMinBox);
            this.Controls.Add(this.LMaxBox);
            this.Controls.Add(this.RMinBox);
            this.Controls.Add(this.RMaxBox);
            this.Controls.Add(this.FlipBox);
            this.Controls.Add(this.DelayBox);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Form1";
            this.Text = "Design Project Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OutputBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer getIOtimer;
        private System.Windows.Forms.TextBox InputBox1;
        private System.Windows.Forms.NumericUpDown OutputBox1;
        private System.IO.Ports.SerialPort serial;
        private System.Windows.Forms.Button Send1;
        private System.Windows.Forms.Button Send2;
        private System.Windows.Forms.Button Get1;
        private System.Windows.Forms.Button Get2;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.TextBox InputBox2;
        private System.Windows.Forms.NumericUpDown OutputBox2;

    }
}
