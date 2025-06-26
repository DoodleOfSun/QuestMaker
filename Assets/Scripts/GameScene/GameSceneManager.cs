using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameSceneManager : MonoBehaviour
{
    public GameObject openedScroll;
    public GameObject scrollLimit1;
    public GameObject scrollLimit2;
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
        if (Mouse.current.leftButton.isPressed) // 마우스 왼쪽 클릭 키다운
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // 펼쳐진 두루마리를 드래그
                if (hit.collider.name.Contains("Open"))
                {
                    hit.collider.gameObject.transform.position = new Vector2(mousePos.x, mousePos.y + 0.5f); // 두루마리 위치 조정
                }

                // 두루마리를 클릭
                else
                {
                    hit.collider.gameObject.SetActive(false);
                    Instantiate(openedScroll, new Vector2(mousePos.x, mousePos.y + 0.5f), Quaternion.identity);
                }


                hit.collider.transform.position = ClampScroll(hit.collider.gameObject.transform.position);
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
}