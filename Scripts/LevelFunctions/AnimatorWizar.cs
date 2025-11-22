using UnityEngine;

public class AnimatorWizar : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //if (GameManager.aniCounterHit >= 5)
        {
            //anim.SetTrigger("Hurt");
            //GameManager.aniCounterHit = 0;
            //Debug.Log("wiz hurt");
        }
    }
}
