using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGeometric.Figures;

namespace InteractiveGeometric.Controllers
{
    public class FiguresController
    {
        public List<Figure> Figures { get; set; }
        public List<SetTheoreticOperations> STOs { get; private set; }

        private Figure preview;
        public Figure Preview { get; set; }
        public FiguresController()
        {
            Figures = new List<Figure>();
			STOs = new List<SetTheoreticOperations>();
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
        public SetTheoreticOperations GetSTO(PointF point)
		{
			foreach (var sto in STOs)
			{
				if(sto.Figure1.PointInPolygon(point) || sto.Figure2.PointInPolygon(point))
					return sto;
			}
			return null;
		}

        public void Union(Figure figure1, Figure figure2)
		{
			AddSTO(figure1, figure2, OperationType.Union);
		}

		public void Intersection(Figure figure1, Figure figure2)
		{
			AddSTO(figure1, figure2, OperationType.Intersection);
		}
		private void AddSTO(Figure figure1, Figure figure2, OperationType operation)
		{
			figure1.Points.Add(figure1.Points[0]);
			figure2.Points.Add(figure2.Points[0]);

			var sto = new SetTheoreticOperations();
			sto.Operation = operation;
			sto.Figure1 = figure1;
			sto.Figure2 = figure2;
			Figures.Remove(figure1);
			Figures.Remove(figure2);
			STOs.Add(sto);
		}

		public void Clear()
		{
			Figures.Clear();
			STOs.Clear();
		}
	}
}
