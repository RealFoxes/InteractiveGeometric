using System.Windows.Forms;
using InteractiveGeometric.Controllers;

namespace InteractiveGeometric
{
	public partial class MainForm : Form
	{
		private ToolController toolController;
		private GraphicsController graphicsController;
		private FiguresController figuresController;

		public MainForm()
		{
			InitializeComponent();
			figuresController = new FiguresController();
			toolController = new ToolController(panelAdditionalOption, labelToolInfo, figuresController);
			graphicsController = new GraphicsController(pictureBox, figuresController, toolController);
			comboBoxFigures.SelectedIndex = 0;
			graphicsController.Draw();
		}

		private void radioButtonsCheckedChanged(object sender, EventArgs e)
		{
			if (sender is not RadioButton radioButton) return;
			if (!radioButton.Checked) return;
			foreach (var control in this.Controls)
			{
				if (control is ComboBox comboBox)
				{
					var tagEq = (ToolType)radioButton.Tag == (ToolType)comboBox.Tag;
					comboBox.Enabled = tagEq;
					if (!tagEq) comboBox.SelectedIndex = -1;
					else comboBox.SelectedIndex = 0;
				}

			}
			toolController.ToolChanging((ToolType)radioButton.Tag);
			graphicsController.Draw();
		}
		private void comboBoxesChanged(object sender, EventArgs e)
		{
			if (sender is not ComboBox comboBox) return;
			if (comboBox.SelectedIndex == -1) return;
			toolController.ToolOptionChanging(comboBox.SelectedIndex);
		}


		private void buttonClear_Click(object sender, EventArgs e)
		{
			toolController.Reset();
			figuresController.Clear();
			graphicsController.Draw();
		}

		private void buttonColor_Click(object sender, EventArgs e)
		{
			colorDialog.ShowDialog();
			toolController.SelectedColor = colorDialog.Color;
		}

		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				toolController.Complete();
				graphicsController.Draw();
				return;
			}
			var point = new Point(e.X, e.Y);
			toolController.UseTool(point);
			graphicsController.Draw();
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			var point = new Point(e.X, e.Y);
			if (toolController.MouseMove(point))
				graphicsController.Draw();
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var point = new Point(e.X, e.Y);
			if (toolController.MouseUp(point))
				graphicsController.Draw();

		}

		private void buttonInfo_Click(object sender, EventArgs e)
		{
			string info =
				"Краткая справка по всему приложению:\n" +
				"В верхней части приложения выбираются инструменты\n" +
				"Через переключатели группа, а в выпадающем списке уже конкретный инструмент\n" +
				"Вся интеракция с графикой происходит через нажатие на ЛКМ и ПКМ\n" +
				"Поддерживается зажатие мышки\n" +
				"В левом нижнем углу высвечиваються подсказки";

			MessageBox.Show(info, "Справка");
		}
	}
}
