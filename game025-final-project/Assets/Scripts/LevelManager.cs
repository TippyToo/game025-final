using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int coins;

    public Text coinDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinDisplay.text = coins.ToString();
        if (Input.GetKeyDown(KeyCode.Equals)) { addCoins(1); }
        if (Input.GetKeyDown(KeyCode.Minus)) { addCoins(-1); }
    }

    public void addCoins(int count)
    {
        coins += count;
    }
}
