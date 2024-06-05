using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteractiveGeometric.Controllers;

namespace InteractiveGeometric.Tools
{
    public class ToolTransform : Tool
	{
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

					break;
				case TransformType.Rf:
					break;
				case TransformType.Syc:
					break;
				case TransformType.Mc:
					break;
			}
		}

		public override void Complete()
		{

			base.Complete();
		}
	}
}
