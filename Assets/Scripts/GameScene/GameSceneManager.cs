using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    public GameObject scroll;
    public GameObject openedScroll;
    public GameObject scrollLimit1;
    public GameObject scrollLimit2;
    public GameObject scrollMovingSpot;

    public AudioClip monsterQuestSound;
    public AudioClip scrollPaperSound;
    public AudioSource additionalSFX;

    public GameObject slimeForMaps;
    public GameObject slimeForScroll;

    public int gameLevel = 1;


    public int questLimitNumber = 3;    // 최대 퀘스트 제한
    private int currentQuestNumber = 0; // 현제 퀘스트 숫자

    private Vector2 currentRefScroll;

    public List<GameObject> monsterSpawnSpots = new List<GameObject>(); // 몬스터 스폰 위치 리스트

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //additionalSFX.volume = FindAnyObjectByType<AudioManager>().sfxVolume; // SFX 볼륨 설정
        additionalSFX.volume = 0.5f; // SFX 볼륨 설정 (임시로 0.5f로 설정, AudioManager에서 가져오는 것으로 변경 가능) 이욜 좀치네
        gameLevel = 1;


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
                    additionalSFX.PlayOneShot(monsterQuestSound); // 몬스터 퀘스트 사운드 재생
                    MonsterClickAction(hit.collider.gameObject);
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
                    additionalSFX.PlayOneShot(scrollPaperSound); // 두루마리 클릭 사운드 재생 올 ㅋㅋ
                    Scroll scroll = hit.collider.gameObject.GetComponent<Scroll>();
                    GameObject newOpenScroll = Instantiate(openedScroll, new Vector2(mousePos.x, mousePos.y + 0.5f), Quaternion.identity);

                    if (scroll.targetMonster.name.Contains("Slime"))
                    {
                        Instantiate(slimeForScroll, new Vector2(newOpenScroll.transform.position.x, newOpenScroll.transform.position.y + 0.5f), Quaternion.identity, newOpenScroll.transform);
                        newOpenScroll.GetComponent<OpenedScroll>().Init(newOpenScroll, slimeForScroll, scroll.rewardGold);
                    }

                    Destroy(hit.collider.gameObject); // 말려있는 두루마리 제거

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

    private void MonsterClickAction(GameObject monster)
    {
        Transform child = monster.transform.Find("NewQuestMark");
        if (child == null)
        {
            Debug.Log("이미 퀘스트 등록된 개체입니다." + currentQuestNumber);
        }
        else
        {
            MakingScroll(scrollMovingSpot.transform.position, monster);
            Destroy(child.gameObject); // 퀘스트 마크 제거
        }
    }

    private void MakingScroll(Vector2 pos, GameObject monster)
    {
        currentQuestNumber++;
        // 두루마리 생성
        GameObject newScroll = Instantiate(scroll, new Vector2(pos.x, pos.y), Quaternion.identity);

        if (monster.name.Contains("Slime"))
        {
            Debug.Log("1111");
            newScroll.GetComponent<Scroll>().Init(newScroll, slimeForScroll, 34);
        }

        Debug.Log("두루마리 생성됨: " + currentQuestNumber);
    }
}