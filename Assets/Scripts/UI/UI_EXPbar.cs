using UnityEngine;
using UnityEngine.UI;

public class UI_EXPbar : MonoBehaviour
{
    [SerializeField]
    GameObject _player;
    [SerializeField]
    Slider _slider;

  
    private  PlayerStat _stat;
    
   

    void Start()
    {
        _player = GameObject.Find("UnityChan").gameObject;
        _slider = GameObject.Find("EXPBar").gameObject.GetComponent<Slider>();       
        _stat = _player.GetComponent<PlayerStat>();
        
    }

    void Update()
    {
        OnExpUpdate();

    }

    void OnExpUpdate()
    {
        //레벨업 후 exp 초기화는 playerstat 클래스에서 관리

        int level = _stat.Level;
        float nowexp = _stat.EXP;


        if (Managers.Data.StatDict.TryGetValue(level, out Data.Stat stats))
        {
            //TODO

        }
        float minustotalexp = Managers.Data.StatDict[level].totalexp; //딕셔너리 참조
        float totalexp = Managers.Data.StatDict[level + 1].totalexp; //딕셔너리 참조
        _slider.value = (float)(nowexp) / totalexp;

        if (nowexp == totalexp)
        {
            nowexp -= minustotalexp;
            _stat.EXP -= (int)minustotalexp;
            _slider.value = (float)(nowexp) / totalexp;
        }
    }
}
