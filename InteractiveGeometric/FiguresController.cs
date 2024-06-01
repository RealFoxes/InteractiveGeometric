using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGeometric
{
    public class FiguresController
    {
        public List<Figure> Figures { get; set; }
        public FiguresController()
        {
            Figures = new List<Figure>();
        }
        public Figure CreateFigure(FigureType type, List<PointF> points, Color selectedColor)
        {
            var figure = new Figure();
            figure.FigureType = type;
            figure.Points = points;
            figure.Color = selectedColor;
            Figures.Add(figure);
            return figure;
		}
		public NStar CreateNStar(int numRays, List<PointF> points, Color selectedColor)
		{
			var figure = new NStar();
			figure.FigureType = FigureType.Zv;
            figure.NumRays = numRays;
			figure.Points = points;
			figure.Color = selectedColor;
			Figures.Add(figure);
            return figure;
		}
	}
}
