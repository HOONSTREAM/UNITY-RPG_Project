using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 참조문서 
{
    // 유니티 프로파일링 URL
    /* https://dev-nicitis.tistory.com/7 */


    // Sprite 클래스와 Image 클래스의 차이점

    /*`Sprite` 클래스와 `Image` 클래스는 Unity에서 2D 그래픽을 다루는 데 사용되는 클래스들인데, 각각의 역할과 사용 용도가 조금 다릅니다.
   1. `Sprite`: `Sprite`는 Unity의 2D 그래픽 시스템에서 사용되는 텍스처의 한 형태입니다. `Sprite`는 텍스처, 크기, 위치, 회전 등의 정보를 포함하며, 
   이를 이용하여 2D 캐릭터, 배경, 아이템 등을 표현합니다. `Sprite`는 `SpriteRenderer` 컴포넌트를 통해 화면에 표시됩니다.
   2. `Image`: `Image`는 Unity의 UI 시스템에서 사용되는 컴포넌트 중 하나입니다. `Image` 컴포넌트는 `Sprite`를 이용하여 화면에 그림을 표시하는 역할을 합니다. 
   즉, UI 요소(버튼, 아이콘, 배경 등)를 화면에 표시할 때 사용됩니다.
   요약하면, `Sprite`는 2D 게임 오브젝트를 표현하는 데 사용되는 데이터 형식이고,
   `Image`는 이러한 `Sprite`를 이용하여 UI 요소를 화면에 표시하는 컴포넌트입니다.*/




    /*Scriptable Object는 대량의 데이터를 저장하는데 사용할 수 있는 데이터 컨테이너입니다. 

 메모리의 효율
 유니티에서 같은 오브젝트를 여러개 만들때, 프리팹이라는 방식으로 원본을 만들고 그 원본을 복사하여 원본과 똑같은 사본들을 만들어냅니다. 이런 경우에 Scriptable Object는 유용하게 사용될 수 있습니다. 
 프리팹을 인스턴스화 할 때 마다 해당 데이터의 자체 사본이 생성됩니다. 즉 사본을 많이 생성할수록 메모리를 많이 소모하게됩니다. 
 만약 원본 오브젝트가 1byte의 메모리를 사용한다면, 사본을 10개 만들면 10byte, 100개 만들면 100byte의 메모리가 필요합니다.
 단순히 1byte의 원본 데이터와 완전히 똑같은 데이터를 100개 저장하는데 메모리를 사용한다면, 이는 굉장히 비효율적이라고 볼 수 있습니다. 
 이 때, Scriptable Object를 사용한다면 메모리에서 단 1바이트만 사용하여 원본 데이터를 저장하고, 같은 내용을 사용하는 100개의 사본들은 이를 참조하는 방식으로 쓸 수 있습니다. 

  */



}
