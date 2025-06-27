using UnityEngine;

public class OpenedScroll : MonoBehaviour
{
    public GameObject scroll;
    public GameObject targetMonster;
    public int rewardGold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(GameObject scroll, GameObject targetMonster, int reward)
    {
        this.scroll = scroll;
        this.targetMonster = targetMonster;
        this.rewardGold = reward;
    }
}
