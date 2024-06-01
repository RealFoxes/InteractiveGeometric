using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InteractiveGeometric.Tools
{
	public class ToolOperation : Tool
	{
		public ToolOperation(ToolController toolController) : base(toolController)
		{
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
					break;
				case OperationType.Intersection:
					break;
			}
		}

		public override void Complete()
		{

			base.Complete();
		}
	}
}
