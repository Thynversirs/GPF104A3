using UnityEngine;

public class NoteObjectUp : MonoBehaviour
{
    public float speed = 5f; //these are all just variants of NoteObject
    public KeyCode keyToPress;
    private bool canBePressed = false;

    private static NoteObjectUp first;

    void Start()
    {
        if (first == null || transform.position.x > first.transform.position.x)
            {
                first = this;
            }
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (first == null || transform.position.x > first.transform.position.x)
        {
            first = this;
        }


        if (Input.GetKeyDown(keyToPress))
        {
            if (first == this && canBePressed == true)
            {
                GameManager.instance.AddScore();
                Debug.Log("Hit");
                first = null;
                Destroy(gameObject);
            }
            if(first == this && canBePressed == false)
            {
                GameManager.instance.MissNote();    
                Debug.Log("Miss");
                first = null;
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Thingy"))
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Thingy"))
        {
            canBePressed = false;

            if (first == this)
                first = null;

            GameManager.instance.MissNote();
            Destroy(gameObject);
        }
    }
}
