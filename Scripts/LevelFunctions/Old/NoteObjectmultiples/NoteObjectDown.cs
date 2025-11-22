using System.Collections.Generic;
using UnityEngine;

public class NoteObjectDown : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode keyToPress; //this did not work at all

    private bool canBePressed = false;


    private static List<NoteObjectDown> activeNotes = new List<NoteObjectDown>();

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);


        HandleInput();
    }


private static void HandleInput()
{
    if (Input.anyKeyDown)
    {
        if (activeNotes.Count == 0)
        {
            GameManager.instance.MissNote();
            Debug.Log("Miss");
            return;
        }

        foreach (var note in activeNotes)
        {
            if (Input.GetKeyDown(note.keyToPress))
            {
                if (note.canBePressed)
                {
                    GameManager.instance.AddScore();
                    Debug.Log("Hit");
                    note.DestroyNote();
                }
                else
                {
                    GameManager.instance.MissNote();
                    Debug.Log("Miss1");
                    note.DestroyNote();
                }


                return;
            }
        }
    }
}


    private void DestroyNote()
    {
        RemoveFromActiveNotes();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Thingy"))
        {
            canBePressed = true;
            if (!activeNotes.Contains(this))
                activeNotes.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Thingy"))
        {
            canBePressed = false;

            if (IsFirstNote())
            {

                GameManager.instance.MissNote();
                Debug.Log("Miss2");
                DestroyNote();
            }
            else
            {
                RemoveFromActiveNotes();
            }
        }
    }

    private bool IsFirstNote()
    {
        return activeNotes.Count > 0 && activeNotes[0] == this;
    }

    private void RemoveFromActiveNotes()
    {
        activeNotes.Remove(this);
    }
}
