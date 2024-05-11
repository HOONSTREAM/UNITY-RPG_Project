using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Scene_Number_Manager : MonoBehaviour
{
    private const int LOADING_SCENE_NUMBER = 0;
    private const int RUDENCIAN_SCENE_NUMBER = 1;
    private const int RUDENCIAN_SHOP_SCENE_NUMBER = 2;
    private const int RUDENCIAN_SOUTH_SCENE_NUMBER = 3;
    private const int LOGIN_SCENE_NUMBER = 4;
    private const int RUDENCIAN_BANK_SCENE_NUMBER = 5;
    private const int RUDENCIAN_JEWEL_SCENE_NUMBER = 7;
    private const int RUDENCIAN_INN_SCENE_NUMBER = 8;
    private const int RUDENCIAN_SOUTH_2_SCENE_NUMBER = 9;
    private const int RUDENCIAN_HOUSE_CHIEF_SCENE_NUMBER = 10;
    private const int START_SCENE_NUMBER = 11;


   public int Get_loading_scene()
    {
        return LOADING_SCENE_NUMBER;
    }
    public int Get_Rudencian_scene()
    {
        return RUDENCIAN_SCENE_NUMBER;
    }

    public int Get_Rudencian_shop()
    {
        return RUDENCIAN_SHOP_SCENE_NUMBER;
    }

    public int Get_Rudencian_south()
    {
        return RUDENCIAN_SOUTH_SCENE_NUMBER;
    }

    public int Get_Login_Scene()
    {
        return LOGIN_SCENE_NUMBER;
    }

    public int Get_Rudencian_bank_Scene()
    {
        return RUDENCIAN_BANK_SCENE_NUMBER;
    }

    public int Get_Rudencian_jewel_Scene()
    {
        return RUDENCIAN_JEWEL_SCENE_NUMBER;
    }

    public int Get_Rudencian_inn_Scene()
    {
        return RUDENCIAN_INN_SCENE_NUMBER;
    }

    public int Get_Rudencian_South2_Scene()
    {
        return RUDENCIAN_SOUTH_2_SCENE_NUMBER;
    }

    public int Get_Rudencian_House_chief_Scene()
    {
        return RUDENCIAN_HOUSE_CHIEF_SCENE_NUMBER;
    }

    public int Get_Start_Scene()
    {
        return START_SCENE_NUMBER;
    }
}
