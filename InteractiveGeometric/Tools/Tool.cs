using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGeometric.Tools
{
	public abstract class Tool
	{
        public ToolMode ToolMode { get; set; }
		public abstract void Use(PointF point);
		public abstract void ChangeOption(int indexOption);
		
		public List<PointF> SelectedPoints { get; set; }
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
		}
		public virtual void Complete() 
		{ 
			Reset();
		}
	}
	
}
