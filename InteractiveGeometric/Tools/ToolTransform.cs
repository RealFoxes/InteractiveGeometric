using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteractiveGeometric.Controllers;
using InteractiveGeometric.Figures;

namespace InteractiveGeometric.Tools
{
    public class ToolTransform : Tool, IDragMovable
	{
		private Figure previewFigure;
		private Figure selectedFigure;
		private List<PointF> savedPoints;
		public ToolTransform(ToolController toolController) : base(toolController)
		{
		}

		public TransformType ToolOption { get; set; } = TransformType.Rf;

		public override void ChangeOption(int indexOption)
		{
			ToolOption = (TransformType)indexOption;
		}

		public override void Use(PointF point)
		{
			switch (ToolOption)
			{
				case TransformType.None:
					return;
				case TransformType.Move:
					StartDragTransform(point);
					break;
				case TransformType.Rf:
					if (StartDragTransform(point))
						savedPoints = new List<PointF>(selectedFigure.Points);
					break;
				case TransformType.Syc:
					if (StartDragTransform(point))
						savedPoints = new List<PointF>(selectedFigure.Points);
					break;
				case TransformType.Mc:
					ToolMode = ToolMode.SelectFigure;
					selectedFigure = toolController.figuresController.GetFigure(point);
					if (selectedFigure == null) return;
					selectedFigure.Mirror();
					Complete();
					break;
			}
		}

		private bool StartDragTransform(PointF point)
		{
			selectedFigure = toolController.figuresController.GetFigure(point);
			if (selectedFigure == null) return false;
			selectedFigure.IsSelected = true;
			point = selectedFigure.GetCenter();
			ToolMode = ToolMode.DragMove;
			SelectedPoints.Add(point);
			SelectedPoints.Add(point);
			previewFigure = selectedFigure.Clone();
			toolController.figuresController.Preview = previewFigure;
			return true;
		}

		public override void Complete()
		{
			if(selectedFigure != null)
				selectedFigure.IsSelected = false;
			previewFigure = null;
			selectedFigure = null;
			base.Complete();
		}

		public void Move(Point point)
		{
			SelectedPoints[1] = point;
			DoTransform(previewFigure);
		}

		public void End(Point point)
		{
			SelectedPoints[1] = point;
			DoTransform(selectedFigure);
			Complete();
		}

		private void DoTransform(Figure figure)
		{
			switch (ToolOption)
			{
				case TransformType.None:
					return;
				case TransformType.Move:
					figure.Move(SelectedPoints[1]);
					break;
				case TransformType.Rf:
					figure.Points = new List<PointF>(savedPoints);
					figure.RotateByPoint(SelectedPoints[1]);
					break;
				case TransformType.Syc:
					figure.Points = new List<PointF>(savedPoints);
					figure.ScaleY(SelectedPoints[1]);
					break;
				case TransformType.Mc:
					break;
			}
		}
	}
}
