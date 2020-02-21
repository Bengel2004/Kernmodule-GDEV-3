using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astar
{
    /// <summary>
    /// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path
    /// Note that you will probably need to add some helper functions
    /// from the startPos to the endPos
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        List<Node> _closedList = new List<Node>();
        List<Node> _openList = new List<Node>();
        Node _startNode = new Node(startPos, null, 0, 0);
        Node _endNode = new Node(endPos, null, 0, 0);

        _openList.Add(_startNode);

        while (_openList.Count > 0) 
        {
            // Adds the 1st
            Node _current = _openList[0];

            for (int i = 1; i < _openList.Count; i++)
            {
                if (_openList[i].FScore <= _current.FScore)
                {
                    _current = _openList[i]; 
                }
            }

            if (_current.position == endPos)
            {
                _endNode = _current;
                break;
            }


            _openList.Remove(_current);
            _closedList.Add(_current);

            List<Cell> _neighbouringCells = GetNeighbourCells(grid[_current.position.x, _current.position.y], grid);
            Debug.Log(_neighbouringCells.Count);


            foreach (Cell _neighbourCell in _neighbouringCells)
            {
                Node _neighbourNode = new Node(_neighbourCell.gridPosition, _current, (int)_current.GScore + 1, Heuristic(_neighbourCell.gridPosition, endPos));

                if (_closedList.Contains(_neighbourNode))
                {
                    continue; // Safety Checks
                }


                int _moveCost = Mathf.RoundToInt(_current.GScore) + Heuristic(_current.position, _neighbourNode.position);

                if (_moveCost < _neighbourNode.GScore || !_openList.Any(n => n.position == _neighbourCell.gridPosition))
                {
                _neighbourNode.parent = _current;
                _neighbourNode.GScore = _moveCost;

                    if (!_openList.Contains(_neighbourNode))
                    {
                        _openList.Add(_neighbourNode);
                    }
                }
            }
        }

        return Trace(_startNode, _endNode);
    }


    private List<Vector2Int> Trace(Node _start, Node _end)
    {
        // Traces the path
        if (_end.parent == null)
        {
            return null;
        }

        Node _currentNode = _end;
        List<Vector2Int> _path = new List<Vector2Int>();

        while (_currentNode != _start)
        {
            _path.Add(_currentNode.position);
            _currentNode = _currentNode.parent;
        }

        _path.Reverse();
        return _path;
    }


    private List<Cell> GetNeighbourCells(Cell _cell, Cell[,] _grid)
    {
        List<Cell> _neighbours = new List<Cell>();

        // checks the Neighbouring cells if they walls aren't up
        if (!_cell.HasWall(Wall.UP))
            _neighbours.Add(_grid[_cell.gridPosition.x, _cell.gridPosition.y + 1]);

        if (!_cell.HasWall(Wall.DOWN))
            _neighbours.Add(_grid[_cell.gridPosition.x, _cell.gridPosition.y - 1]);

        if (!_cell.HasWall(Wall.LEFT))
            _neighbours.Add(_grid[_cell.gridPosition.x - 1, _cell.gridPosition.y]);

        if (!_cell.HasWall(Wall.RIGHT))
            _neighbours.Add(_grid[_cell.gridPosition.x + 1, _cell.gridPosition.y]);

        return _neighbours;
    }

    int Heuristic(Vector2Int _startPos, Vector2Int _endPos) 
    {
        // Calculates the Heuristic from using the Manhattan Distance
        return Mathf.Abs(_startPos.x - _endPos.x) + Mathf.Abs(_startPos.y - _endPos.y);
    }

    
    /// <summary>
    /// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
    /// </summary>
    /// 
    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public float FScore { //GScore + HScore
            get { return GScore + HScore; }
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
}
