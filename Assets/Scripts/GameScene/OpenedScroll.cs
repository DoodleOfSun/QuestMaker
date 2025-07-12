using UnityEngine;
using UnityEngine.UI;

public class OpenedScroll : MonoBehaviour
{
    public GameObject scroll;
    public GameObject targetMonster;
    public int rewardGold;
    private Text rewardGoldText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovingText();
    }

    public void Init(GameObject scroll, GameObject targetMonster, int reward)
    {
        this.scroll = scroll;
        this.targetMonster = targetMonster;
        this.rewardGold = reward;
        rewardGoldText = GetComponentInChildren<Text>();
        rewardGoldText.text = rewardGold.ToString() + "G"; // 보상 금액 텍스트 설정
    }



    private void MovingText()
    {
        if (rewardGoldText != null)
        {
            Vector3 targetPosition = Camera.main.WorldToScreenPoint(this.transform.position);
            rewardGoldText.transform.position = new Vector3(targetPosition.x, targetPosition.y-50f, targetPosition.z);
        }
    }
}
