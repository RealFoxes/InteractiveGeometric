namespace InteractiveGeometric
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
    }
}