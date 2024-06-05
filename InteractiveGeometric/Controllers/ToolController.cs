using InteractiveGeometric.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;

namespace InteractiveGeometric.Controllers
{
    public class ToolController
    {
        public readonly Panel AdditionalPanel;
        public readonly FiguresController figuresController;

        public Color SelectedColor;
        private Tool SelectedTool;
        public ToolController(Panel panelAdditionalOption, FiguresController figuresController)
        {
            SelectedTool = new ToolAddFigure(this);
            AdditionalPanel = panelAdditionalOption;
            this.figuresController = figuresController;
            SelectedColor = Color.Black;
        }

        public List<PointF> GetSelectedPoints() => SelectedTool?.SelectedPoints;
        public void ToolChanging(ToolType tag)
        {
            AdditionalPanel.Controls.Clear();
            AdditionalPanel.Visible = false;
            switch (tag)
            {
                case ToolType.None:
                    break;
                case ToolType.AddFigure:
                    SelectedTool = new ToolAddFigure(this);
                    break;
                case ToolType.Transform:
                    SelectedTool = new ToolTransform(this);
                    break;
                case ToolType.Operation:
                    SelectedTool = new ToolOperation(this);
                    break;
                case ToolType.SelectDeleting:
                    SelectedTool = new ToolSelectDeleting(this);
                    break;
                default:
                    break;
            }
            ToolOptionChanging(0);

		}
        public void ToolOptionChanging(int indexOption)
        {
            AdditionalPanel.Controls.Clear();
			AdditionalPanel.Visible = false;
			SelectedTool.ChangeOption(indexOption);
        }



        public void UseTool(Point point)
        {
            SelectedTool.Use(point);
        }
        public bool MouseMove(Point point)
        {
            if (SelectedTool is not IDragMovable sizeble) return false;
            if (SelectedTool.ToolMode != ToolMode.DragMove) return false;
            sizeble.Move(point);
            return true;
        }

        public bool MouseUp(Point point)
        {
            if (SelectedTool is not IDragMovable sizeble) return false;
            if (SelectedTool.ToolMode != ToolMode.DragMove) return false;
            sizeble.End(point);
            return true;
        }


        public void Complete()
        {
            SelectedTool.Complete();
        }

        public void Reset()
        {
            SelectedTool.Reset();
        }
    }

    public enum ToolMode
    {
        None,
        SelectPoint,
        SelectFigure,
        DragMove
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
