using UnityEngine;


public class LevelSelect : MonoBehaviour
{
    public int Level = 1;
    public Transform[] levelPositions;
    public Transform playerIcon;
    public float moveSpeed = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Level++;
            Level = Mathf.Clamp(Level, 0, levelPositions.Length - 1);
            UpdateIconPosition();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Level--;
            Level = Mathf.Clamp(Level, 0, levelPositions.Length - 1);
            UpdateIconPosition();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            LoadSelectedLevel();
        }


        if (levelPositions != null && Level < levelPositions.Length)
        {
            playerIcon.position = Vector3.Lerp
            (
                playerIcon.position,
                levelPositions[Level].position,
                Time.deltaTime * moveSpeed
            );
        }
    }

    void UpdateIconPosition()
    {
        if (levelPositions.Length > 0 && Level < levelPositions.Length)
        {
            Debug.Log("Selected Level: " + Level);
        }
    }

    void LoadSelectedLevel()
    {
        string sceneName = "Level" + Level;
        Debug.Log("Loading scene: " + sceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
