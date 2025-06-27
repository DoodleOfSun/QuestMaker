using UnityEngine;

public class Scroll: MonoBehaviour
{
    public GameObject scroll;
    public GameObject targetMonster;
    public int rewardGold;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(GameObject scroll, GameObject targetMonster,int reward)
    {
        this.scroll = scroll;
        this.targetMonster = targetMonster;
        this.rewardGold = reward;
    }

    private void floating()
    {

    }

}
