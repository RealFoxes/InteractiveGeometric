using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGeometric.Tools
{
	public interface IDragSizeble
	{
		void Move(Point point);
		void End(Point point);
	}
}
