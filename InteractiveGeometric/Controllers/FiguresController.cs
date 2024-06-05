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
			var unionPoints = new List<PointF>();

			// Простая реализация объединения: просто соединить все точки
			unionPoints.AddRange(figure1.Points);
			unionPoints.AddRange(figure2.Points);

			// Удалить дубликаты точек
			unionPoints = unionPoints.Distinct().ToList();

			var unionFigure = new Figure
			{
				FigureType = FigureType.FPg,
				Points = unionPoints,
				Color = figure1.Color // Цвет объединенной фигуры
			};

			Figures.Remove(figure1);
			Figures.Remove(figure2);
			Figures.Add(unionFigure);

		}
		public void Intersection(Figure figure1, Figure figure2)
		{
			var intersectionPoints = new List<PointF>();

			// Простая реализация пересечения: взять точки, которые находятся внутри обоих многоугольников
			foreach (var point in figure1.Points)
			{
				if (figure2.PointInPolygon(point))
				{
					intersectionPoints.Add(point);
				}
			}

			foreach (var point in figure2.Points)
			{
				if (figure1.PointInPolygon(point))
				{
					intersectionPoints.Add(point);
				}
			}

			intersectionPoints = intersectionPoints.Distinct().ToList();

			var intersectionFigure = new Figure
			{
				FigureType = FigureType.FPg,
				Points = intersectionPoints,
				Color = figure1.Color
			};

			Figures.Remove(figure1);
			Figures.Remove(figure2);
			Figures.Add(intersectionFigure);
		}
	}
}
