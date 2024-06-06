namespace InteractiveGeometric.Figures
{

    public class NStar : Figure
    {
        public Rectangle Bounds { get; set; }
        public int NumRays { get; set; }
        public override bool Calculated { get; set; } = false;
        public override void Calculate()
        {
            List<PointF> starPoints = new List<PointF>();

            float centerX = Bounds.X + Bounds.Width / 2f;
            float centerY = Bounds.Y + Bounds.Height / 2f;
            float radiusOuter = Math.Min(Bounds.Width, Bounds.Height) / 2f;
            float radiusInner = radiusOuter / 2.5f;

            double angleStep = Math.PI / NumRays;

            for (int i = 0; i < 2 * NumRays; i++)
            {
                double angle = i * angleStep - Math.PI / 2;
                float radius = (i % 2 == 0) ? radiusOuter : radiusInner;
                float x = centerX + (float)(radius * Math.Cos(angle));
                float y = centerY + (float)(radius * Math.Sin(angle));
                starPoints.Add(new PointF(x, y));
            }

            Points = starPoints;
			Calculated = true;
		}
    }
}