using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform[] spawnPoints; //note, this was made originally for all four lanes to work off this, i was lazy and reporpused
    public float[] noteTimes; // it to be one lane when i found the four together to create issues
    public KeyCode[] keys;

    private int noteIndex = 0;
    private float songTime;

    public AudioSource musicSource;



    void Update()
    {
        songTime = musicSource.time;

        if (noteIndex < noteTimes.Length && songTime >= noteTimes[noteIndex])
        {
            SpawnNote(noteIndex);
            noteIndex++;
        }
    }

    void SpawnNote(int index)
    {
        int lane = index % spawnPoints.Length;
        GameObject note = Instantiate(notePrefab, spawnPoints[lane].position, Quaternion.identity);
    }
}
