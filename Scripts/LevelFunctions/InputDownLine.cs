using System.Collections.Generic;
using UnityEngine;

public class InputDownLine : MonoBehaviour
{
    public KeyCode keyToPress;

    private List<GameObject> notesInBox = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (notesInBox.Count > 0)
            {
                GameObject nearNote = FindNearNote(notesInBox);
                if (nearNote != null)
                {
                    GameManager.instance.AddScore();
                    Debug.Log("Hit");
                    notesInBox.Remove(nearNote);
                    Destroy(nearNote);
                }
            }
            else
            {
                GameObject nearNote = FindNearNoteOutsideBox();
                if (nearNote != null)
                {
                    GameManager.instance.MissNote();
                    Debug.Log("Miss");
                    Destroy(nearNote);
                }
                else
                {
                    GameManager.instance.MissNote();
                                        Debug.Log("Miss");
                }
            }
        }
    }

    private GameObject FindNearNote(List<GameObject> noteList)
    {
        GameObject nearNote = null;
        float minDistance = Mathf.Infinity; //prolly should be smaller, doesnt matter enough for me to change it
        Vector3 currentPosition = transform.position;

        foreach (GameObject note in noteList)
        {
            if (note == null) continue;

            float distance = Vector3.Distance(note.transform.position, currentPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearNote = note;
            }
        }

        return nearNote;
    }

    private GameObject FindNearNoteOutsideBox()
    {
        GameObject[] allNotes = GameObject.FindGameObjectsWithTag("Note");
        if (allNotes.Length == 0) return null;
        List<GameObject> notesOutsideBox = new List<GameObject>();
        foreach (GameObject note in allNotes)
        {
            if (!notesInBox.Contains(note))
            {
                notesOutsideBox.Add(note);
            }
        }
        if (notesOutsideBox.Count == 0)
        {
            notesOutsideBox.AddRange(allNotes);
        }

        return FindNearNote(notesOutsideBox);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            if (!notesInBox.Contains(other.gameObject))
            {
                notesInBox.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
        if (notesInBox.Contains(other.gameObject))
        {
            notesInBox.Remove(other.gameObject);
        }
        }
    }

}
