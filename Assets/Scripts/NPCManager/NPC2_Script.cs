using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*GPT4Input.GetMouseButtonUp(0)�� �ܼ��� ���콺 ��ư�� ���ȴٰ� �������� ���� �̺�Ʈ�� �����մϴ�. 
 * �̴� UI ��ҿ��� �߻��� �̺�Ʈ����, �ƴϸ� �� ���� �������� �߻��� �̺�Ʈ������ ���� ������ ���� �����Ƿ�, UI â ���� 3D ������Ʈ�� Ŭ���Ǵ� ������ �߻��� �� �ֽ��ϴ�.
 * �ݸ鿡, IPointerClickHandler �������̽��� UI ��� �������� Ŭ�� �̺�Ʈ���� ó���մϴ�. 
 * �� �������̽��� �����ϸ�, UI ��� ������ ���콺 ��ư�� �����ٰ� ���� �̺�Ʈ�� �����ϰ� ó���� �� �ֽ��ϴ�. ���� UI â �������� Ŭ�� �̺�Ʈ�� UI â�� ���� ó���ǰ�, UI â ���� 3D ������Ʈ�� Ŭ������ �ʽ��ϴ�.
 * ��, Input.GetMouseButtonUp(0)�� IPointerClickHandler�� ���� ū ��������, 
 * ���ڴ� ��� Ŭ�� �̺�Ʈ�� �����ϴ� �ݸ�, ���ڴ� UI ��� �������� Ŭ�� �̺�Ʈ���� �����Ѵٴ� ���Դϴ�. 
 * �̷� ���� IPointerClickHandler�� ����ϸ� UI â ���� 3D ������Ʈ Ŭ���� ������ �� �ֽ��ϴ�.
 */

//�����ݰ�

public class NPC2_Script : MonoBehaviour , IPointerClickHandler
{
    private int _mask = (1 << (int)Define.Layer.NPC2);
    Texture2D _attackIcon;
    GameObject _player;
   
    void Awake()
    {
        _player = Managers.Resources.Load<GameObject>("PreFabs/UnityChan"); // �÷��̾� ���� 
        _attackIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/cursor(10)");
    }

 /*IPointer �������̽��� ����ϰ�, MainCamera Physics Raycaster�� Ȱ���ϸ� UI �� 3D������Ʈ�� �����ʰԲ� �Ҽ� �ִ�.*/
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

