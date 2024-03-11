using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color : MonoBehaviour
{
    public GameObject player, skin;
    public Material material1;
    public Button button;
    public Text texto;
    public string colorName;
    public bool buy;
    public int price;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ColorBtn);
    }

    // Update is called once per frame
    void ColorBtn()
    {
        if (player.gameObject.GetComponent<Player>().money >= price && buy == false)
        {
            skin.gameObject.GetComponent<SkinnedMeshRenderer>().material = material1;
            buy = true;
            player.gameObject.GetComponent<Player>().money = player.gameObject.GetComponent<Player>().money - price;
            player.gameObject.GetComponent<Player>().texto.text = "$: " + player.gameObject.GetComponent<Player>().money;
            texto.text = colorName;  
        }
        if (buy == true)
        {
            skin.gameObject.GetComponent<SkinnedMeshRenderer>().material = material1;
        }
    }
}
