using UnityEngine;

public class Scroll: MonoBehaviour
{
    public GameObject scroll;
    public GameObject targetMonster;
    public int rewardGold;


    // Floating 효과
    public float speed;
    public float floatingHeight;
    private Vector2 startPos;
    private float timeOffset;


    void Start()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, Mathf.PI * 2f); // 0 ~ 2π 사이 랜덤 오프셋
    }


    // Update is called once per frame
    void Update()
    {
        floating();
    }

    public void Init(GameObject scroll, GameObject targetMonster,int reward)
    {
        this.scroll = scroll;
        this.targetMonster = targetMonster;
        this.rewardGold = reward;
    }

    private void floating()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed + timeOffset) * floatingHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

}
