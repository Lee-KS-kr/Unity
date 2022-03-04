using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    //FindObjectOfType : 특정 타입의 컴포넌트를 가지고 있는 게임 오브젝트를 찾아주는 함수
    //Find : 파라메터로 받은 문자열과 같은 이름을 가진 게임 오브젝트를 찾아주는 함수 (가장 비효율적)
    //FindGameObjectWithTag : 파라메터로 받은 문자열과 같은 태그를 가진 게임 오브젝트를 찾아주는 함수


    // 싱글톤 사용법. Monobehaviour를 상속받은 스크립트에서는 사용이 불가능하다.
    private static TestTest instance = null; // 프로그램 전체에서 단 하나만 존재한다.
    private TestTest() { } // 생성자를 private으로 해서 밖에서 new 할 수 없도록 한다.

    private static int score = 0;
    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    // 프로퍼티 작성
    // public이라 아직 밖에서 접근이 가능하다
    // static 함수는 객체를 밖에서 생성하지 않아도 되기 때문에 static으로 선언

    public static TestTest Inst
    {
        get
        {
            if (instance == null) // 객체 생성이 한번도 안일어났는지 확인
            {
                instance = new TestTest(); // 한번도 안일어났으면 그때 처음으로 객체 생성
            }
            return instance; // return까지 왔다는 것은 instance에 이미 무엇인가 할당되어있음
        }
    }
}
