using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSceneManager : MonoBehaviour
{
    public GameObject scroll;
    public GameObject openedScroll;
    public GameObject scrollLimit1;
    public GameObject scrollLimit2;
    public GameObject scrollMovingSpot;

    public int questLimitNumber = 3;    // 최대 퀘스트 제한
    private int currentQuestNumber = 0; // 현제 퀘스트 숫자

    private Vector2 currentRefScroll;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckingMouseControl();
    }

    private void CheckingMouseControl()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)) // 마우스 또는 터치 클릭

        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // 몬스터 클릭한 경우
                if (hit.collider.name.Contains("Monster") && currentQuestNumber <= questLimitNumber)
                {
                    MonsterAction(hit.collider.gameObject);
                    return;
                }
            }
        }

        else if (Mouse.current.leftButton.isPressed ||
            (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)) // 마우스 왼쪽 클릭 키다운
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // 펼쳐진 두루마리를 드래그
                if (hit.collider.name.Equals("ScrollOpened(Clone)"))
                {
                    hit.collider.gameObject.transform.position = new Vector2(mousePos.x, mousePos.y + 0.5f); // 두루마리 위치 조정
                    hit.collider.transform.position = ClampScroll(hit.collider.gameObject.transform.position);
                }

                // 두루마리를 클릭함
                else if(hit.collider.name.Equals("Scroll(Clone)"))
                {
                    Destroy(hit.collider.gameObject); // 두루마리 제거
                    Instantiate(openedScroll, new Vector2(mousePos.x, mousePos.y + 0.5f), Quaternion.identity);
                }
            }
        }
    }

    private Vector2 ClampScroll(Vector2 scrollPos)
    {
        // 마우스 위치를 사각형 안으로 클램프
        float clampedX = Mathf.Clamp(scrollPos.x, scrollLimit1.transform.position.x, scrollLimit2.transform.position.x);
        float clampedY = Mathf.Clamp(scrollPos.y, scrollLimit1.transform.position.y, scrollLimit2.transform.position.y);
        Vector2 clampedPos = new Vector2(clampedX, clampedY);
        return clampedPos;
    }

    private void MonsterAction(GameObject monster)
    {
        Transform child = monster.transform.Find("NewQuestMark");
        if (child == null)
        {
            Debug.Log("이미 퀘스트 등록된 개체입니다." + currentQuestNumber);
        }
        else
        {
            MakingScroll(scrollMovingSpot.transform.position);
            Destroy(child.gameObject); // 퀘스트 마크 제거
        }
    }

    private void MakingScroll(Vector2 pos)
    {
        currentQuestNumber++;


        // 두루마리 생성
        GameObject newScroll = Instantiate(scroll, new Vector2(pos.x, pos.y), Quaternion.identity);

        Debug.Log("두루마리 생성됨: " + currentQuestNumber);
    }
}