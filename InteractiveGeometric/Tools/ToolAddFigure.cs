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
	public class ToolAddFigure : Tool
	{
		public ToolAddFigure(ToolController toolController) : base(toolController)
		{
		}

		public FigureType ToolOption { get; set; } = FigureType.ER;

		public override void ChangeOption(int indexOption)
		{
			ToolOption = (FigureType)indexOption;
			if (ToolOption == FigureType.Zv)
			{
				var numberSelect = new NumberSelect("Выберите кол-во лучей");
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
					SelectedPoints.Add(point);
					if (SelectedPoints.Count == 4) Complete();
					break;
				case FigureType.FPg:
					SelectedPoints.Add(point);
					break;
				case FigureType.Zv:
					break;
			}
		}

		public override void Complete()
		{
			toolController.figuresController.CreateFigure(ToolOption, new List<PointF>(SelectedPoints), toolController.SelectedColor);
			base.Complete();
		}
	}
}
