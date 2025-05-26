using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectFour : MonoBehaviour
{
    private const int Rows = 6;
    private const int Columns = 9;
    private int[,] board = new int[Rows, Columns];
    private int currentPlayer = 1;
    private bool gameOver = false;

    public Image[] gridCells;
    public Button[] columnButtons;
    public TextMeshProUGUI statusText;
    public Image turnIndicator; // New field for turn indicator
    public Color emptyColor;
    public Color redColor = Color.red;
    public Color yellowColor = Color.yellow;

    void Start()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                board[row, col] = 0;
            }
        }

        for (int col = 0; col < Columns; col++)
        {
            int column = col;
            columnButtons[col].onClick.AddListener(() => OnColumnButtonClick(column));
        }

        UpdateBoardUI();
        UpdateStatusText();
    }

    void OnColumnButtonClick(int col)
    {
        if (gameOver) return;

        int row = -1;
        for (int r = Rows - 1; r >= 0; r--)
        {
            if (board[r, col] == 0)
            {
                row = r;
                break;
            }
        }

        if (row == -1)
        {
            statusText.text = "Column full! Try another.";
            statusText.color = Color.white;
            turnIndicator.color = currentPlayer == 1 ? redColor : yellowColor; // Keep turn indicator
            return;
        }

        board[row, col] = currentPlayer;
        UpdateBoardUI();

        if (CheckWin(row, col))
        {
            statusText.text = $"Player {(currentPlayer == 1 ? "Red" : "Yellow")} Wins!";
            statusText.color = currentPlayer == 1 ? redColor : yellowColor;
            turnIndicator.color = currentPlayer == 1 ? redColor : yellowColor;
            gameOver = true;
            DisableButtons();
            return;
        }
        else if (IsBoardFull())
        {
            statusText.text = "Game Draw!";
            statusText.color = Color.white;
            turnIndicator.color = Color.gray; // Neutral for draw
            gameOver = true;
            DisableButtons();
            return;
        }

        currentPlayer = currentPlayer == 1 ? 2 : 1;
        UpdateStatusText();
    }

    void UpdateBoardUI()
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                int index = row * Columns + col;
                if (index < gridCells.Length)
                {
                    Image cell = gridCells[index];
                    cell.color = board[row, col] switch
                    {
                        0 => emptyColor,
                        1 => redColor,
                        2 => yellowColor,
                        _ => emptyColor
                    };
                }
            }
        }
    }

    void UpdateStatusText()
    {
        statusText.text = $"Player {(currentPlayer == 1 ? "Red" : "Yellow")}'s Turn";
        statusText.color = currentPlayer == 1 ? redColor : yellowColor;
        if (turnIndicator != null)
        {
            turnIndicator.color = currentPlayer == 1 ? redColor : yellowColor;
        }
    }

    bool CheckWin(int row, int col)
    {
        int player = board[row, col];
        int count;

        // Horizontal
        count = 0;
        for (int c = 0; c < Columns; c++)
        {
            if (board[row, c] == player) count++;
            else count = 0;
            if (count >= 4) return true;
        }

        // Vertical
        count = 0;
        for (int r = 0; r < Rows; r++)
        {
            if (board[r, col] == player) count++;
            else count = 0;
            if (count >= 4) return true;
        }

        // Diagonal (top-left to bottom-right)
        count = 0;
        int startRow = row - Mathf.Min(row, col);
        int startCol = col - Mathf.Min(row, col);
        while (startRow < Rows && startCol < Columns)
        {
            if (board[startRow, startCol] == player) count++;
            else count = 0;
            if (count >= 4) return true;
            startRow++;
            startCol++;
        }

        // Diagonal (top-right to bottom-left)
        count = 0;
        startRow = row - Mathf.Min(row, Columns - 1 - col);
        startCol = col + Mathf.Min(row, Columns - 1 - col);
        while (startRow < Rows && startCol >= 0)
        {
            if (board[startRow, startCol] == player) count++;
            else count = 0;
            if (count >= 4) return true;
            startRow++;
            startCol--;
        }

        return false;
    }

    bool IsBoardFull()
    {
        for (int col = 0; col < Columns; col++)
        {
            if (board[0, col] == 0) return false;
        }
        return true;
    }

    void DisableButtons()
    {
        foreach (Button btn in columnButtons)
        {
            btn.interactable = false;
        }
    }
}