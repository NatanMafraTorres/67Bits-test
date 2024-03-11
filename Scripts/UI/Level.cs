using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public GameObject player;
    public Button button;
    public Text textPrice, textLvlGm;
    public int levelNumber, maxLevel, price;

    void Start()
    {
        button.onClick.AddListener(LevelBtn);
    }

    void LevelBtn()
    {
        if (player.gameObject.GetComponent<Player>().money >= price && levelNumber < maxLevel - 1)
        {
            levelNumber = levelNumber + 1;
            player.gameObject.GetComponent<Player>().money = player.gameObject.GetComponent<Player>().money - price;
            player.gameObject.GetComponent<Player>().texto.text = "$: " + player.gameObject.GetComponent<Player>().money;
            player.gameObject.GetComponent<Player>().bMax = player.gameObject.GetComponent<Player>().bMax + 1;
            textLvlGm.text = "Level: " + levelNumber;
        }
        if (player.gameObject.GetComponent<Player>().money >= price && levelNumber == maxLevel -1)
        {
            levelNumber = levelNumber + 1;
            player.gameObject.GetComponent<Player>().money = player.gameObject.GetComponent<Player>().money - price;
            player.gameObject.GetComponent<Player>().texto.text = "$: " + player.gameObject.GetComponent<Player>().money;
            player.gameObject.GetComponent<Player>().bMax = player.gameObject.GetComponent<Player>().bMax + 1;
            textLvlGm.text = "Level: MAX";
            textPrice.text = "Level: MAX";
        }
    }
}
