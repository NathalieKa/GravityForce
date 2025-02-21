using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrintVersionMainMeni : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI versionText;
    void Start()
    {
        string version = Application.version;
        versionText.text = "Version: " + version;

    }

}
