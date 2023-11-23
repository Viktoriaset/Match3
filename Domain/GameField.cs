using System;
using System.Collections.Generic;
using System.Drawing;
using ThreeInRow.Domain;
using ThreeInRow.Domain.BonusCommand;
using ThreeInRow.Domain.BonusFactory;
using ThreeInRow.Domain.FigureFactory;

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
        private BonusCreator _bombDestroyerBonusCreator = new BombBonusCreator();

        private int _matrixDrawingStartPoint;
        private int _figureCellSize;


        public GameField(int rowsCount, int columnsCount, int matrixDrawingStartPoint, int figureCellSize)
        {
            this.rowsCount = rowsCount;
            this.columnsCount = columnsCount;
            _field = new Figure[rowsCount, columnsCount];
            _creator = new RandomFigureCreator();
            _matrixDrawingStartPoint = matrixDrawingStartPoint;
            _figureCellSize = figureCellSize;
        }

        public void FillRandomElements()
        {
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    EmptyFigureCreator figureCreator = new EmptyFigureCreator();
                    Figure figure = figureCreator.CreateFigure();
                    figure.position = new Point(
                        _matrixDrawingStartPoint + i * _figureCellSize,
                        _matrixDrawingStartPoint + j * _figureCellSize
                        );

                    _field[i, j] = figure;
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
            SpawnNewElements();

            for (int i = 0; i < columnsCount - 1; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {
                    if (_field[j, i + 1].Type == FigureType.Empty && _field[j, i].Type != FigureType.Empty)
                    {
                        if (_field[j, i].position.Y < _field[j, i + 1].position.Y)
                        {
                            _field[j, i].position.Y += 10;
                        }
                        else
                        {
                            _field[j, i].position = new Point(
                                _matrixDrawingStartPoint + j * _figureCellSize,
                                _matrixDrawingStartPoint + i * _figureCellSize
                                );
                            Figure figure = _field[j, i + 1];
                            _field[j, i + 1] = _field[j, i];
                            _field[j, i] = figure;
                            SwapPosition(_field[j, i + 1], _field[j, i]);
                        }
                    }

                }
            }

            SearchMathcing();
        }

        private void SwapPosition(Figure element1, Figure element2)
        {
            Point position = element1.position;
            element1.position = element2.position;
            element2.position = position;
        }

        private void SpawnNewElements()
        {
            for (int j = 0; j < columnsCount; j++)
            {
                if (_field[j, 0].Type == FigureType.Empty)
                {
                    Figure figure = _creator.CreateFigure();
                    figure.position = new Point(
                        _matrixDrawingStartPoint + j * _figureCellSize,
                        _matrixDrawingStartPoint
                        );

                    _field[j, 0] = figure;
                }
            }
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
            Point position = figure.position;
            _field[element1.X, element1.Y] = _field[element2.X, element2.Y];
            _field[element2.X, element2.Y] = figure;
            _field[element2.X, element2.Y].position = _field[element1.X, element1.Y].position;
            _field[element1.X, element1.Y].position = position;
        }

        public bool FindAndDestroyMatchingForElement(Point element)
        {
            bool isMathced = false;
            Figure figure = _field[element.X, element.Y];

            if (element.Y < rowsCount - 1 && _field[element.X, element.Y + 1].Type == FigureType.Empty)
            {
                return false;
            }

            List<Point> verticalMatchedPoints = FindMatchingInDirection(element.X, element.Y, _upDirection, figure);
            verticalMatchedPoints.AddRange(FindMatchingInDirection(element.X, element.Y, _douwnDirection, figure));

            List<Point> horizontalMatchedPoints = FindMatchingInDirection(element.X, element.Y, _leftDirection, figure);
            horizontalMatchedPoints.AddRange(FindMatchingInDirection(element.X, element.Y, _rightDirection, figure));

            bool isBonusSeted = ChekAndCreateBonus(
                horizontalMatchedPoints.Count,
                verticalMatchedPoints.Count, 
                figure
            );

            if (1 + verticalMatchedPoints.Count > 2)
            {
                DestroyPointList(verticalMatchedPoints);
                isMathced = true;
            }

            if (1 + horizontalMatchedPoints.Count > 2)
            {
                DestroyPointList(horizontalMatchedPoints);
                isMathced = true;
            }

            if (!isMathced)
            {
                return false;
            }

            if (isBonusSeted) return true;

            DestroyElement(element);
            return true;
        }

        private bool ChekAndCreateBonus(int horizontalMatchedCount, int verticalMatchedCount, Figure figure)
        {
            if (horizontalMatchedCount >= 4 || verticalMatchedCount >= 4)
            {
                figure.Bonus = _bombDestroyerBonusCreator.CreateBonus();
                return true;
            }
            else if (verticalMatchedCount >= 2 && horizontalMatchedCount >= 2)
            {
                figure.Bonus = _bombDestroyerBonusCreator.CreateBonus();
                return true;
            }
            else if (horizontalMatchedCount == 3)
            {
                figure.Bonus = _verticalDestroyerBonusCreator.CreateBonus();
                return true;
            }
            else if (verticalMatchedCount == 3)
            {
                figure.Bonus = _horizontalDestroyerBonusCreator.CreateBonus();
                return true;
            }

            return false;
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
            if (point.X < 0 || point.X >= columnsCount 
                || point.Y < 0 || point.Y >= rowsCount)
            {
                return;
            }  

            int points = 0;
            Figure figure = _field[point.X, point.Y];

            if (figure.HasBonus())
            {
                points += UseBonus(figure.ExtractBonus(), point);
            }
            points += figure.Destroy();

            FigureDestroyed(points);
        }

        private int UseBonus(BaseBonus bonus, Point point)
        {
            return bonus.UseBonus(point, this);
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

        public Figure GetElement(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < columnsCount && y < rowsCount)
            {
                return _field[x, y];
            }

            return null;
        }

        public void DrawField(Graphics g)
        {
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    _field[i, j].Draw(g);
                }
            }
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
