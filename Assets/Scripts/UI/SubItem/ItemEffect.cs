using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject //추상클래스
{
    public abstract bool ExecuteRole(ItemType itemtype);
       
}



/*Scriptable Object는 대량의 데이터를 저장하는데 사용할 수 있는 데이터 컨테이너입니다. 

메모리의 효율
유니티에서 같은 오브젝트를 여러개 만들때, 프리팹이라는 방식으로 원본을 만들고 그 원본을 복사하여 원본과 똑같은 사본들을 만들어냅니다. 이런 경우에 Scriptable Object는 유용하게 사용될 수 있습니다. 
프리팹을 인스턴스화 할 때 마다 해당 데이터의 자체 사본이 생성됩니다. 즉 사본을 많이 생성할수록 메모리를 많이 소모하게됩니다. 
만약 원본 오브젝트가 1byte의 메모리를 사용한다면, 사본을 10개 만들면 10byte, 100개 만들면 100byte의 메모리가 필요합니다.
단순히 1byte의 원본 데이터와 완전히 똑같은 데이터를 100개 저장하는데 메모리를 사용한다면, 이는 굉장히 비효율적이라고 볼 수 있습니다. 
이 때, Scriptable Object를 사용한다면 메모리에서 단 1바이트만 사용하여 원본 데이터를 저장하고, 같은 내용을 사용하는 100개의 사본들은 이를 참조하는 방식으로 쓸 수 있습니다. 

 */