namespace InteractiveGeometric
{
	public partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Label label1;
			Label label2;
			Label label3;
			pictureBox = new PictureBox();
			comboBoxTransforms = new ComboBox();
			comboBoxOperations = new ComboBox();
			buttonColor = new Button();
			buttonClear = new Button();
			buttonInfo = new Button();
			radioButtonTransform = new RadioButton();
			radioButtonFigure = new RadioButton();
			radioButtonOperation = new RadioButton();
			radioButtonSelectDeleting = new RadioButton();
			comboBoxFigures = new ComboBox();
			colorDialog = new ColorDialog();
			panelAdditionalOption = new Panel();
			label1 = new Label();
			label2 = new Label();
			label3 = new Label();
			((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(31, 7);
			label1.Name = "label1";
			label1.Size = new Size(73, 15);
			label1.TabIndex = 11;
			label1.Text = "Примитивы";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(192, 7);
			label2.Name = "label2";
			label2.Size = new Size(192, 15);
			label2.TabIndex = 12;
			label2.Text = "Геометрические преобразования";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(588, 7);
			label3.Name = "label3";
			label3.Size = new Size(74, 15);
			label3.TabIndex = 13;
			label3.Text = "Выбор ТМО";
			// 
			// pictureBox
			// 
			pictureBox.BackColor = Color.White;
			pictureBox.Dock = DockStyle.Bottom;
			pictureBox.Location = new Point(0, 70);
			pictureBox.Margin = new Padding(3, 2, 3, 2);
			pictureBox.Name = "pictureBox";
			pictureBox.Size = new Size(772, 345);
			pictureBox.TabIndex = 0;
			pictureBox.TabStop = false;
			pictureBox.MouseDown += pictureBox_MouseDown;
			pictureBox.MouseMove += pictureBox_MouseMove;
			// 
			// comboBoxTransforms
			// 
			comboBoxTransforms.Enabled = false;
			comboBoxTransforms.FormattingEnabled = true;
			comboBoxTransforms.Items.AddRange(new object[] { "Перемещение", "Поворот вокруг центра фигуры на произвольный угол | Rf", "Масштабирование по оси Y относительно заданного центра | Syc", "Зеркальное отражение относительно заданного центра | Mc" });
			comboBoxTransforms.Location = new Point(192, 20);
			comboBoxTransforms.Margin = new Padding(3, 2, 3, 2);
			comboBoxTransforms.Name = "comboBoxTransforms";
			comboBoxTransforms.Size = new Size(367, 23);
			comboBoxTransforms.TabIndex = 4;
			comboBoxTransforms.Tag = ToolType.Transform;
			comboBoxTransforms.SelectedIndexChanged += comboBoxesChanged;
			// 
			// comboBoxOperations
			// 
			comboBoxOperations.Enabled = false;
			comboBoxOperations.FormattingEnabled = true;
			comboBoxOperations.Items.AddRange(new object[] { "Объединение", "Пересечение" });
			comboBoxOperations.Location = new Point(588, 20);
			comboBoxOperations.Margin = new Padding(3, 2, 3, 2);
			comboBoxOperations.Name = "comboBoxOperations";
			comboBoxOperations.Size = new Size(133, 23);
			comboBoxOperations.TabIndex = 6;
			comboBoxOperations.Tag = ToolType.Operation;
			comboBoxOperations.SelectedIndexChanged += comboBoxesChanged;
			// 
			// buttonColor
			// 
			buttonColor.Location = new Point(10, 44);
			buttonColor.Margin = new Padding(3, 2, 3, 2);
			buttonColor.Name = "buttonColor";
			buttonColor.Size = new Size(82, 22);
			buttonColor.TabIndex = 7;
			buttonColor.Text = "Цвет";
			buttonColor.UseVisualStyleBackColor = true;
			buttonColor.Click += buttonColor_Click;
			// 
			// buttonClear
			// 
			buttonClear.Location = new Point(98, 44);
			buttonClear.Margin = new Padding(3, 2, 3, 2);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new Size(82, 22);
			buttonClear.TabIndex = 8;
			buttonClear.Text = "Очистить";
			buttonClear.UseVisualStyleBackColor = true;
			buttonClear.Click += buttonClear_Click;
			// 
			// buttonInfo
			// 
			buttonInfo.Location = new Point(186, 44);
			buttonInfo.Margin = new Padding(3, 2, 3, 2);
			buttonInfo.Name = "buttonInfo";
			buttonInfo.Size = new Size(82, 22);
			buttonInfo.TabIndex = 9;
			buttonInfo.Text = "Справка";
			buttonInfo.UseVisualStyleBackColor = true;
			// 
			// radioButtonTransform
			// 
			radioButtonTransform.AutoSize = true;
			radioButtonTransform.Location = new Point(172, 24);
			radioButtonTransform.Margin = new Padding(3, 2, 3, 2);
			radioButtonTransform.Name = "radioButtonTransform";
			radioButtonTransform.Size = new Size(14, 13);
			radioButtonTransform.TabIndex = 3;
			radioButtonTransform.Tag = ToolType.Transform;
			radioButtonTransform.UseVisualStyleBackColor = true;
			radioButtonTransform.CheckedChanged += radioButtonsCheckedChanged;
			// 
			// radioButtonFigure
			// 
			radioButtonFigure.AutoSize = true;
			radioButtonFigure.Checked = true;
			radioButtonFigure.Location = new Point(10, 24);
			radioButtonFigure.Margin = new Padding(3, 2, 3, 2);
			radioButtonFigure.Name = "radioButtonFigure";
			radioButtonFigure.Size = new Size(14, 13);
			radioButtonFigure.TabIndex = 1;
			radioButtonFigure.TabStop = true;
			radioButtonFigure.Tag = ToolType.AddFigure;
			radioButtonFigure.UseVisualStyleBackColor = true;
			radioButtonFigure.CheckedChanged += radioButtonsCheckedChanged;
			// 
			// radioButtonOperation
			// 
			radioButtonOperation.AutoSize = true;
			radioButtonOperation.Location = new Point(568, 24);
			radioButtonOperation.Margin = new Padding(3, 2, 3, 2);
			radioButtonOperation.Name = "radioButtonOperation";
			radioButtonOperation.Size = new Size(14, 13);
			radioButtonOperation.TabIndex = 5;
			radioButtonOperation.Tag = ToolType.Operation;
			radioButtonOperation.UseVisualStyleBackColor = true;
			radioButtonOperation.CheckedChanged += radioButtonsCheckedChanged;
			// 
			// radioButtonSelectDeleting
			// 
			radioButtonSelectDeleting.AutoSize = true;
			radioButtonSelectDeleting.Location = new Point(284, 46);
			radioButtonSelectDeleting.Margin = new Padding(3, 2, 3, 2);
			radioButtonSelectDeleting.Name = "radioButtonSelectDeleting";
			radioButtonSelectDeleting.Size = new Size(150, 19);
			radioButtonSelectDeleting.TabIndex = 10;
			radioButtonSelectDeleting.Tag = ToolType.SelectDeleting;
			radioButtonSelectDeleting.Text = "Выборочное удаление";
			radioButtonSelectDeleting.UseVisualStyleBackColor = true;
			radioButtonSelectDeleting.CheckedChanged += radioButtonsCheckedChanged;
			// 
			// comboBoxFigures
			// 
			comboBoxFigures.FormattingEnabled = true;
			comboBoxFigures.Items.AddRange(new object[] { "Кубический сплайн | ER", "Произвольный n-угольник | FPg", "Правильная n-конечная звезда | Zv" });
			comboBoxFigures.Location = new Point(31, 20);
			comboBoxFigures.Margin = new Padding(3, 2, 3, 2);
			comboBoxFigures.Name = "comboBoxFigures";
			comboBoxFigures.Size = new Size(133, 23);
			comboBoxFigures.TabIndex = 2;
			comboBoxFigures.Tag = ToolType.AddFigure;
			comboBoxFigures.SelectedIndexChanged += comboBoxesChanged;
			// 
			// panelAdditionalOption
			// 
			panelAdditionalOption.BackColor = Color.Transparent;
			panelAdditionalOption.Location = new Point(572, 70);
			panelAdditionalOption.Name = "panelAdditionalOption";
			panelAdditionalOption.Size = new Size(200, 100);
			panelAdditionalOption.TabIndex = 14;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(772, 415);
			Controls.Add(panelAdditionalOption);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(label1);
			Controls.Add(radioButtonSelectDeleting);
			Controls.Add(radioButtonOperation);
			Controls.Add(radioButtonFigure);
			Controls.Add(radioButtonTransform);
			Controls.Add(buttonInfo);
			Controls.Add(buttonClear);
			Controls.Add(buttonColor);
			Controls.Add(comboBoxOperations);
			Controls.Add(comboBoxTransforms);
			Controls.Add(comboBoxFigures);
			Controls.Add(pictureBox);
			Margin = new Padding(3, 2, 3, 2);
			Name = "MainForm";
			Text = "InteractiveGeometric";
			((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private PictureBox pictureBox;
		private Button buttonColor;
		private Button buttonClear;
		private Button buttonInfo;
		private RadioButton radioButtonTransform;
		private RadioButton radioButtonFigure;
		private RadioButton radioButtonOperation;
		private RadioButton radioButtonSelectDeleting;
		public ComboBox comboBoxTransforms;
		public ComboBox comboBoxOperations;
		public ComboBox comboBoxFigures;
		private ColorDialog colorDialog;
		public Panel panelAdditionalOption;
	}
}
