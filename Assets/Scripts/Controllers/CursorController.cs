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

    }

    CursorType cursorType = CursorType.None;

    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster | 1 << (int)Define.Layer.NPC);
    Texture2D _attackIcon;
    Texture2D _handIcon;
    Texture2D _NPCIcon;

    void Start()
    {
        _attackIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/cursor(12)");
        _handIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/cursor(1)");
        _NPCIcon = Managers.Resources.Load<Texture2D>("Textures/Cursor/cursor(10)");

    }


    void Update()
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
            else 
            {
                Cursor.SetCursor(_NPCIcon, new Vector2(_NPCIcon.width / 3, 0), CursorMode.Auto);

            }
        }
    }
}
