using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    [Header("Board Info")]
    public Sprite squareSprite;
    public Color pressedSquareColor;
    public Color unPressedSquareColor;

    [Range(0.05f,0.5f)]
    [Header("Line Gaps Info")]
    public float lineWidthBetweenSquares = 0.1f;
    public Material linesMaterials;
    public float LineGapsOffset = 0.5f;

    [Header("Player Info")]
    public GameObject PlayerText;
    public TMPro.TextMeshProUGUI winText;

    private Camera _cam;
    private GameObject _canvas;
    private GameObject _restartButton;

    void Start()
    {
        _cam = Camera.main;
        _canvas = GameObject.FindGameObjectWithTag("canvas");
        for(int i =0; i< _canvas.transform.childCount; i++)
        {
            if (_canvas.transform.GetChild(i).CompareTag("restartbutton"))
            {
                _restartButton = _canvas.transform.GetChild(i).gameObject;
            }
        }
        CreateBoard();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
    }
    private void MouseDown()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            ClickSquare(hit);
        }

    }
    public void ClickSquare(RaycastHit2D hit)
    {
        if (BoardState.isGameOver) return;
        if (!BoardState.isGameStarted) BoardState.isGameStarted = true;
        Vector2 squarePos = hit.collider.gameObject.transform.position;
        Vector2 squarePosInSquares = squarePos + new Vector2(1, 1);
        Square square = BoardState.squares.Find((square) => square.squarePos == squarePosInSquares);
        GameObject squareObj = hit.collider.gameObject;
        if (square.isPressed) return;
        square.isPressed = true;
        GameObject playerText = Instantiate(PlayerText, new Vector3(0,0,0), Quaternion.identity);
        playerText.transform.SetParent(_canvas.transform);
        playerText.GetComponent<RectTransform>().transform.position = (squarePos * 180) + _canvas.transform.position.vect3To2();
        playerText.GetComponent<TMPro.TextMeshProUGUI>().text = BoardState.isXTurn ? "X" : "O";
        playerText.transform.tag = "destroy";
        square.state = BoardState.isXTurn ? "X" : "O";
        winText.text = (BoardState.isXTurn ? "O" : "X") + " Turn";
        BoardState.isXTurn = !BoardState.isXTurn;
        Destroy(squareObj);
        DrawSquare(square);
        if (isGameOver())
        {
            BoardState.isGameOver = true;
            winText.text = (BoardState.isXTurn ? "O" : "X") + " is The Winner!";
            _restartButton.SetActive(true);
        }
    }
    public void CreateBoard()
    {
        for (int i = 0; i < BoardState.SIZE; i++)
        {
            for (int j = 0; j < BoardState.SIZE; j++)
            {
                Square square = new Square(isPressed: false,
                                           squarePos: new Vector2(i, j));
                DrawSquare(square);
                
            }
        }
        winText.text = (BoardState.isXTurn ? "X" : "O") + " Turn";
        DrawGapLines();
    }
    public void DrawSquare(Square square)
    {
        if(BoardState.squares.Contains(square))
        {
            BoardState.squares.Insert(BoardState.squares.FindIndex(s => s.squarePos == square.squarePos)+1, square);
            BoardState.squares.RemoveAt(BoardState.squares.FindIndex(s => s.squarePos == square.squarePos));
        }
        else
        {
            BoardState.squares.Add(square);
        }
        GameObject squareObj = new GameObject();
        squareObj.AddComponent<SpriteRenderer>();
        squareObj.GetComponent<SpriteRenderer>().sprite = squareSprite;
        squareObj.GetComponent<SpriteRenderer>().color = square.isPressed ? pressedSquareColor : unPressedSquareColor;
        squareObj.AddComponent<BoxCollider2D>();
        squareObj.transform.SetParent(transform);
        squareObj.transform.position = square.squarePos - new Vector2(1, 1);
        squareObj.name = "Square" + square.squarePos;

    }
    public void DrawGapLines()
    {
        // (1,0) -> (1,3)
        GameObject obj1 = new GameObject();
        obj1.name = "line1";
        var line1 = obj1.AddComponent<LineRenderer>();
        line1.material = linesMaterials;
        line1.positionCount = 2;
        line1.SetPosition(0,new Vector3(1 - LineGapsOffset, 0 - LineGapsOffset, 0));
        line1.SetPosition(1, new Vector3(1 - LineGapsOffset, 3 - LineGapsOffset, 0));
        line1.startWidth = lineWidthBetweenSquares;
        line1.endWidth = lineWidthBetweenSquares;
        obj1.transform.SetParent(transform);
        // (2,0) -> (2,3)
        GameObject obj2 = new GameObject();
        obj2.name = "line2";
        var line2 = obj2.AddComponent<LineRenderer>();
        line2.material = linesMaterials;
        line2.positionCount = 2;
        line2.SetPosition(0, new Vector3(2- LineGapsOffset, 0 - LineGapsOffset, 0));
        line2.SetPosition(1, new Vector3(2 - LineGapsOffset, 3 - LineGapsOffset, 0));
        line2.startWidth = lineWidthBetweenSquares;
        line2.endWidth = lineWidthBetweenSquares;
        obj2.transform.SetParent(transform);
        // (0,1) -> (3,1)
        GameObject obj3 = new GameObject();
        obj3.name = "line3";
        var line3 = obj3.AddComponent<LineRenderer>();
        line3.material = linesMaterials;
        line3.positionCount = 2;
        line3.SetPosition(0, new Vector3(0 - LineGapsOffset, 1 - LineGapsOffset, 0));
        line3.SetPosition(1, new Vector3(3 - LineGapsOffset, 1 - LineGapsOffset, 0));
        line3.startWidth = lineWidthBetweenSquares;
        line3.endWidth = lineWidthBetweenSquares;
        obj3.transform.SetParent(transform);
        // (0,2) -> (3,2)
        GameObject obj4 = new GameObject();
        obj4.name = "line4";
        var line4 = obj4.AddComponent<LineRenderer>();
        line4.material = linesMaterials;
        line4.positionCount = 2;
        line4.SetPosition(0, new Vector3(0 - LineGapsOffset, 2 - LineGapsOffset, 0));
        line4.SetPosition(1, new Vector3(3 - LineGapsOffset, 2 - LineGapsOffset, 0));
        line4.startWidth = lineWidthBetweenSquares;
        line4.endWidth = lineWidthBetweenSquares;
        obj4.transform.SetParent(transform);
    }
    public bool isGameOver()
    {
        // Board
        // 2 5 8
        // 1 4 7
        // 0 3 6
        List<int[]> winPositions = new List<int[]> { 
            new[] { 0, 1, 2 }, 
            new[] { 3, 4, 5}, 
            new[] { 6, 7, 8 },
            new[] { 2, 5, 8 },
            new[] { 1, 4, 7 },
            new[] { 0, 3, 6 },
            new[] { 2, 4, 6 },
            new[] { 0, 4, 8 },};


        foreach (int[] pos in winPositions)
        {
            // if the line is field check if it is the same state
            if(BoardState.squares[pos[0]].state != null && BoardState.squares[pos[1]].state != null && BoardState.squares[pos[2]].state != null)
            {
                if(BoardState.squares[pos[0]].state == BoardState.squares[pos[1]].state && BoardState.squares[pos[0]].state == BoardState.squares[pos[2]].state)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void RestartGame()
    {
        BoardState.isGameOver = false;
        winText.text = "";
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < _canvas.transform.childCount; i++)
        {
            if(_canvas.transform.GetChild(i).gameObject.CompareTag("destroy"))
            {
                Destroy(_canvas.transform.GetChild(i).gameObject);
            }
        }
        BoardState.isXTurn = true;
        BoardState.isGameStarted = true;
        BoardState.squares = new List<Square>();

        _restartButton.SetActive(false);
        CreateBoard();
    }
}



