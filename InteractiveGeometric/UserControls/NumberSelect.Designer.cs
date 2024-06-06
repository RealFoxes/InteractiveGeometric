namespace InteractiveGeometric.UserControls
{
	partial class NumberSelect
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			trackBar = new TrackBar();
			labelName = new Label();
			labelValue = new Label();
			((System.ComponentModel.ISupportInitialize)trackBar).BeginInit();
			SuspendLayout();
			// 
			// trackBar
			// 
			trackBar.Location = new Point(3, 25);
			trackBar.Name = "trackBar";
			trackBar.Size = new Size(394, 45);
			trackBar.TabIndex = 0;
			trackBar.ValueChanged += trackBar_ValueChanged;
			// 
			// labelName
			// 
			labelName.AutoSize = true;
			labelName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelName.Location = new Point(3, 0);
			labelName.Name = "labelName";
			labelName.Size = new Size(43, 21);
			labelName.TabIndex = 1;
			labelName.Text = "label";
			// 
			// labelValue
			// 
			labelValue.AutoSize = true;
			labelValue.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
			labelValue.Location = new Point(181, 66);
			labelValue.Name = "labelValue";
			labelValue.Size = new Size(43, 21);
			labelValue.TabIndex = 2;
			labelValue.Text = "label";
			// 
			// NumberSelect
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(labelValue);
			Controls.Add(labelName);
			Controls.Add(trackBar);
			Name = "NumberSelect";
			Size = new Size(400, 100);
			((System.ComponentModel.ISupportInitialize)trackBar).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		public TrackBar trackBar;
		private Label labelName;
		private Label labelValue;
	}
}
