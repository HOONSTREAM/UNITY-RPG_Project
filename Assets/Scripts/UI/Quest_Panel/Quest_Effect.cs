using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Unity의 ScriptableObject는 데이터를 저장하고 관리하는 데 사용되는 클래스입니다. 
/// 주로 게임 개발에서 게임 오브젝트의 상태나 설정을 관리하는 데 활용됩니다. 
/// ScriptableObject는 Unity에서 제공하는 기본 MonoBehaviour와는 달리, 게임 오브젝트에 부착될 필요 없이 독립적으로 데이터만을 담는 오브젝트입니다. 
/// 이를 통해 게임의 데이터 구조를 더 효율적으로 관리하고 재사용할 수 있습니다.
/// </summary>
public abstract class Quest_Effect : ScriptableObject //추상클래스
    {
        public abstract bool ExecuteRole(QuestType questtype);

    }

