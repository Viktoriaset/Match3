using System;
using System.Collections.Generic;
using System.Drawing;
using ThreeInRow.Domain;

namespace ThreeInRow.Back
{
    public class GameField : IFigureDestroyedObservable
    {
        private Figure[,] _field;
        public int rowsCount { get; }
        public int columnsCount { get; }

        private Point selectedElement = new Point(-1, -1);

        private Direction _leftDirection = new Direction(-1, 0);
        private Direction _rightDirection = new Direction(1, 0);
        private Direction _upDirection = new Direction(0, 1);
        private Direction _douwnDirection = new Direction(0, -1);

        private FigureCreator _creator;
        private List<IFigureDestroyedObserver> figureDestroyedObservers
            = new List<IFigureDestroyedObserver>();
        private BonusCreator _horizontalDestroyerBonusCreator = new HorizontalDestroyerBonusCreator();
        private BonusCreator _verticalDestroyerBonusCreator = new VerticalDestroyerBonusCreator();
        private BonusCreator _bombDestroyerBonusCreator;


        public GameField(int rowsCount, int columnsCount)
        {
            this.rowsCount = rowsCount;
            this.columnsCount = columnsCount;
            _field = new Figure[rowsCount, columnsCount];
            _creator = new RandomFigureCreator();
        }

        public void FillRandomElements()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    _field[i, j] = _creator.CreateFigure();
                }
            }
        }

        public void SearchMathcing()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    FindAndDestroyMatchingForElement(new Point(i, j));
                }
            }
        }

        public void MooveElementsDouwn()
        {
            for (int j = 0; j < columnsCount; j++)
            {
                if (_field[j, 0].Type == FigureType.Empty)
                {
                    _field[j, 0] = _creator.CreateFigure();
                }
            }

            for (int i = 0; i < columnsCount - 1; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    if (_field[j, i + 1].Type == FigureType.Empty)
                    {
                        Swap(new Point(j, i), new Point(j, i + 1));
                    }
                }
            }

            SearchMathcing();
        }

        public void SelectElement(int x, int y)
        {
            if (selectedElement.X == -1 || selectedElement.Y == -1)
            {
                selectedElement.X = x;
                selectedElement.Y = y;
                _field[x, y].Select();
                return;
            }

            Direction direction = new Direction(x - selectedElement.X, y - selectedElement.Y);
            if (direction.GetLen() != 1)
            {
                UnSelectElement();
                return;
            }

            Point secondElement = new Point(x, y);
            Swap(selectedElement, secondElement);

            if (!FindAndDestroyMatchingForElement(selectedElement)
                && !FindAndDestroyMatchingForElement(secondElement))
            {
                Swap(selectedElement, secondElement);
            }

            UnSelectElement();
        }

        public void UnSelectElement()
        {
            if (selectedElement.X == -1 || selectedElement.Y == -1)
            {
                return;
            }

            _field[selectedElement.X, selectedElement.Y].UnSelect();
            selectedElement.X = -1;
            selectedElement.Y = -1;
        }

        public void Swap(Point element1, Point element2)
        {
            Figure figure = _field[element1.X, element1.Y];
            figure.UnSelect();
            _field[element1.X, element1.Y] = _field[element2.X, element2.Y];
            _field[element2.X, element2.Y] = figure;
        }

        public bool FindAndDestroyMatchingForElement(Point element)
        {
            bool isMathced = false;
            Figure figure = _field[element.X, element.Y];

            List<Point> verticalMatchedPoints = FindMatchingInDirection(element.X, element.Y, _upDirection, figure);
            verticalMatchedPoints.AddRange(FindMatchingInDirection(element.X, element.Y, _douwnDirection, figure));


            if (1 + verticalMatchedPoints.Count > 2)
            {
                DestroyPointList(verticalMatchedPoints);
                isMathced = true;
            }

            List<Point> horizontalMatchedPoints = FindMatchingInDirection(element.X, element.Y, _leftDirection, figure);
            horizontalMatchedPoints.AddRange(FindMatchingInDirection(element.X, element.Y, _rightDirection, figure));

            if (1 + horizontalMatchedPoints.Count > 2)
            {
                DestroyPointList(horizontalMatchedPoints);
                isMathced = true;
            }

            if (!isMathced)
            {
                return false;
            }

            if (verticalMatchedPoints.Count == 2 && horizontalMatchedPoints.Count == 2)
            {
                figure.BonusCommand = _horizontalDestroyerBonusCreator.CreateBonus(); // TODO create bomb
            }
            else if (horizontalMatchedPoints.Count == 3)
            {
                figure.BonusCommand = _verticalDestroyerBonusCreator.CreateBonus();
            }
            else if (verticalMatchedPoints.Count == 3)
            {
                figure.BonusCommand = _horizontalDestroyerBonusCreator.CreateBonus();
            }
            else if (horizontalMatchedPoints.Count == 4 || verticalMatchedPoints.Count == 4)
            {
                figure.BonusCommand = _horizontalDestroyerBonusCreator.CreateBonus(); // TODO create bomb
            }

            DestroyElement(element);
            return true;
        }

        private void DestroyPointList(List<Point> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                DestroyElement(points[i]);
            }
        }

        public void DestroyElement(Point point)
        {
            int points = 0;
            if (_field[point.X, point.Y].HasBonus())
            {
                points += UseBonus(_field[point.X, point.Y].BonusCommand, point);
            }
            points += _field[point.X, point.Y].Destroy(point);

            FigureDestroyed(points);
        }

        private List<Point> FindMatchingInDirection(int x, int y, Direction direction, Figure figure)
        {
            List<Point> matchedPoints = new List<Point>();
            x += direction.x;
            y += direction.y;

            while (x >= 0 && x < columnsCount && y >= 0 && y < rowsCount)
            {
                if (figure == _field[x, y])
                {
                    matchedPoints.Add(new Point(x, y));
                    x += direction.x;
                    y += direction.y;
                }
                else
                {
                    return matchedPoints;
                }
            }

            return matchedPoints;
        }

        private int UseBonus(IBonusCommand bonus, Point point)
        {
            return bonus.UseBonus(point, this);
        }

        public Figure GetElement(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < columnsCount && y < rowsCount)
            {
                return _field[x, y];
            }

            return null;
        }

        public void Subscribe(IFigureDestroyedObserver observer)
        {
            if (!figureDestroyedObservers.Contains(observer))
            {
                figureDestroyedObservers.Add(observer);
            }
        }

        public void Unsubscribe(IFigureDestroyedObserver observer)
        {
            if (figureDestroyedObservers.Contains(observer))
            {
                figureDestroyedObservers.Remove(observer);
            }
        }

        public void FigureDestroyed(int points)
        {
            foreach (var observer in figureDestroyedObservers)
            {
                observer.FigureDestroyed(points);
            }
        }
    }

    public struct Direction
    {
        public Direction(int directionX, int directionY)
        {
            x = directionX;
            y = directionY;
        }

        public int x { get; set; }
        public int y { get; set; }

        public void SwapXAndY()
        {
            int buffer = x;
            x = y;
            y = buffer;
        }

        public int GetLen()
        {
            return Math.Abs(x + y);
        }

        public static Direction operator *(Direction direction, int number)
        {
            direction.x *= number;
            direction.y *= number;
            return direction;
        }
    }
}
