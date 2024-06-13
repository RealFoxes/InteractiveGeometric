using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGeometric.Figures;

namespace InteractiveGeometric.Controllers
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
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            g = Graphics.FromImage(bitmap);
        }

        public void Draw()
        {
            g.Clear(Color.White);

            DrawFigures();
            DrawToolSelectedPoints();

			pictureBox.Image = bitmap;
            pictureBox.Refresh();
        }

        private void DrawFigures()
        {

            var debugStr = string.Empty;
            foreach (var figure in figuresController.Figures)
            {
				if (figure.IsSelected)
				{
                    var selectedFigure = figure.Clone();
                    selectedFigure.Color = Color.IndianRed;
                    selectedFigure.ScaleXY(5);
					DrawFigure(selectedFigure);
				}
				DrawFigure(figure);
                debugStr += $"Type:{figure.FigureType} \nColor:{figure.Color} \nPoints:{string.Join(", ", figure.Points.ToArray())}\n";
            }
            if (figuresController.Preview != null)
            {
                var previewFigure = figuresController.Preview.Clone();
                var color = previewFigure.Color;
                previewFigure.Color = Color.FromArgb(120, color.R, color.G, color.B);

				DrawFigure(previewFigure);
            }
			//debug
            //g.DrawString(debugStr, new Font(FontFamily.GenericMonospace, 12), new SolidBrush(Color.Black), 10, 10);
        }
        private void DrawFigure(Figure figure)
        {
            switch (figure.FigureType)
            {
                case FigureType.None:
                    break;
                case FigureType.Ln:
                    g.DrawLine(new Pen(figure.Color), figure.Points[0], figure.Points[1]);
                    break;
                case FigureType.ER:
                    DrawCubicSpline(figure.Points, figure.Color);
                    break;
                case FigureType.FPg:
                case FigureType.Zv:
                    if (!figure.Calculated)
                        figure.Calculate();
                    g.FillPolygon(new SolidBrush(figure.Color), figure.Points.ToArray());
                    break;
            }
        }

		private void DrawCubicSpline(List<PointF> points, Color color)
        {
            if (points.Count < 4) return;

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
    }
}
