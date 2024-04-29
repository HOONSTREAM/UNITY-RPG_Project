using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{

    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,

    }
    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

public enum Layer
    {
        Default = 0,
        UI = 5,
        Monster = 8,
        Ground = 9,
        Block = 10,
        NPC = 12,
        NPC1 = 13,
        NPC2 = 14,
        Console = 15,
        NPC3 = 16,
        FieldItem = 17,
        NPC4 = 18,
        NPC5 = 19,
        NPC6 = 20,
        


    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Rudencian,
        Rudencian_shop,
        Rudencian_South,
        Rudencian_Bank,
        Rudencian_Inn,
        Rudencian_jewel_shop,
        Rudencian_chief_house,



    }

    public enum Sound
    {
        Bgm,
        Effect,
        Ambiance,
        Maxcount 

    }

    public enum UIEvent
    {
        Click,
        Drag,

    }
    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
    public enum CameraMode
    {
        QuarterView,

    }
  
}
