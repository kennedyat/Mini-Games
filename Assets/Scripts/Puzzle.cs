using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null; 
    private Camera _camera;
    [SerializeField] private tilescript[] tiles = null;
    private int emptySpaceIndex = 15;
    private bool _isFinished;
    [SerializeField] private GameObject endPanel = null, newRecordText = null;
    [SerializeField] private Text endPanelTimeText = null, bestTimeText = null;


    void Start()
    {
        _camera = Camera.main;
        shuffle();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(a: emptySpace.position, b: hit.transform.position) < 2)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    tilescript thisTile = hit.transform.GetComponent<tilescript>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        if (!_isFinished)
        {
            int correctTiles = 0;
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.inRightPlace)
                        correctTiles++;
                }
            }
            if (correctTiles == tiles.Length - 1)
            {
                _isFinished = true;
                endPanel.SetActive(true);
                var a = GetComponent<Timer>();
                a.stopTimer();
                endPanelTimeText.text = (a.minutes < 10? "0" : "") + a.minutes + ":" + (a.seconds < 10? "0" : "") + a.seconds;
                int bestTime;
                if (PlayerPrefs.HasKey("bestTime"))
                {
                    bestTime = PlayerPrefs.GetInt(key: "bestTime");
                }
                else
                {
                    bestTime = 999999;
                }
                
                int playerTime = a.minutes * 60 + a.seconds;
                if (playerTime < bestTime)
                {
                    newRecordText.SetActive(true);
                    PlayerPrefs.SetInt("bestTime", playerTime);
                }
                else
                {
                    int minutes = bestTime / 60;
                    int seconds = bestTime - minutes*60;
                    bestTimeText.text = (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
                    bestTimeText.transform.parent.gameObject.SetActive(true);
                }
            }
        }
    }
    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void shuffle()
    {
        if (emptySpaceIndex != 15)
        {
            var tileOn15LastPos = tiles[15].targetPosition;
            tiles[15].targetPosition = emptySpace.position;
            emptySpace.position = tileOn15LastPos;
            tiles[emptySpaceIndex] = tiles[15];
            tiles[15] = null;
            emptySpaceIndex = 15;
        }
        int invertion;
        do
        {
            for (int i = 0; 1 <= 14; i++)
            {
                if (tiles[i] != null)
                {
                    var lastPos = tiles[i].targetPosition;
                    int randomIndex = Random.Range(0, 14);
                    tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                    tiles[randomIndex].targetPosition = lastPos;
                    var tile = tiles[i];
                    tiles[i] = tiles[randomIndex];
                    tiles[randomIndex] = tile;
                }
            }
            invertion = GetInversions();
            Debug.Log(message: "Puzzle Shuffled");
        } while (invertion % 2 !=0);
    }

    public int findIndex(tilescript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }
}
