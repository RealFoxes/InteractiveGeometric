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
			selectedFigures.Add(figure);
			if (selectedFigures.Count == 2)
			{
				if (selectedFigures[0].Equals(selectedFigures[1]))
				{
					Complete();
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
