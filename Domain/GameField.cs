using System;
using System.Collections.Generic;
using System.Drawing;
using ThreeInRow.Domain;

namespace ThreeInRow.Back
{
    public class GameField: IFigureDestroyedObservable
    {
        private Shape[,] _field;
        public int rowsCount { get; }
        public int columnsCount { get; }

        private Point selectedElement = new Point(-1, -1);

        private Direction _leftDirection = new Direction(-1, 0);
        private Direction _rightDirection = new Direction(1, 0);
        private Direction _upDirection = new Direction(0, 1);
        private Direction _douwnDirection = new Direction(0, -1);

        private FigureCreator _creator;

        private List<IFigureDestroyedObserver> figureDestroidObservers =
            new List<IFigureDestroyedObserver>();

        public GameField(int rowsCount, int columnsCount)
        {
            this.rowsCount = rowsCount;
            this.columnsCount = columnsCount;
            _field = new Figure[rowsCount, columnsCount];
            _creator = new RandomFigureCreator();
        }

        public void Subscribe(IFigureDestroyedObserver observer)
        {
            if (!figureDestroidObservers.Contains(observer))
            {
                figureDestroidObservers.Add(observer);
            }   
        }

        public void Unsubscribe(IFigureDestroyedObserver observer)
        {
            if (figureDestroidObservers.Contains(observer))
            {
                figureDestroidObservers.Remove(observer);
            }
        }
        
        public void FigureDestroyed(int points)
        {
            foreach(var observer in figureDestroidObservers)
            {
                observer.FigureDestroyed(points);
            }
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
                if (_field[j, 0].Type == ShapeType.Empty)
                {
                    _field[j, 0] = _creator.CreateFigure();
                }
            }

            for (int i = 0; i < columnsCount - 1; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    if (_field[j, i + 1].Type == ShapeType.Empty)
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

            int points = 0;
            points += FindAndDestroyMatchingForElement(selectedElement);
            points += FindAndDestroyMatchingForElement(secondElement);

            if (points == 0)
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
            Shape figure = _field[element1.X, element1.Y];
            figure.UnSelect();
            _field[element1.X, element1.Y] = _field[element2.X, element2.Y];
            _field[element2.X, element2.Y] = figure;
        }

        public int FindAndDestroyMatchingForElement(Point element)
        {
            int points = 0;
            Shape figure = _field[element.X, element.Y];

            List<Point> verticalMatchedPoints = FindMatchingInDirection(element.X, element.Y, _upDirection, figure);
            verticalMatchedPoints.AddRange(FindMatchingInDirection(element.X, element.Y, _douwnDirection, figure));


            if (1 + verticalMatchedPoints.Count > 2)
            {
                points += CountPoints(verticalMatchedPoints);
            }

            List<Point> horizontalMatchedPoints = FindMatchingInDirection(element.X, element.Y, _leftDirection, figure);
            horizontalMatchedPoints.AddRange(FindMatchingInDirection(element.X, element.Y, _rightDirection, figure));

            if (1 + horizontalMatchedPoints.Count > 2)
            {
                points += CountPoints(horizontalMatchedPoints);
            }

            if (points == 0) return 0;

            if (verticalMatchedPoints.Count == 2 && horizontalMatchedPoints.Count == 2)
            {
                HorizontalDestroyerBonusCommand bonus = new HorizontalDestroyerBonusCommand();
                bonus.SetGameField(this);
                return points;
            } 
            else if (horizontalMatchedPoints.Count == 3)
            {
                VerticalDestroyerBonusCommand bonus = new VerticalDestroyerBonusCommand();
                bonus.SetGameField(this);
                return points;
            } else if (verticalMatchedPoints.Count == 3)
            {
                HorizontalDestroyerBonusCommand bonus = new HorizontalDestroyerBonusCommand();
                bonus.SetGameField(this);
                return points;
            } 
            else if (horizontalMatchedPoints.Count == 4 || verticalMatchedPoints.Count == 4)
            {
                HorizontalDestroyerBonusCommand bonus = new HorizontalDestroyerBonusCommand();
                bonus.SetGameField(this);
                return points;
            }

            figure.Destroy(element);
            return points;
        }

        private int CountPoints(List<Point> points)
        {
            int countPoints = 0;
            for (int i = 0; i < points.Count; i++)
            {
                DestroyFigure(points[i]);
            }

            return countPoints;
        }

        public void DestroyFigure(Point position)
        {
            int points = _field[position.X, position.Y].Destroy(position);

            FigureDestroyed(points);
        }

        private List<Point> FindMatchingInDirection(int x, int y, Direction direction, Shape figure)
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

        public Shape GetElement(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < columnsCount && y < rowsCount)
            {
                return _field[x, y];
            }

            return null;
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
