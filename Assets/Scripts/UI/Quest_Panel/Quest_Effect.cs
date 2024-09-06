using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Unity�� ScriptableObject�� �����͸� �����ϰ� �����ϴ� �� ���Ǵ� Ŭ�����Դϴ�. 
/// �ַ� ���� ���߿��� ���� ������Ʈ�� ���³� ������ �����ϴ� �� Ȱ��˴ϴ�. 
/// ScriptableObject�� Unity���� �����ϴ� �⺻ MonoBehaviour�ʹ� �޸�, ���� ������Ʈ�� ������ �ʿ� ���� ���������� �����͸��� ��� ������Ʈ�Դϴ�. 
/// �̸� ���� ������ ������ ������ �� ȿ�������� �����ϰ� ������ �� �ֽ��ϴ�.
/// </summary>
public abstract class Quest_Effect : ScriptableObject //�߻�Ŭ����
    {
        public abstract bool ExecuteRole(QuestType questtype);

    }

