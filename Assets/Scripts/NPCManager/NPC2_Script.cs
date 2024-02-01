using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*GPT4Input.GetMouseButtonUp(0)은 단순히 마우스 버튼이 눌렸다가 떼어졌을 때의 이벤트를 감지합니다. 
 * 이는 UI 요소에서 발생한 이벤트인지, 아니면 그 외의 공간에서 발생한 이벤트인지에 대한 구분을 하지 않으므로, UI 창 뒤의 3D 오브젝트가 클릭되는 문제가 발생할 수 있습니다.
 * 반면에, IPointerClickHandler 인터페이스는 UI 요소 위에서의 클릭 이벤트만을 처리합니다. 
 * 이 인터페이스를 구현하면, UI 요소 위에서 마우스 버튼을 눌렀다가 떼는 이벤트를 감지하고 처리할 수 있습니다. 따라서 UI 창 위에서의 클릭 이벤트는 UI 창에 의해 처리되고, UI 창 뒤의 3D 오브젝트는 클릭되지 않습니다.
 * 즉, Input.GetMouseButtonUp(0)과 IPointerClickHandler의 가장 큰 차이점은, 
 * 전자는 모든 클릭 이벤트를 감지하는 반면, 후자는 UI 요소 위에서의 클릭 이벤트만을 감지한다는 점입니다. 
 * 이로 인해 IPointerClickHandler를 사용하면 UI 창 뒤의 3D 오브젝트 클릭을 방지할 수 있습니다.
 */

//나무금고

public class NPC2_Script : MonoBehaviour , IPointerClickHandler
{
    private int _mask = (1 << (int)Define.Layer.NPC2);
    Texture2D _attackIcon;
    GameObject _player;
   
    void Awake()
    {
        _player = Managers.Resources.Load<GameObject>("PreFabs/UnityChan"); // 플레이어 세팅 
        _attackIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/cursor(10)");
    }

 /*IPointer 인터페이스를 사용하고, MainCamera Physics Raycaster를 활용하면 UI 뒤 3D오브젝트를 뚫지않게끔 할수 있다.*/
    public void OnPointerClick(PointerEventData eventData)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.NPC2)
            {

                PlayerController pc = _player.GetComponent<PlayerController>();
                pc.State = Define.State.Idle;             

                GameObject go = GameObject.Find("Storage CANVAS").gameObject;
                Storage_Script storage = go.GetComponent<Storage_Script>();
                storage.Enter();

            }

        }
    }
}

