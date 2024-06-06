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
		public override string[] ToolOptionInfos => new string[]
		{
			"Выберите фигуру или СТО для удаления с помощью ЛКМ",
		};
		public override void ChangeOption(int indexOption)
		{
			
		}

		public override void Use(PointF point)
		{
			var selectedFigure = toolController.figuresController.GetFigure(point);
			if (selectedFigure == null) return;
			toolController.figuresController.Figures.Remove(selectedFigure);
			var selectedSto = toolController.figuresController.GetSTO(point);
			if (selectedSto == null) return;
			toolController.figuresController.STOs.Remove(selectedSto);
			Complete();
		}
	}
}
