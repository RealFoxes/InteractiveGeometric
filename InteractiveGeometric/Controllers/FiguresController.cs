using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGeometric.Figures;
using Polybool.Net.Logic;
using Polybool.Net.Objects;
using PbPoint = Polybool.Net.Objects.Point;
using Region = Polybool.Net.Objects.Region;

namespace InteractiveGeometric.Controllers
{
    public class FiguresController
    {
        public List<Figure> Figures { get; set; }
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
			var polygons = ConvertToPolyBool(new List<Figure> { figure1, figure2 });

			var unified = SegmentSelector.Union(polygons[0], polygons[1]);
			var list = ConvertToFigures(unified, figure1.Color);
			Figures.AddRange(list);

			Figures.Remove(figure1);
			Figures.Remove(figure2);

		}
		public void Intersection(Figure figure1, Figure figure2)
		{
			var polygons = ConvertToPolyBool(new List<Figure> { figure1, figure2 });

			var unified = SegmentSelector.Intersect(polygons[1], polygons[0]);

			var list = ConvertToFigures(unified, figure1.Color);
			Figures.AddRange(list);
			Figures.Remove(figure1);
			Figures.Remove(figure2);

		}
		private List<Polygon> ConvertToPolyBool(List<Figure> figures)
		{
			var list = new List<Polygon>();
			foreach (var figure in figures)
			{
				var polygon = new Polygon();
				var region = new Region();
				polygon.Regions.Add(region);
				region.Points = new List<PbPoint>();
				foreach (var point in figure.Points)
					region.Points.Add(new PbPoint((decimal)point.X, (decimal)point.Y));
				list.Add(polygon);
			}
			return list;
		}
		private List<Figure> ConvertToFigures(Polygon polygon, Color color)
		{
			var list = new List<Figure>();
			foreach (var region in polygon.Regions)
			{
				var points = new List<PointF>();
				foreach (var point in region.Points)
					points.Add(new PointF((float)point.X, (float)point.Y));
					//возможно нужна логика связки 

				var figure = CreateFigure(FigureType.FPg, points, color);
				list.Add(figure);
			}
			return list;
		}

		public void Clear()
		{
			Figures.Clear();
		}
	}
}
