﻿namespace InteractiveGeometric
{
    public class Figure
    {
		public FigureType FigureType { get; set; }
		public OperationType OperationType { get; set; }
		public Color Color { get; set; }
		public List<PointF> Points { get; set; }
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
	}

	public class NStar : Figure
	{
        public int NumRays { get; set; }
    }
}