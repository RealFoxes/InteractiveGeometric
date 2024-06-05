using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InteractiveGeometric.Controllers;

namespace InteractiveGeometric.Figures
{
    public class Figure
	{
		public FigureType FigureType { get; set; }
		public OperationType OperationType { get; set; }
		public Color Color { get; set; }
		public List<PointF> Points { get; set; }
		public virtual bool Calculated { get; set; } = true;
		public Figure()
		{
			Points = new List<PointF>();
		}
		public Figure Clone()
		{
			var obj = (Figure)MemberwiseClone();
			obj.Points = new List<PointF>(Points);
			return obj;
		}
		public virtual void Calculate()
		{

		}

		public void Move(PointF newPoint)
		{
			PointF currentCenter = GetCenter();
			double dx = newPoint.X - currentCenter.X;
			double dy = newPoint.Y - currentCenter.Y;

			double[,] mat1 = new double[1, 3];
			double[,] mat2 = new double[3, 3];

			mat2[0, 0] = mat2[1, 1] = mat2[2, 2] = 1;

			mat2[2, 0] = dx;
			mat2[2, 1] = dy;

			ApplyTransformationMatrix(mat1, mat2);
		}
		private Point GetCenter()
		{
			Point C = new Point();
			int xMax = 0, xMin = (int)Math.Round(Points[0].X), yMax = 0, yMin = (int)Math.Round(Points[0].Y);
			for (int i = 0; i < Points.Count; i++)
			{
				if (Points[i].X > xMax) xMax = (int)Math.Round(Points[i].X);
				else if (Points[i].X < xMin) xMin = (int)Math.Round(Points[i].X);
				if (Points[i].Y > yMax) yMax = (int)Math.Round(Points[i].Y);
				else if (Points[i].Y < yMin) yMin = (int)Math.Round(Points[i].Y);
			}
			C.X = xMin + ((xMax - xMin) / 2);
			C.Y = yMin + ((yMax - yMin) / 2);
			return C;
		}
		private void ApplyTransformationMatrix(double[,] matrix1, double[,] matrix2)
		{
			int n = Points.Count();
			PointF fP = new PointF();
			double[,] result;
			for (int i = 0; i < n; i++)
			{
				matrix1[0, 0] = Points[i].X; matrix1[0, 1] = Points[i].Y; matrix1[0, 2] = 1;
				result = Multiplication(matrix1, matrix2);
				fP.X = (float)result[0, 0]; fP.Y = (float)result[0, 1];
				Points[i] = fP;
			}
		}
		private double[,] Multiplication(double[,] matrix1, double[,] matrix2) // умножение матриц
		{
			double[,] result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
			for (int i = 0; i < matrix1.GetLength(0); i++)
			{
				for (int j = 0; j < matrix2.GetLength(1); j++)
				{
					for (int k = 0; k < matrix2.GetLength(0); k++)
					{
						result[i, j] += matrix1[i, k] * matrix2[k, j];
					}
				}
			}
			return result;
		}
	}
}

