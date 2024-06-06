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
    public class ToolOperation : Tool
	{
		private List<Figure> selectedFigures;
		public ToolOperation(ToolController toolController) : base(toolController)
		{
			selectedFigures = new List<Figure>();
		}

		public OperationType ToolOption { get; set; } = OperationType.Union;
		public override string[] ToolOptionInfos => new string[]
		{
			"Выберите 2 фигуры с помощью ЛКМ",
			"Выберите 2 фигуры с помощью ЛКМ"
		};

		public override void ChangeOption(int indexOption)
		{
			ToolOption = (OperationType)indexOption;
		}

		public override void Use(PointF point)
		{
			switch (ToolOption)
			{
				case OperationType.None:
					break;
				case OperationType.Union:
					ToolMode = ToolMode.SelectFigure;
					if (Selection(point))
					{
						toolController.figuresController.Union(selectedFigures[0], selectedFigures[1]);
						Complete();
					}
					break;
				case OperationType.Intersection:
					ToolMode = ToolMode.SelectFigure;
					if (Selection(point))
					{
						toolController.figuresController.Intersection(selectedFigures[0], selectedFigures[1]);
						Complete();
					}
					break;
			}
		}
		private bool Selection(PointF point)
		{
			var figure = toolController.figuresController.GetFigure(point);
			if(figure == null)
				return false;
			if (figure.FigureType == FigureType.ER)
			{
				toolController.PrintToolError("Не может быть выбран кубический сплайн");
				return false;
			}
			figure.IsSelected = true;
			selectedFigures.Add(figure);
			SelectedPoints.Add(point);
			if (selectedFigures.Count == 2)
			{
				if (selectedFigures[0].Equals(selectedFigures[1]))
				{
					Complete();
					toolController.PrintToolError("Выбрана одна и та же фигура");
					return false;
				}
				return true;
			}

			return false;
		}
		public override void Complete()
		{
			selectedFigures.Clear();
			base.Complete();
		}
	}
}
