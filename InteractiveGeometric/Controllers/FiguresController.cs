using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGeometric.Figures;

namespace InteractiveGeometric.Controllers
{
    public class FiguresController
    {
        public List<Figure> Figures { get; set; }

        private Figure preview;
        public Figure Preview { get; set; }
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
            return figure;
        }
        public NStar CreateNStar(int numRays, Rectangle bounds, Color selectedColor)
        {
            var figure = new NStar();
            figure.FigureType = FigureType.Zv;
            figure.NumRays = numRays;
            figure.Color = selectedColor;
            figure.Bounds = bounds;
            return figure;
        }

		public Figure GetFigure(PointF point)
		{
			foreach (var figure in Figures)
			{
				if (figure.PointInPolygon(point))
				{
					return figure;
				}
			}
			return null;
		}

		public void Union(Figure figure1, Figure figure2)
		{
			

		}
		public void Intersection(Figure figure1, Figure figure2)
		{
			
		}
	}
}
