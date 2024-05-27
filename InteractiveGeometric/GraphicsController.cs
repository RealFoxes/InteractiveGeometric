using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveGeometric
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
            this.bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            this.g = Graphics.FromImage(bitmap);
        }

        public void Draw()
        {
            g.Clear(Color.White);
            DrawToolSelectedPoints();
            pictureBox.Image = this.bitmap;
            pictureBox.Refresh();
        }
        private void DrawToolSelectedPoints()
        {
            var s = 5;
            foreach (var point in toolController.GetSelectedPoints())
                g.DrawEllipse(new Pen(Color.Red, 1), point.X-s, point.Y-s, s, s);
        }
    }
}
