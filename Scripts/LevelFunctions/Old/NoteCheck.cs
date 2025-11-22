using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
public class NoteCheck : MonoBehaviour
{
    private bool noteAbove = false;
    public float Score = 0f;
    public TextMeshProUGUI ScoreText;

    private void Start()
    {
        UpdateScoreText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            noteAbove = true;
            Debug.Log("NoteAbove");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (noteAbove)
            {
                Score += 100;
                Debug.Log("+100");
            }
            else
            {
                Score -= 50;
                Debug.Log("-50");
            }

            UpdateScoreText();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            noteAbove = false;
            Debug.Log("Note gone");
        }
    }
    private void UpdateScoreText()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + Score;
        }
    }
}
