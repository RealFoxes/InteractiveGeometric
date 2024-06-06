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
			DrawSTOs();

			pictureBox.Image = bitmap;
            pictureBox.Refresh();
        }

        private void DrawFigures()
        {

            var debugStr = string.Empty;
            foreach (var figure in figuresController.Figures)
            {
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
		private void DrawSTOs()
		{
			foreach (var sto in figuresController.STOs)
			{
				DrawSTO(sto);
			}
		}
		// я в рот ебал того кто это написал, но сам не смог осилить поэтому вставил 
		//потратил миллион лет на алгоритмы для ТМО Вейлера–Атертона и Грайнера–Хорманна
		// Вейлера–Атертона не может обработать дыры внутри себя на него и потратил кучу времени, пояснеие картинка в корне репо
		// Грайнера–Хорманна вроде может сделать адекватно ТМО но на него уже сил не хватило
		private void DrawSTO(SetTheoreticOperations sto)
		{
			var fig1 = sto.Figure1;
			var fig2 = sto.Figure2;
			List<int> Xal = new List<int>(), Xar = new List<int>();//список левых и список правых границ фигуры А
			List<int> Xbl = new List<int>(), Xbr = new List<int>();//список левых и список правых границ фигуры В
			int YminA = (int)fig1.Points[0].Y;//нижняя граница фигуры А
			int YmaxA = (int)fig1.Points[0].Y;//верхняя граница фигуры А
			int YminB = (int)fig2.Points[0].Y;//нижняя граница фигуры В
			int YmaxB = (int)fig2.Points[0].Y;//верхняя граница фигуры В
			int ko;
			float xF;//вычисленный икс
			int X;//округленный вычисленный икс
			int AindexMin = 1;//индекс точки, являющейся нижней границей фигуры А
			bool aCW;//обход фигуры А
			int BindexMin = 1;//индекс точки, являющейся нижней границей фигуры В
			bool bCW;//обход фигуры В

			for (int i = 1; i < fig1.Points.Count() - 1; i++)
			{
				if (YminA > fig1.Points[i].Y)
				{
					YminA = (int)fig1.Points[i].Y;
					AindexMin = i;
				}
				if (YmaxA < fig1.Points[i].Y)
				{
					YmaxA = (int)fig1.Points[i].Y;
				}
			}
			for (int i = 1; i < fig2.Points.Count() - 1; i++)
			{
				if (YminB > fig2.Points[i].Y)
				{
					YminB = (int)fig2.Points[i].Y;
					BindexMin = i;
				}
				if (YmaxB < fig2.Points[i].Y)
				{
					YmaxB = (int)fig2.Points[i].Y;
				}
			}

			//вычисление обхода фигуры А
			if ((fig1.Points[AindexMin - 1].X * (fig1.Points[AindexMin].Y - fig1.Points[AindexMin + 1].Y) +
				fig1.Points[AindexMin].X * (fig1.Points[AindexMin + 1].Y - fig1.Points[AindexMin - 1].Y) +
				fig1.Points[AindexMin + 1].X * (fig1.Points[AindexMin - 1].Y - fig1.Points[AindexMin].Y)) > 0)//формула площади треугольника
				aCW = true;//если площадь треугольника > 0 обход фигуры по часовой стрелке
			else aCW = false;//иначе против часовой
							 //вычисление обхода фигуры В
			if ((fig2.Points[BindexMin - 1].X * (fig2.Points[BindexMin].Y - fig2.Points[BindexMin + 1].Y) +
				fig2.Points[BindexMin].X * (fig2.Points[BindexMin + 1].Y - fig2.Points[BindexMin - 1].Y) +
				fig2.Points[BindexMin + 1].X * (fig2.Points[BindexMin - 1].Y - fig2.Points[BindexMin].Y)) > 0)
				bCW = true;
			else bCW = false;

			for (int Y = Math.Min(YminA, YminB); Y <= Math.Max(YmaxA, YmaxB); Y++)//для всех строк от Ymin до Ymax
			{
				Xal.Clear();
				Xar.Clear();
				for (int i = 0; i < fig1.Points.Count(); i++)//проход по всем точкам фигуры А
				{
					if (i < fig1.Points.Count() - 1)//если i не последняя точка
					{
						ko = i + 1;//ко - следующая точка после i
					}
					else ko = 0;//иначе ко - первая точка
					if (((fig1.Points[i].Y < Y) && (fig1.Points[ko].Y >= Y)) || ((fig1.Points[i].Y >= Y) && (fig1.Points[ko].Y < Y)))
					{
						xF = ((float)Y - (float)fig1.Points[i].Y) / ((float)fig1.Points[ko].Y - (float)fig1.Points[i].Y) * ((float)fig1.Points[ko].X - (float)fig1.Points[i].X) + (float)fig1.Points[i].X; //нахождение икса точки пересечения строки Y со стороной фигуры А с помощью уравнения
						X = (int)Math.Round(xF);
						if (aCW)//если по часовой
						{
							if (fig1.Points[ko].Y - fig1.Points[i].Y > 0) Xar.Add(X); //заполнение списка "правых" иксов фигуры А
							else Xal.Add(X);//заполнение списка "левых" иксов фигуры А
						}
						else if (!aCW)
						{
							if (fig1.Points[ko].Y - fig1.Points[i].Y < 0) Xar.Add(X);
							else Xal.Add(X);
						}
					}
				}
				Xal.Sort();//сортировка по возрастанию
				Xar.Sort();
				//все тоже самое для фигуры В
				Xbl.Clear();
				Xbr.Clear();
				for (int i = 0; i < fig2.Points.Count(); i++)
				{
					if (i < fig2.Points.Count() - 1)
					{
						ko = i + 1;
					}
					else ko = 0;
					if (((fig2.Points[i].Y < Y) && (fig2.Points[ko].Y >= Y)) || ((fig2.Points[i].Y >= Y) && (fig2.Points[ko].Y < Y)))
					{
						xF = ((float)Y - (float)fig2.Points[i].Y) / ((float)fig2.Points[ko].Y - (float)fig2.Points[i].Y) * ((float)fig2.Points[ko].X - (float)fig2.Points[i].X) + (float)fig2.Points[i].X;
						X = (int)Math.Round(xF);
						if (bCW)
						{
							if (fig2.Points[ko].Y - fig2.Points[i].Y > 0) Xbr.Add(X);
							else Xbl.Add(X);
						}
						else if (!bCW)
						{
							if (fig2.Points[ko].Y - fig2.Points[i].Y < 0) Xbr.Add(X);
							else Xbl.Add(X);
						}
					}
				}
				Xbl.Sort();
				Xbr.Sort();
				List<int> Mx = new List<int>();//список для записи координаты x границы сегмента
				int[] MdQ = new int[Xal.Count() + Xar.Count() + Xbl.Count() + Xbr.Count()];//массив для записи соответствующего приращения пороговой функции с учетом веса операнда
				int[] SetQ = new int[2];//множество значений суммы Q пороговых функций операндов, соответствующее заданной ТМО;
				int nM, n;
				List<int> Xrl = new List<int>(), Xrr = new List<int>();//Результатом работы алгоритма будут массивы Xrl и Xrr левых и правых границ сегментов сечения результирующей области строкой Y.
				if (sto.Operation == OperationType.Union) { SetQ[0] = 1; SetQ[1] = 3; } // объединение
				else if (sto.Operation == OperationType.Intersection) { SetQ[0] = 2; SetQ[1] = 2; }//разность
				n = Xal.Count();
				for (int i = 1; i <= n; i++)
				{
					Mx.Add(Xal[i - 1]);
					MdQ[i - 1] = 2;
				}
				nM = n;
				n = Xar.Count();
				for (int i = 1; i <= n; i++)
				{
					Mx.Add(Xar[i - 1]);
					MdQ[nM + i - 1] = -2;
				}
				nM += n;
				n = Xbl.Count();
				for (int i = 1; i <= n; i++)
				{
					Mx.Add(Xbl[i - 1]);
					MdQ[nM + i - 1] = 1;
				}
				nM += n;
				n = Xbr.Count();
				for (int i = 1; i <= n; i++)
				{
					Mx.Add(Xbr[i - 1]);
					MdQ[nM + i - 1] = -1;
				}
				nM += n; // общее число элементов в массиве Mх
				for (int i = 0; i < nM; i++)//сортировка списка Мх по возрастанию и массива МdQ относительно массива Мх
				{
					for (int j = 0; j < nM - 1 - i; j++)
					{
						if (Mx[j] > Mx[j + 1])
						{
							int buff1 = Mx[j];
							int buff2 = MdQ[j];

							Mx[j] = Mx[j + 1];
							MdQ[j] = MdQ[j + 1];

							Mx[j + 1] = buff1;
							MdQ[j + 1] = buff2;
						}
					}
				}
				int Q = 0;
				for (int i = 0; i < nM; i++)//проход по всему списку Мх
				{
					int x = Mx[i];
					int Qnew = Q + MdQ[i];
					if (!(Q >= SetQ[0] && Q <= SetQ[1]) && (Qnew >= SetQ[0] && Qnew <= SetQ[1]))
					{
						Xrl.Add(x);
					}
					if ((Q >= SetQ[0] && Q <= SetQ[1]) && !(Qnew >= SetQ[0] && Qnew <= SetQ[1]))
					{
						Xrr.Add(x);
					}
					Q = Qnew;
				}
				var count = 0;
				for (int i = 0; i < Xrl.Count(); i++)//закраска результирующей области
				{
					g.DrawLine(new Pen(fig1.Color), Xrl[i], Y, Xrr[i], Y);
					count++;
				}
				Console.WriteLine(count);
			}
			return;
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
