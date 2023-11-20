
using System;
using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;

namespace ThreeInRow
{
    public partial class Level1 : Form
    {
        GameField gameField;
        int size;
        int startPoint;
        public Level1()
        {
            InitializeComponent();
            Init();
        }

        public void Level1_Load(object Sender, System.EventArgs e)
        {

        }

        public void Init()
        {
            size = 60;
            startPoint = 160;
            gameField = new GameField(8, 8);
            gameField.FillRandomElements();

            Invalidate();
        }

        public void Update(object sender, EventArgs e)
        {
            gameField.MooveElementsDouwn();
            Invalidate();
        }

        public void DrawMap(Graphics g)
        {
            for (int i = 0; i < gameField.rowsCount; i++)
            {
                for (int j = 0; j < gameField.columnsCount; j++)
                {
                    Figure figure = gameField.GetElement(i, j);
                    Rectangle imageRectangle = new Rectangle(
                        startPoint + i * size,
                        startPoint + j * size,
                        figure.GetSize().Width,
                        figure.GetSize().Height
                    );

                    g.DrawImage(gameField.GetElement(i, j)._bitmap, imageRectangle);
                }
            }
        }

        public void DrawGrid(Graphics g)
        {
            for (int i = 0; i <= gameField.rowsCount; i++)
            {
                Point leftPoint = new Point(startPoint, startPoint + i * size);
                Point rightPoint = new Point(startPoint + gameField.rowsCount * size,
                    startPoint + i * size);

                g.DrawLine(Pens.Black, leftPoint, rightPoint);
            }

            for (int i = 0; i <= gameField.columnsCount; i++)
            {
                Point upPoint = new Point(startPoint + i * size, startPoint);
                Point douwnPoint = new Point(startPoint + i * size,
                    startPoint + gameField.columnsCount * size);

                g.DrawLine(Pens.Black, upPoint, douwnPoint);
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawMap(e.Graphics);
            DrawGrid(e.Graphics);
        }

        private void SelectElement(object sender, MouseEventArgs e)
        {
            int x = e.X - startPoint;
            int y = e.Y - startPoint;

            if (x < 0 || x >= size * gameField.columnsCount ||
                y < 0 || y >= size * gameField.rowsCount)
            {
                gameField.UnSelectElement();
                MessageBox.Show("X: " + x + " Y: " + y + "hight: " + size * gameField.columnsCount + "wight: " + size * gameField.rowsCount);
                return;
            }

            x = x / size;
            y = y / size;

            gameField.SelectElement(x, y);
        }
    }
}
