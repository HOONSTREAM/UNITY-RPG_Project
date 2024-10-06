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
    private const int RUDENCIAN_DEEP_PLACE = 12;
    private const int RUDENCIAN_ROOKISS_ROOM = 13;
    private const int EPILENIA_MAIN_SCENE_NUMBER = 14;
    private const int EPILENIA_BANK_SCENE_NUMBER = 15;

    public int LoadingScene => LOADING_SCENE_NUMBER;
    public int RudencianScene => RUDENCIAN_SCENE_NUMBER;
    public int RudencianShop => RUDENCIAN_SHOP_SCENE_NUMBER;
    public int RudencianSouth => RUDENCIAN_SOUTH_SCENE_NUMBER;
    public int LoginScene => LOGIN_SCENE_NUMBER;
    public int RudencianBankScene => RUDENCIAN_BANK_SCENE_NUMBER;
    public int RudencianJewelScene => RUDENCIAN_JEWEL_SCENE_NUMBER;
    public int RudencianInnScene => RUDENCIAN_INN_SCENE_NUMBER;
    public int RudencianSouth2Scene => RUDENCIAN_SOUTH_2_SCENE_NUMBER;
    public int RudencianHouseChiefScene => RUDENCIAN_HOUSE_CHIEF_SCENE_NUMBER;
    public int StartScene => START_SCENE_NUMBER;
    public int DeepPlaceScene => RUDENCIAN_DEEP_PLACE;
    public int RooKissRoomScene => RUDENCIAN_ROOKISS_ROOM;
    public int EpileniaMainScene => EPILENIA_MAIN_SCENE_NUMBER;
    public int EpileniaBankScene => EPILENIA_BANK_SCENE_NUMBER;
}