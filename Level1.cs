
using System;
using System.Drawing;
using System.Windows.Forms;
using ThreeInRow.Back;
using ThreeInRow.Domain;

namespace ThreeInRow
{
    public partial class Level1 : Form
    {
        GameField gameField;
        int size;
        int startPoint;
        Animator figureFallAnimator;
        WinConditionByTime winCondition;
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
            gameField = new GameField(8, 8, startPoint, size);
            gameField.FillRandomElements();

            winCondition = new WinConditionByTime(points_label, 120000, game_timer);
            gameField.Subscribe(winCondition);

            Invalidate();
        }

        public void Update(object sender, EventArgs e)
        {
            if (winCondition.isGameFinished(timer1.Interval))
            {
                timer1.Stop();
                DialogResult result = MessageBox.Show("Your points: " + winCondition.TotalPoints);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
            Invalidate();
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
            e.Graphics.Clear(Color.White);
            gameField.MooveElementsDouwn();
            gameField.DrawField(e.Graphics);
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
