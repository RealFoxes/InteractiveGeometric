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
		//debug
		public double debugNum;

		public Guid Id { get; set; }
		public FigureType FigureType { get; set; }
		public OperationType OperationType { get; set; }
		public Color Color { get; set; }
		public List<PointF> Points { get; set; }
		public bool IsSelected { get; set; }
		public virtual bool Calculated { get; set; } = true;
		public Figure()
		{
			Points = new List<PointF>();
			Id = Guid.NewGuid();
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
		public bool PointInPolygon(PointF point)
		{
			int n = Points.Count - 1;
			int k;
			PointF Pi, Pk;
			double x;
			List<int> Xb = new List<int>();
			bool check = false;
			Xb.Clear();

			for (int i = 0; i <= n; i++)
			{
				if (i < n) k = i + 1; else k = 0;
				Pi = Points[i];
				Pk = Points[k];
				if ((Pi.Y < point.Y && Pk.Y >= point.Y) || (Pi.Y >= point.Y && Pk.Y < point.Y))
				{
					x = (point.Y - Pi.Y) * (Pk.X - Pi.X) / (Pk.Y - Pi.Y) + Pi.X;
					Xb.Add((int)Math.Round(x));
				}
			}

			if (Xb.Count > 0)
			{
				Xb.Sort();
				for (int i = 0; i < Xb.Count; i += 2)
					if (point.X >= Xb[i] && point.X <= Xb[i + 1])
					{
						check = true;
						break;
					}
			}

			return check;
		}
		public Point GetCenter()
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
		public void RotateByPoint(PointF point)
		{
			PointF C = GetCenter();
			double angle = CalculateAngle(C, point);

			double radians = angle * Math.PI / 180.0;

			double[,] rotationMatrix = new double[3, 3];
			rotationMatrix[0, 0] = Math.Cos(radians);
			rotationMatrix[0, 1] = Math.Sin(radians);
			rotationMatrix[1, 0] = -Math.Sin(radians);
			rotationMatrix[1, 1] = Math.Cos(radians);
			rotationMatrix[2, 2] = 1;

			// Translate to origin
			double[,] translateToOrigin = new double[3, 3];
			translateToOrigin[0, 0] = translateToOrigin[1, 1] = translateToOrigin[2, 2] = 1;
			translateToOrigin[2, 0] = -C.X;
			translateToOrigin[2, 1] = -C.Y;

			// Translate back
			double[,] translateBack = new double[3, 3];
			translateBack[0, 0] = translateBack[1, 1] = translateBack[2, 2] = 1;
			translateBack[2, 0] = C.X;
			translateBack[2, 1] = C.Y;

			// Combine transformations
			double[,] combinedMatrix = Multiplication(translateToOrigin, rotationMatrix);
			combinedMatrix = Multiplication(combinedMatrix, translateBack);

			ApplyTransformationMatrix(new double[1, 3], combinedMatrix);
		}
		public void ScaleY(PointF point)
		{
			const double scaleFactorMultyply = 0.01;
			PointF C = GetCenter();

			double deltaX = C.X - point.X;
			double deltaY = C.Y - point.Y;
			double scaleFactor = Math.Sqrt(deltaX * deltaX + deltaY * deltaY) * scaleFactorMultyply;

			for (int i = 0; i < Points.Count; i++)
			{
				float newY = C.Y + (Points[i].Y - C.Y) * (float)scaleFactor;
				Points[i] = new PointF(Points[i].X, newY);
			}
		}
		public void Mirror()
		{
			Point C = GetCenter(); // Get the center of the figure

			// Translation to origin matrix
			double[,] translateToOrigin = new double[3, 3]
			{
				{ 1, 0, 0 },
				{ 0, 1, 0 },
				{ -C.X, -C.Y, 1 }
			};

			// Reflection matrix (mirror with respect to the origin)
			double[,] reflectionMatrix = new double[3, 3]
			{
				{ -1, 0, 0 },
				{ 0, -1, 0 },
				{ 0, 0, 1 }
			};

			// Translation back to the original position matrix
			double[,] translateBack = new double[3, 3]
			{
				{ 1, 0, 0 },
				{ 0, 1, 0 },
				{ C.X, C.Y, 1 }
			};

			// Combine the transformations: translate to origin -> reflect -> translate back
			double[,] combinedMatrix = Multiplication(translateToOrigin, reflectionMatrix);
			combinedMatrix = Multiplication(combinedMatrix, translateBack);

			// Apply the combined transformation matrix to the points of the figure
			ApplyTransformationMatrix(new double[1, 3], combinedMatrix);
		}

		private double CalculateAngle(PointF point1, PointF point2)
		{
			float xDiff = point1.X - point2.X;
			float yDiff = point1.Y - point2.Y;
			return Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

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

