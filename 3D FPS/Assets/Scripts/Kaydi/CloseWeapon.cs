using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CloseWeapon : MonoBehaviour
{
    [FormerlySerializedAs("handName")] public string weaponName;  // ��Ŭ�̳� �� �� ����. �̸����� ������ ���̴�.

    public bool isHand;
    public bool isAxe;
    public bool isSword;
    public bool isPickAxe;

    public float range; // ���� ����. ���� ������ ������ ������ ������
    public int damage; // ���ݷ�
    public float workSpeed; // �۾� �ӵ�

    public float attackDelay;  // ���� ������. ���콺 Ŭ���ϴ� ���� ���� ������ �� �����Ƿ�.
    public float attackDelayOn;  // ���� Ȱ��ȭ ����. ���� �ִϸ��̼� �߿��� �ָ��� �� �������� �� ���� ���� �������� ���� �Ѵ�.
    public float attackDelayOff;  // ���� ��Ȱ��ȭ ����. ���� �� ������ �ָ��� ���� �ִϸ��̼��� ���۵Ǹ� ���� �������� ���� �ȵȴ�.

    public Animator animator;  // �ִϸ����� ������Ʈ
}
