using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGeometric.Controllers;

namespace InteractiveGeometric.Tools
{
    public class ToolSelectDeleting : Tool
	{
		public ToolSelectDeleting(ToolController toolController) : base(toolController)
		{
		}

		public override void ChangeOption(int indexOption)
		{
			
		}

		public override void Use(PointF point)
		{
			var selectedFigure = toolController.figuresController.GetFigure(point);
			if (selectedFigure == null) return;
			toolController.figuresController.Figures.Remove(selectedFigure);
			Complete();
		}
	}
}
