using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManualData : MonoBehaviour
{
    public string[] enemyNames;

    public string[] enemyDescriptions;

    public string[] enemyCounters;

    public Text nameText;

    public Text descText;

    public Text counterText;

    public int pageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nameText.text = enemyNames[pageIndex];
        descText.text = enemyDescriptions[pageIndex];
        counterText.text = enemyCounters[pageIndex];
    }

    public void ButtonInput(int input)
    {
        if (pageIndex + input < enemyNames.Length && pageIndex + input > -1)
        {
            pageIndex += input;
        }
    }
}
