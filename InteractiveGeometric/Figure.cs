namespace InteractiveGeometric
{
    public class Figure
    {
        private FigureType figureType;
        private OperationType operationType;
        private List<PointF> points;
        public Figure(FigureType figureType, OperationType operationType, List<PointF> points)
        {
            this.figureType = figureType;
            this.operationType = operationType;
            this.points = points;
        }
    }
}