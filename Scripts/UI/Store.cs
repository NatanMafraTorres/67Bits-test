using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public Button button;
    public GameObject store;
    public bool active;
    public Text texto;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(StoreBtn);
    }


    public void StoreBtn()
    {
        if (active == true)
        {
            store.SetActive(false);
            active = false;
            texto.text = "Store";
        }
        else
        {
            store.SetActive(true);
            active = true;
            texto.text = "Return";
        }
    }
}
