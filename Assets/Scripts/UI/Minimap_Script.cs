using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; //현재 씬 가져오기 

public class Minimap_Script : MonoBehaviour
{
    [SerializeField]
    private Camera minicam;
    [SerializeField]
    private GameObject player;
    bool activeminimap = false;
    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster); //레이어마스크
    public TextMeshProUGUI scenename; 
    private string scene_name;


    private void Start()
    {
        player = GameObject.Find("UnityChan").gameObject;
    }

    public void Open_Exit_Minimap()

    {

        Scene scene = SceneManager.GetActiveScene(); //현재 씬 가져오기  
        scene_name = scene.name; //현재 씬 가져오기 

        Managers.Sound.Play("Inven_Open");
        activeminimap = !activeminimap;
        scenename.text = scene_name;
        minicam.gameObject.SetActive(activeminimap);

        return;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Debug.Log("미니맵을 클릭하여 지정된 좌표로 이동합니다.");

            RaycastHit hit;
            Ray ray = minicam.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition);

            bool raycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);

            Debug.DrawRay(minicam.transform.position, ray.direction * 100.0f, Color.red, 1.0f);



        }
    }




}
