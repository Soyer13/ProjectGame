using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject PlayerUiPanel;
    public GameObject DeadPanel;
    public GameObject ESCmenu;
    public GameObject DamagePanel;
    public TextMeshProUGUI Life;   
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

   
}
