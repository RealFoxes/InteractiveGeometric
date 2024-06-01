using InteractiveGeometric.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveGeometric.Tools
{
	public class ToolAddFigure : Tool, IDragSizeble
	{
		private NumberSelect numberSelect;
		private Figure creatingFigure;
		public ToolAddFigure(ToolController toolController) : base(toolController)
		{
		}

		public FigureType ToolOption { get; set; } = FigureType.ER;

		public override void ChangeOption(int indexOption)
		{
			ToolOption = (FigureType)indexOption;
			if (ToolOption == FigureType.Zv)
			{
				numberSelect = new NumberSelect("Выберите кол-во лучей");
				numberSelect.trackBar.Minimum = 3;
				numberSelect.trackBar.Value = 3;
				numberSelect.trackBar.Maximum = 20;
				numberSelect.RefreshValueLabel();
				numberSelect.Dock = DockStyle.Fill;
				toolController.AdditionalPanel.Controls.Add(numberSelect);
			}
		}

		public override void Use(PointF point)
		{
			switch (ToolOption)
			{
				case FigureType.None:
					return;
				case FigureType.ER:
					ToolMode = ToolMode.SelectPoint;
					SelectedPoints.Add(point);
					if (SelectedPoints.Count == 4) Complete();
					break;
				case FigureType.FPg:
					ToolMode = ToolMode.SelectPoint;
					SelectedPoints.Add(point);
					break;
				case FigureType.Zv:
					ToolMode = ToolMode.DragSize;
					SelectedPoints.Add(point);
					SelectedPoints.Add(point);
					creatingFigure = toolController.figuresController.CreateNStar(numberSelect.trackBar.Value, SelectedPoints, toolController.SelectedColor);
					break;
			}
		}

		public override void Complete()
		{
			if (ToolOption == FigureType.Zv)
			{
				base.Complete();
				return;
			}

			toolController.figuresController.CreateFigure(ToolOption, new List<PointF>(SelectedPoints), toolController.SelectedColor);
			base.Complete();
		}

		public void Move(Point point)
		{
			SelectedPoints[1] = point;
		}

		public void End(Point point)
		{
			SelectedPoints[1] = point;
			creatingFigure.Points = new List<PointF>(SelectedPoints);
			Complete();
		}
	}
}
