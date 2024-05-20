using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;

    private string Keyname = "Player";
    private string filename = "SaveFile.es3";
    private void Start()
    {
        Player = Managers.Game.GetPlayer();
    }

    public void Save()
    {
        ES3AutoSaveMgr.Current.Save();  
    }

    public void Load()
    {
        if (ES3.FileExists(filename))
        {
            ES3AutoSaveMgr.Current.Load();
        }

        else
        {
            Save();
        }
    }


}
