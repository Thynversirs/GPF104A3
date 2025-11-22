using System.Collections.Generic;
using UnityEngine;

public class inputLineUp : MonoBehaviour
{
    public KeyCode keyToPress;

    private List<GameObject> notesInTrigger = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (notesInTrigger.Count > 0)
            {
                GameObject nearestNote = FindNearestNote(notesInTrigger);
                if (nearestNote != null)
                {
                    GameManager.instance.AddScore();
                    Debug.Log("Hit");
                    notesInTrigger.Remove(nearestNote);
                    Destroy(nearestNote);
                }
            }
            else
            {
                GameObject nearestNote = FindNearestNoteOutsideTrigger();
                if (nearestNote != null)
                {
                    GameManager.instance.MissNote();
                    Debug.Log("Miss");
                    Destroy(nearestNote);
                }
                else
                {
                    GameManager.instance.MissNote();
                                        Debug.Log("Miss");
                }
            }
        }
    }

    private GameObject FindNearestNote(List<GameObject> noteList)
    {
        GameObject nearestNote = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject note in noteList)
        {
            if (note == null) continue;

            float distance = Vector3.Distance(note.transform.position, currentPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNote = note;
            }
        }

        return nearestNote;
    }

    private GameObject FindNearestNoteOutsideTrigger()
    {
        GameObject[] allNotes = GameObject.FindGameObjectsWithTag("NoteU");
        if (allNotes.Length == 0) return null;
        List<GameObject> notesOutsideTrigger = new List<GameObject>();
        foreach (GameObject note in allNotes)
        {
            if (!notesInTrigger.Contains(note))
            {
                notesOutsideTrigger.Add(note);
            }
        }
        if (notesOutsideTrigger.Count == 0)
        {
            notesOutsideTrigger.AddRange(allNotes);
        }

        return FindNearestNote(notesOutsideTrigger);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NoteU"))
        {
            if (!notesInTrigger.Contains(other.gameObject))
            {
                notesInTrigger.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NoteU"))
        {
        if (notesInTrigger.Contains(other.gameObject))
        {
            notesInTrigger.Remove(other.gameObject);
        }
        }
    }

}
