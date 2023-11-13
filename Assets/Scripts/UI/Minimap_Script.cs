using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; //���� �� �������� 

public class Minimap_Script : MonoBehaviour
{
    [SerializeField]
    private Camera minicam;
    [SerializeField]
    private GameObject player;
    bool activeminimap = false;
    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster); //���̾��ũ
    public TextMeshProUGUI scenename; 
    private string scene_name;


    private void Start()
    {
        player = GameObject.Find("UnityChan").gameObject;
    }

    public void Open_Exit_Minimap()

    {

        Scene scene = SceneManager.GetActiveScene(); //���� �� ��������  
        scene_name = scene.name; //���� �� �������� 

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

            Debug.Log("�̴ϸ��� Ŭ���Ͽ� ������ ��ǥ�� �̵��մϴ�.");

            RaycastHit hit;
            Ray ray = minicam.ScreenPointToRay(Input.mousePosition);
            Debug.Log(Input.mousePosition);

            bool raycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);

            Debug.DrawRay(minicam.transform.position, ray.direction * 100.0f, Color.red, 1.0f);



        }
    }




}
