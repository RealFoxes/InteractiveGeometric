using InteractiveGeometric.Tools;
using InteractiveGeometric.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
		private readonly Label labelToolInfo;

        public Color SelectedColor;
        private Tool SelectedTool;
        public ToolController(Panel panelAdditionalOption, Label labelToolInfo, FiguresController figuresController)
        {
            SelectedTool = new ToolAddFigure(this);
            AdditionalPanel = panelAdditionalOption;
			this.labelToolInfo = labelToolInfo;
			this.figuresController = figuresController;
            SelectedColor = Color.Black;
            Debug();
        }
        private void Debug()
        {
            //var fig1 = figuresController.CreateFigure(FigureType.FPg, new List<PointF> { new PointF(365, 295), new PointF(170, 120), new PointF(590, 115) }, Color.Bisque);
            //var fig2 = figuresController.CreateFigure(FigureType.FPg, new List<PointF> { new PointF(220, 250), new PointF(532, 250), new PointF(370, 70) }, Color.DarkCyan);
            var fig1 = figuresController.CreateNStar(3, new Rectangle(100,100,300,300), Color.DarkCyan);
            var fig2 = figuresController.CreateNStar(3, new Rectangle(400,400,-300,-300), Color.Bisque);

            var fig3 = figuresController.CreateNStar(3, new Rectangle(600,100,300,300), Color.DarkCyan);
            var fig4 = figuresController.CreateNStar(3, new Rectangle(900,400,-300,-300), Color.Bisque);
            figuresController.Figures.Add(fig1);
            figuresController.Figures.Add(fig2);
            figuresController.Figures.Add(fig3);
            figuresController.Figures.Add(fig4);
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
            PrintToolInfo(SelectedTool.ToolOptionInfos[indexOption]);
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
        public void PrintToolInfo(string info)
        {
            labelToolInfo.ForeColor = Color.Black;
            labelToolInfo.Text = "Подсказка: " + info;
		}
		public void PrintToolError(string error)
		{
			labelToolInfo.ForeColor = Color.DarkRed;
            labelToolInfo.Text = "Ошибка: " + error;
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
