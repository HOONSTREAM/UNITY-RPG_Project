using UnityEngine;
using UnityEngine.UI;

public class UI_EXPbar : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Slider _slider;
    private  PlayerStat _stat;
    
   

    void Start()
    {
        _player = Managers.Game.GetPlayer();
        _slider = GameObject.Find("EXPBar").gameObject.GetComponent<Slider>();       
        _stat = _player.GetComponent<PlayerStat>();
        
    }

    void Update()
    {
        OnExpUpdate();

    }

    void OnExpUpdate()
    {
        //������ �� exp �ʱ�ȭ�� playerstat Ŭ�������� ����

        int LEVEL = _stat.LEVEL;
        float nowexp = _stat.EXP;


        if (Managers.Data.StatDict.TryGetValue(LEVEL, out Data.Stat stats))
        {
          

        }
        float minustotalexp = Managers.Data.StatDict[LEVEL].totalexp; //��ųʸ� ����
        float totalexp = Managers.Data.StatDict[LEVEL + 1].totalexp; //��ųʸ� ����
        _slider.value = (float)(nowexp) / totalexp;

        if (nowexp == totalexp)
        {
            nowexp -= minustotalexp;
            _stat.EXP -= (int)minustotalexp;
            _slider.value = (float)(nowexp) / totalexp;
        }
    }
}
