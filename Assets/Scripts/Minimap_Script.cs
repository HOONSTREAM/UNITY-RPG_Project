using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_Script : MonoBehaviour
{
    [SerializeField]
    private Camera minicam;
    bool activeminimap = false;

    public void Open_Exit_Minimap()

    {
        Managers.Sound.Play("Inven_Open");
        activeminimap = !activeminimap;

        minicam.gameObject.SetActive(activeminimap);

        return;
    }

    


}
