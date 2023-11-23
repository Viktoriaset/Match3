namespace ThreeInRow
{
    partial class Level1
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.points_label = new System.Windows.Forms.Label();
            this.game_timer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Update);
            // 
            // points_label
            // 
            this.points_label.AutoSize = true;
            this.points_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.points_label.Location = new System.Drawing.Point(12, 9);
            this.points_label.Name = "points_label";
            this.points_label.Size = new System.Drawing.Size(111, 39);
            this.points_label.TabIndex = 0;
            this.points_label.Text = "Очки:";
            // 
            // game_timer
            // 
            this.game_timer.AutoSize = true;
            this.game_timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.game_timer.Location = new System.Drawing.Point(340, 9);
            this.game_timer.Name = "game_timer";
            this.game_timer.Size = new System.Drawing.Size(109, 39);
            this.game_timer.TabIndex = 1;
            this.game_timer.Text = "label1";
            // 
            // Level1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.game_timer);
            this.Controls.Add(this.points_label);
            this.DoubleBuffered = true;
            this.Name = "Level1";
            this.Text = "Level1";
            this.Load += new System.EventHandler(this.Level1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SelectElement);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label points_label;
        private System.Windows.Forms.Label game_timer;
    }
}