using System.Windows.Forms;

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
			toolController = new ToolController(panelAdditionalOption, figuresController);
			graphicsController = new GraphicsController(pictureBox, figuresController, toolController);
			comboBoxFigures.SelectedIndex = 0;
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
		}
		private void comboBoxesChanged(object sender, EventArgs e)
		{
			if (sender is not ComboBox comboBox) return;
			if (comboBox.SelectedIndex == -1) return;
			toolController.ToolOptionChanging(comboBox.SelectedIndex);
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
			toolController.MouseMove(e);
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			//toolController.Reset();
			figuresController.Figures.Clear();
			graphicsController.Draw();
		}

		private void buttonColor_Click(object sender, EventArgs e)
		{
			colorDialog.ShowDialog();
			toolController.SelectedColor = colorDialog.Color;
		}
	}
}
