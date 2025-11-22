using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float score = 0f;
    public TextMeshProUGUI scoreText;
    public Slider scoreSlider;
    public GameObject HitNote;
    public GameObject missNote;
    public Transform HitSpawnPoint;
    public Transform missSpawnPoint;
    public AudioSource musicSource;
    public float fadeDuration = 2f;
    public float fadeInDelay = 1f;
    public float aniCounterMiss = 0f;
    public float aniCounterHit = 0f; 
    public Animator anim;
    public Animator anim2;
    public GameObject DefeatUI;
    public GameObject VictoryUI;
    void Awake()
{
    if (instance == null)
    {
        instance = this;
    }
}
    private void Start()
    {
        UpdatescoreText();
        Debug.Log("Check1");
        StartCoroutine(gameOver(68f));
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void AddScore()
    {
        score += 2; //note: this is +2 because there is something causing missed notes to trigger twice and i cant find it
        UpdatescoreText();
        if(HitNote != null && HitSpawnPoint != null)
        {
           Instantiate(HitNote, HitSpawnPoint.position, HitSpawnPoint.rotation);
        }
        aniCounterHit += 1;
    }
    public void MissNote()
    {
        Debug.Log("Miss");
        score -= 1;
        aniCounterMiss += 1;
        if(missNote != null && missSpawnPoint != null)
        {
        Instantiate(missNote, missSpawnPoint.position, missSpawnPoint.rotation);
        }
        UpdatescoreText();
        StartCoroutine(SoundDip());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Check2");
            AddScore();
            UpdatescoreText();
        }

        if (aniCounterHit >= 5)
        {
            anim.SetTrigger("Wizards_hurt");
            Debug.Log("wiz hurt");
            aniCounterHit = 0;
            anim.SetTrigger("Wizards_hurt");
        }
        if (aniCounterMiss >= 10)
        {
            anim2.SetTrigger("Nerve_hurt");
            Debug.Log("playerhit");
            anim2.SetTrigger("Nerve_hurt");
            aniCounterMiss = 0;
        }
    }

    //public void AnimReset()
    //{
    //    anim2.SetTrigger("Nerve_hurt");
    //    aniCounterMiss = 0;
    //    aniCounterHit = 0;
    //    anim.SetTrigger("Wizards_hurt");
    //} imo, i didnt use my brain making this.


    private void UpdatescoreText()
    {
        scoreText.text = "Score: " + score;
        scoreSlider.value = score;
    }

    IEnumerator gameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(score >= 32)
        {
        VictoryUI.SetActive(true);
        anim2.SetTrigger("wizard_defeated");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        anim.SetTrigger("Wizard_down");
        }

        if(score <= 32)
        {
        DefeatUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }

    }
    
private IEnumerator SoundDip()
{
    float startVolume = musicSource.volume;
    float t = 0;
    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
        yield return null;
    }
    musicSource.volume = 0;
    yield return new WaitForSeconds(fadeInDelay); 
    t = 0;
    while (t < fadeDuration)
    {
        t += Time.deltaTime;
        musicSource.volume = Mathf.Lerp(0, 1f, t / fadeDuration);
        yield return null; 
    }
    musicSource.volume = 1f;
}
}
