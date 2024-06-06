using InteractiveGeometric.Controllers;
using InteractiveGeometric.Figures;
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
	public class ToolAddFigure : Tool, IDragMovable
	{
		private NumberSelect numberSelect;
		private Figure previewFigure;
		public ToolAddFigure(ToolController toolController) : base(toolController)
		{
		}

		public FigureType ToolOption { get; set; } = FigureType.ER;
		public override string[] ToolOptionInfos => new string[]
		{ 
			"Выберите 4 точки с помощью ЛКМ",
			"Выберите от 3 точек и более с помощью ЛКМ",
			"Выберите нужное кол-во лучей и \"растяните\" фигуру с помощью ЛКМ"
		};

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
				toolController.AdditionalPanel.Visible = true;
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
					if (SelectedPoints.Count > 2)
					{
						previewFigure = toolController.figuresController.CreateFigure(ToolOption, SelectedPoints, toolController.SelectedColor);
						toolController.figuresController.Preview = previewFigure;
					}
					break;
				case FigureType.Zv:
					ToolMode = ToolMode.DragMove;
					SelectedPoints.Add(point);
					SelectedPoints.Add(point);
					previewFigure = toolController.figuresController.CreateNStar(numberSelect.trackBar.Value, SelectedBounds, toolController.SelectedColor);
					toolController.figuresController.Preview = previewFigure;
					break;
			}
		}

		public override void Complete()
		{
			if (previewFigure != null)
			{
				toolController.figuresController.Figures.Add(previewFigure.Clone());
				previewFigure = null;
				base.Complete();
				return;
			}
			switch (ToolOption)
			{
				case FigureType.None:
					break;
				case FigureType.ER:
					if (SelectedPoints.Count < 4)
					{
						base.Complete();
						return;
					}
					break;
				case FigureType.FPg:
					if (SelectedPoints.Count < 3)
					{
						base.Complete();
						return;
					}
					break;
				case FigureType.Zv:
					break;
			}

			var figure = toolController.figuresController.CreateFigure(ToolOption, new List<PointF>(SelectedPoints), toolController.SelectedColor);
				toolController.figuresController.Figures.Add(figure);
			
			base.Complete();
		}

		public void Move(Point point)
		{
			SelectedPoints[1] = point;
			if (previewFigure is NStar nStar)
				nStar.Bounds = SelectedBounds;

		}

		public void End(Point point)
		{
			SelectedPoints[1] = point;
			if (previewFigure is NStar nStar)
			{
				nStar.Bounds = SelectedBounds;
				previewFigure.Calculate();
			}
			Complete();
		}
	}
}
