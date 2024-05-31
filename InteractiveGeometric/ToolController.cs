using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGeometric
{
    public class ToolController
    {
        private List<PointF> SelectedPoints;
        private readonly MainForm form;
        private readonly FiguresController figuresController;
        private ToolMode toolMode;
        public ToolType SelectedTool;
        public Color SelectedColor;
        public int SelectedToolOption;
        private Action completeAction;
        public ToolController(MainForm form, FiguresController figuresController)
        {
            this.SelectedPoints = new List<PointF>();
            this.form = form;
            this.figuresController = figuresController;
            this.toolMode = ToolMode.None;
            this.SelectedColor = Color.Black;
            this.completeAction = () => { };
        }
        public void AddFigure(Point point)
        {
            switch ((FigureType)SelectedToolOption)
            {
                case FigureType.None:
                    return;
                case FigureType.ER:
                    if (toolMode == ToolMode.None)
                    {
                        toolMode = ToolMode.SelectPoint;
                        completeAction = () => figuresController.CreateFigure(FigureType.ER, new List<PointF>(SelectedPoints), SelectedColor); 
                    }
                    SelectedPoints.Add(point);
                    if (SelectedPoints.Count == 4) Complete();
                    break;
                case FigureType.FPg:
					if (toolMode == ToolMode.None)
					{
						toolMode = ToolMode.SelectPoint;
						completeAction = () => figuresController.CreateFigure(FigureType.FPg, new List<PointF>(SelectedPoints), SelectedColor);
					}
					SelectedPoints.Add(point);
					break;
                case FigureType.Zv:
                    break;
            }
        }
        public void Transform(Point point)
        {
            switch ((TransformType)SelectedToolOption)
            {
                case TransformType.None:
                    return;
                case TransformType.Move:
                    break;
                case TransformType.Rf:
                    break;
                case TransformType.Syc:
                    break;
                case TransformType.Mc:
                    break;
            }
        }
        public void Operation(Point point)
        {
            switch ((OperationType)SelectedToolOption)
            {
                case OperationType.None:
                    return;
                case OperationType.Union:
                    break;
                case OperationType.Intersection:
                    break;
            }
        }
        public void SelectDeleting(Point point)
        {

        }

        public void ToolChanging(ToolType tag)
        {
            SelectedTool = tag;
			SelectedToolOption = 1;
			Reset();
        }
        public void ToolOptionChanging(int optionIndex)
        {
            SelectedToolOption = optionIndex;
            Reset();
        }

        public void MouseMove(MouseEventArgs e)
        {
            
        }
        public void Complete()
        {
            completeAction.Invoke();
			Reset();
		}
        public void Reset()
        {
			SelectedPoints.Clear();
            toolMode = ToolMode.None;
            completeAction = () => { };
        }

        public void UseTool(Point point)
        {
            switch (SelectedTool)
            {
                case ToolType.None:
                    return;
                case ToolType.AddFigure:
                    AddFigure(point);
                    break;
                case ToolType.Transform:
                    Transform(point);
                    break;
                case ToolType.Operation:
                    Operation(point);
                    break;
                case ToolType.SelectDeleting:
                    SelectDeleting(point);
                    break;
            }
        }
        public List<PointF> GetSelectedPoints() => SelectedPoints;
    }
    public enum ToolMode
    {
        None,
        SelectPoint,
        SelectFigure,
        DragSize
    }
    public enum ToolType
    {
        None = -1,
        AddFigure,
        Transform,
        Operation,
        SelectDeleting,
    }
    public enum FigureType
    {
        None = -1,
        /// <summary>
        /// Кубический сплайн
        /// </summary>
        ER,

        /// <summary>
        /// Произвольный n-угольник
        /// </summary>
        FPg,

        /// <summary>
        /// Правильная n-конечная звезда
        /// </summary>
        Zv
    }
    public enum TransformType
    {
        None = -1,
        /// <summary>
        /// Перемещение
        /// </summary>
        Move,

        /// <summary>
        /// Поворот вокруг центра фигуры на произвольный угол
        /// </summary>
        Rf,

        /// <summary>
        /// Масштабирование по оси Y относительно заданного центра
        /// </summary>
        Syc,

        /// <summary>
        /// Зеркальное отражение относительно заданного центра
        /// </summary>
        Mc
    }
    public enum OperationType
    {
        None = -1,
        /// <summary>
        /// Объединение
        /// </summary>
        Union,

        /// <summary>
        /// Пересечение
        /// </summary>
        Intersection
    }
}
