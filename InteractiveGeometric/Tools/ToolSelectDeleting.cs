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
			throw new NotImplementedException();
		}

		public override void Use(PointF point)
		{
			throw new NotImplementedException();
		}
	}
}
