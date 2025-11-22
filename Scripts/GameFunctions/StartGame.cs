using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "LevelSelector";

    public void OnStartPressed()
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("working");
    }
}
