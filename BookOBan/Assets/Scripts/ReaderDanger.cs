using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReaderDanger : MonoBehaviour
{
    public int minTime = 60;
    public int maxTime = 120;

    [SerializeField] private float timer = 0;

    public GameObject UI;
    public GameObject button;
    public GameObject screentext;

    public int maxHealth = 10;
    private float health;
    public float regen = 3;
    public GameObject healthBar;
    public GameObject blackoutpanel;


    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(minTime, maxTime);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            UI.SetActive(true);
            button.SetActive(true);
            timer = Random.Range(minTime, maxTime);
        }

        if (UI.activeSelf)
        {
            health -= Time.deltaTime;
        }
        else if(health < maxHealth)
        {
            health += Time.deltaTime * regen;
        }
        healthBar.transform.localScale = new Vector3(295 * (health/maxHealth), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        blackoutpanel.SetActive(UI.activeSelf);
        screentext.SetActive(!UI.activeSelf);

        if (health <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
