using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGeometric.Controllers;

namespace InteractiveGeometric.Tools
{
    public abstract class Tool
	{
        public ToolMode ToolMode { get; set; }
		public abstract void Use(PointF point);
		public abstract void ChangeOption(int indexOption);
		
		public List<PointF> SelectedPoints { get; set; }
		public Rectangle SelectedBounds 
		{ 
			get 
			{
				if (SelectedPoints.Count < 2)
					return new Rectangle(0, 0, 0, 0);

				var rect = new Rectangle();
				rect.X = (int)SelectedPoints[0].X;
				rect.Y = (int)SelectedPoints[0].Y;
				rect.Width = (int)(SelectedPoints[1].X - SelectedPoints[0].X);
				rect.Height = (int)(SelectedPoints[1].Y - SelectedPoints[0].Y);
				return rect;
			} 
		}
		protected ToolController toolController;
        public Tool(ToolController toolController)
		{
			this.toolController = toolController;
			this.SelectedPoints = new List<PointF>();
		}
		public virtual void Reset()
		{
			SelectedPoints.Clear();
			ToolMode = ToolMode.None;
			toolController.figuresController.Preview = null;
		}
		public virtual void Complete() 
		{ 
			Reset();
		}
	}
	
}
