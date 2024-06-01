using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGeometric
{
    public class GraphicsController
    {
        private PictureBox pictureBox;
        private readonly FiguresController figuresController;
        private readonly ToolController toolController;
        private Bitmap bitmap;
        private Graphics g;

        public GraphicsController(PictureBox pictureBox, FiguresController figuresController, ToolController toolController)
        {
            this.pictureBox = pictureBox;
            this.figuresController = figuresController;
            this.toolController = toolController;
            this.bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            this.g = Graphics.FromImage(bitmap);
        }

        public void Draw()
        {
            g.Clear(Color.White);

            DrawFigures();
            DrawToolSelectedPoints();

            pictureBox.Image = this.bitmap;
            pictureBox.Refresh();
        }

		private void DrawFigures()
		{
            foreach (var figure in figuresController.Figures)
            {
                switch (figure.FigureType)
                {
                    case FigureType.None:
                        break;
                    case FigureType.ER:
                        DrawCubicSpline(figure.Points, figure.Color);
						break;
                    case FigureType.FPg:
                        g.FillPolygon(new SolidBrush(figure.Color), figure.Points.ToArray());
                        break;
                    case FigureType.Zv:
						if(figure is NStar nStar)
							DrawNStar(nStar.NumRays, nStar.Points, nStar.Color);
                        break;
                }
            }
		}

		private void DrawCubicSpline(List<PointF> points, Color color)
		{
            if(points.Count < 4) return;

            var pen = new Pen(color);
			PointF[] Ls = new PointF[4];
			PointF Pv1 = points[0];
			PointF Pv2 = points[0];

			const double dt = 0.004;
			double t = 0;
			double term = 1 + dt / 2;
			double xt, yt;

			PointF Ppred = points[0], Pt = points[0];
			Pv1.X = 4 * (points[1].X - points[0].X);
			Pv1.Y = 4 * (points[1].Y - points[0].Y);
			Pv2.X = 4 * (points[3].X - points[2].X);
			Pv2.Y = 4 * (points[3].Y - points[2].Y);

			Ls[0].X = 2 * points[0].X - 2 * points[3].X + Pv1.X + Pv2.X; // Ax
			Ls[0].Y = 2 * points[0].Y - 2 * points[3].Y + Pv1.Y + Pv2.Y; // Ay
			Ls[1].X = -3 * points[0].X + 3 * points[3].X - 2 * Pv1.X - Pv2.X; // Bx
			Ls[1].Y = -3 * points[0].Y + 3 * points[3].Y - 2 * Pv1.Y - Pv2.Y; // By
			Ls[2].X = Pv1.X; // Cx
			Ls[2].Y = Pv1.Y; // Cy
			Ls[3].X = points[0].X; // Dx
			Ls[3].Y = points[0].Y; // Dy

			while (t < term)
			{
				xt = ((Ls[0].X * t + Ls[1].X) * t + Ls[2].X) * t + Ls[3].X;
				yt = ((Ls[0].Y * t + Ls[1].Y) * t + Ls[2].Y) * t + Ls[3].Y;
				Pt.X = (int)Math.Round(xt);
				Pt.Y = (int)Math.Round(yt);
				g.DrawLine(pen, Ppred, Pt);
				Ppred = Pt;
				t += dt;
			}
		}

		private void DrawToolSelectedPoints()
		{
			if (toolController.GetSelectedPoints() == null) return;

			var s = 5;
			foreach (var point in toolController.GetSelectedPoints())
				g.DrawEllipse(new Pen(Color.Red, 1), point.X - s, point.Y - s, s, s);
		}
		private void DrawNStar(int numPoints, List<PointF> points, Color color)
		{
			var rect = new Rectangle();
			rect.X = (int)points[0].X;
			rect.Y = (int)points[0].Y;
			rect.Width = (int)(points[1].X - points[0].X);
			rect.Height = (int)(points[1].Y - points[0].Y);
			var starPoints = GetStarPoints(numPoints, rect);
			g.FillPolygon(new SolidBrush(color), starPoints);
		}

		private PointF[] GetStarPoints(int numPoints, Rectangle bounds)
		{
			List<PointF> starPoints = new List<PointF>();

			float centerX = bounds.X + bounds.Width / 2f;
			float centerY = bounds.Y + bounds.Height / 2f;
			float radiusOuter = Math.Min(bounds.Width, bounds.Height) / 2f;
			float radiusInner = radiusOuter / 2.5f; // Adjust as needed for the inner radius

			double angleStep = Math.PI / numPoints;

			for (int i = 0; i < 2 * numPoints; i++)
			{
				double angle = i * angleStep - Math.PI / 2; // Adjust angle to start from the top
				float radius = (i % 2 == 0) ? radiusOuter : radiusInner;
				float x = centerX + (float)(radius * Math.Cos(angle));
				float y = centerY + (float)(radius * Math.Sin(angle));
				starPoints.Add(new PointF(x, y));
			}

			return starPoints.ToArray();
		}

		//Math below

		private PointF[] MakeStarPoints(double startTheta, int numPoints, int skip, Rectangle rect)
		{
			double theta, dtheta;
			PointF[] result;
			float rx = rect.Width / 2f;
			float ry = rect.Height / 2f;
			float cx = rect.X + rx;
			float cy = rect.Y + ry;

			// If this is a polygon, don't bother with concave points.
			if (skip == 1)
			{
				result = new PointF[numPoints];
				theta = startTheta;
				dtheta = 2 * Math.PI / numPoints;
				for (int i = 0; i < numPoints; i++)
				{
					result[i] = new PointF(
						(float)(cx + rx * Math.Cos(theta)),
						(float)(cy + ry * Math.Sin(theta)));
					theta += dtheta;
				}
				return result;
			}

			// Find the radius for the concave vertices.
			double concave_radius =
				CalculateConcaveRadius(numPoints, skip);

			// Make the points.
			result = new PointF[2 * numPoints];
			theta = startTheta;
			dtheta = Math.PI / numPoints;
			for (int i = 0; i < numPoints; i++)
			{
				result[2 * i] = new PointF(
					(float)(cx + rx * Math.Cos(theta)),
					(float)(cy + ry * Math.Sin(theta)));
				theta += dtheta;
				result[2 * i + 1] = new PointF(
					(float)(cx + rx * Math.Cos(theta) * concave_radius),
					(float)(cy + ry * Math.Sin(theta) * concave_radius));
				theta += dtheta;
			}
			return result;
		}
		private double CalculateConcaveRadius(int num_points, int skip)
		{
			// For really small numbers of points.
			if (num_points < 5) return 0.33f;

			// Calculate angles to key points.
			double dtheta = 2 * Math.PI / num_points;
			double theta00 = -Math.PI / 2;
			double theta01 = theta00 + dtheta * skip;
			double theta10 = theta00 + dtheta;
			double theta11 = theta10 - dtheta * skip;

			// Find the key points.
			PointF pt00 = new PointF(
				(float)Math.Cos(theta00),
				(float)Math.Sin(theta00));
			PointF pt01 = new PointF(
				(float)Math.Cos(theta01),
				(float)Math.Sin(theta01));
			PointF pt10 = new PointF(
				(float)Math.Cos(theta10),
				(float)Math.Sin(theta10));
			PointF pt11 = new PointF(
				(float)Math.Cos(theta11),
				(float)Math.Sin(theta11));

			// See where the segments connecting the points intersect.
			bool lines_intersect, segments_intersect;
			PointF intersection, close_p1, close_p2;
			FindIntersection(pt00, pt01, pt10, pt11,
				out lines_intersect, out segments_intersect,
				out intersection, out close_p1, out close_p2);

			// Calculate the distance between the
			// point of intersection and the center.
			return Math.Sqrt(
				intersection.X * intersection.X +
				intersection.Y * intersection.Y);
		}
		private void FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4, 
			out bool lines_intersect, out bool segments_intersect, out PointF intersection, out PointF close_p1, out PointF close_p2)
		{
			// Get the segments' parameters.
			float dx12 = p2.X - p1.X;
			float dy12 = p2.Y - p1.Y;
			float dx34 = p4.X - p3.X;
			float dy34 = p4.Y - p3.Y;

			// Solve for t1 and t2
			float denominator = (dy12 * dx34 - dx12 * dy34);

			float t1 =
				((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
					/ denominator;
			if (float.IsInfinity(t1))
			{
				// The lines are parallel (or close enough to it).
				lines_intersect = false;
				segments_intersect = false;
				intersection = new PointF(float.NaN, float.NaN);
				close_p1 = new PointF(float.NaN, float.NaN);
				close_p2 = new PointF(float.NaN, float.NaN);
				return;
			}
			lines_intersect = true;

			float t2 =
				((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
					/ -denominator;

			// Find the point of intersection.
			intersection = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);

			// The segments intersect if t1 and t2 are between 0 and 1.
			segments_intersect =
				((t1 >= 0) && (t1 <= 1) &&
				 (t2 >= 0) && (t2 <= 1));

			// Find the closest points on the segments.
			if (t1 < 0)
			{
				t1 = 0;
			}
			else if (t1 > 1)
			{
				t1 = 1;
			}

			if (t2 < 0)
			{
				t2 = 0;
			}
			else if (t2 > 1)
			{
				t2 = 1;
			}

			close_p1 = new PointF(p1.X + dx12 * t1, p1.Y + dy12 * t1);
			close_p2 = new PointF(p3.X + dx34 * t2, p3.Y + dy34 * t2);
		}
    }
}
