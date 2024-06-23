using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    enum CursorType
    {
        None,
        Hand,
        AttackHand,
        NPC,
    }

    CursorType cursorType = CursorType.None;

    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster | 1 << (int)Define.Layer.NPC | 1 << (int)Define.Layer.UI);
    [SerializeField]
    Texture2D _attackIcon;
    [SerializeField]
    Texture2D _handIcon;
    [SerializeField]
    Texture2D _NPCIcon;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        _attackIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/Cursor_Attack");
        _handIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/Cursor_Basic2");
        _NPCIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/Cursor_book");

        Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
        cursorType = CursorType.Hand;
    }
    void Update()
    {
        Mouse_Control();
    }

    private void Mouse_Control()
    {
        if (Input.GetMouseButton(0))
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (cursorType != CursorType.AttackHand)
                {
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    cursorType = CursorType.AttackHand;
                }

            }

            else if (hit.collider.gameObject.layer == (int)Define.Layer.Ground)
            {
                if (cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.Hand;
                }

            }

            else if (hit.collider.gameObject.layer == (int)Define.Layer.UI)
            {
                if (cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_NPCIcon, new Vector2(_NPCIcon.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.NPC;
                }

            }

            else
            {
                if (cursorType == CursorType.Hand)
                {
                    Cursor.SetCursor(_NPCIcon, new Vector2(_NPCIcon.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.NPC;
                }

            }
        }
    }
}
