using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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

        private Point _firstSelectedElement = new Point(-1, -1);
        private Point _firstSwapPosition = new Point(-1, -1);
        private Point _secondSelectedElement = new Point(-1, -1);
        private Point _secondSwapPosition = new Point(-1, -1);
        private int _figureSwapSpeed = 10;
        private bool _isReturnSwap = false;

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
        private Timer _gameTimer;


        public GameField(int rowsCount,
                         int columnsCount,
                         int matrixDrawingStartPoint,
                         int figureCellSize,
                         Timer timer)
        {
            this.rowsCount = rowsCount;
            this.columnsCount = columnsCount;
            _field = new Figure[rowsCount, columnsCount];
            _creator = new RandomFigureCreator();
            _matrixDrawingStartPoint = matrixDrawingStartPoint;
            _figureCellSize = figureCellSize;
            _gameTimer = timer;
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
                            _field[j, i].IsFalling = true;
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
                    else
                    {
                        _field[j, i].IsFalling = false;
                    }
                }
            }

            for (int j = 0; j < columnsCount; j++)
            {
                _field[j, columnsCount - 1].IsFalling = false;
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
            if (_firstSelectedElement.X == -1 || _firstSelectedElement.Y == -1)
            {
                _firstSelectedElement.X = x;
                _firstSelectedElement.Y = y;
                _field[x, y].IsSelected = true;
                return;
            }

            Direction direction = new Direction(x - _firstSelectedElement.X, y - _firstSelectedElement.Y);
            if (direction.GetLen() != 1)
            {
                UnSelectElement();
                return;
            }

            _secondSelectedElement = new Point(x, y);

            Figure firstSelectedFigure = _field[_firstSelectedElement.X, _firstSelectedElement.Y];
            Figure secondSelectedFigure = _field[_secondSelectedElement.X, _secondSelectedElement.Y];

            _firstSwapPosition 
                = new Point(secondSelectedFigure.position.X, secondSelectedFigure.position.Y);

            _secondSwapPosition
                = new Point(firstSelectedFigure.position.X, firstSelectedFigure.position.Y);

            _gameTimer.Tick += PlaySwapAnimation;
        }

        public void UnSelectElement()
        {
            if (_firstSelectedElement.X == -1 || _firstSelectedElement.Y == -1)
            {
                return;
            }

            _field[_firstSelectedElement.X, _firstSelectedElement.Y].IsSelected = false;
            _firstSelectedElement.X = -1;
            _firstSelectedElement.Y = -1;
        }

        public void Swap(Point element1, Point element2)
        {
            Figure figure = _field[element1.X, element1.Y];
            figure.IsSelected = false;
            _field[element1.X, element1.Y] = _field[element2.X, element2.Y];
            _field[element2.X, element2.Y] = figure;

           /* SwapPosition(_field[element1.X, element1.Y], _field[element2.X, element2.Y]);*/
        }

        private void PlaySwapAnimation(object sender, EventArgs e)
        {
            Figure firstSelectedFigure = _field[_firstSelectedElement.X, _firstSelectedElement.Y];
            int distanceX = firstSelectedFigure.position.X - _firstSwapPosition.X;
            int distanceY = firstSelectedFigure.position.Y - _firstSwapPosition.Y;

            Point distance = new Point(distanceX, distanceY);

            if (distanceX != 0 || distanceY != 0)
            {
                MoveSwapingElements(distance);
                return;
            }

            Swap(_firstSelectedElement, _secondSelectedElement);

            if (_isReturnSwap)
            {
                _gameTimer.Tick -= PlaySwapAnimation;
                _isReturnSwap = false;
                UnSelectElement();
                return;
            }

            if (!FindAndDestroyMatchingForElement(_firstSelectedElement)
                && !FindAndDestroyMatchingForElement(_secondSelectedElement))
            {
                _isReturnSwap = true;
                return;
            }

            _gameTimer.Tick -= PlaySwapAnimation;
            UnSelectElement();
        }

        private void MoveSwapingElements(Point distance)
        {
            if (distance.X != 0)
            {
                MoveSwapingElementsByX(distance.X);
            }
            else
            {
                MoveSwapingElementsByY(distance.Y);
            }

        }

        private void MoveSwapingElementsByX(int distanceX)
        {
            Figure firstSelectedFigure = _field[_firstSelectedElement.X, _firstSelectedElement.Y];
            Figure secondSelectedFigure = _field[_secondSelectedElement.X, _secondSelectedElement.Y];

            if (Math.Abs(distanceX) < _figureSwapSpeed)
            {
                firstSelectedFigure.position = _firstSwapPosition;
                secondSelectedFigure.position = _secondSwapPosition;
                return;
            }

            if (distanceX > 0)
            {
                firstSelectedFigure.position.X -= _figureSwapSpeed;
                secondSelectedFigure.position.X += _figureSwapSpeed;
            }
            else
            {
                firstSelectedFigure.position.X += _figureSwapSpeed;
                secondSelectedFigure.position.X -= _figureSwapSpeed;
            }
        }

        private void MoveSwapingElementsByY(int distanceY)
        {
            Figure firstSelectedFigure = _field[_firstSelectedElement.X, _firstSelectedElement.Y];
            Figure secondSelectedFigure = _field[_secondSelectedElement.X, _secondSelectedElement.Y];

            if (Math.Abs(distanceY) < _figureSwapSpeed)
            {
                firstSelectedFigure.position = _firstSwapPosition;
                secondSelectedFigure.position = _secondSwapPosition;
                return;
            }

            if (distanceY > 0)
            {
                firstSelectedFigure.position.Y -= _figureSwapSpeed;
                secondSelectedFigure.position.Y += _figureSwapSpeed;
            }
            else
            {
                firstSelectedFigure.position.Y += _figureSwapSpeed;
                secondSelectedFigure.position.Y -= _figureSwapSpeed;
            }
        }

        public bool FindAndDestroyMatchingForElement(Point element)
        {
            bool isMathced = false;
            Figure figure = _field[element.X, element.Y];

            if (figure.IsFalling)
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

            if (figure.IsDestroyd)
            {
                return;
            }

            if (figure.HasBonus())
            {
                points += UseBonus(figure.ExtractBonus(), point);
            }
            points += figure.Destroy();

            FigureDestroyed(points);
        }

        private int UseBonus(BaseBonus bonus, Point point)
        {
            return bonus.UseBonus(point, this, _gameTimer);
        }

        private List<Point> FindMatchingInDirection(int x, int y, Direction direction, Figure figure)
        {
            List<Point> matchedPoints = new List<Point>();
            x += direction.x;
            y += direction.y;

            while (x >= 0 && x < columnsCount && y >= 0 && y < rowsCount)
            {
                if (figure == _field[x, y] && !_field[x, y].IsFalling && !_field[x, y].IsDestroyd)
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
