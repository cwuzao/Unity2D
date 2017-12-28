using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodMaker : MonoBehaviour {
    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get
        {
            return _instance;
        }
    }
    public int xlimit = 21;
    public int ylimit = 11;
    public int xoffset = 7;
    public GameObject foodPrefab;
    public Sprite[] foodSprites;
    private Transform foodHolder;
    public GameObject rewardPrefab;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        foodHolder = GameObject.Find("FoodHolder").transform;
        MakeFood(false);
    }

    public void MakeFood(bool isReward)
    {
        GameObject food = Instantiate(foodPrefab);
        food.GetComponent<Image>().sprite = foodSprites[Random.Range(0, foodSprites.Length)];
        food.transform.SetParent(foodHolder,false);
        int x = Random.Range(-xlimit + xoffset, xlimit);
        int y = Random.Range(-ylimit, ylimit);
        food.transform.localPosition = new Vector3(x*30, y*30, 0);
        if (isReward)
        {
            GameObject reward = Instantiate(rewardPrefab);
            reward.transform.SetParent(foodHolder, false);
            x = Random.Range(-xlimit + xoffset, xlimit);
            y = Random.Range(-ylimit, ylimit);
            reward.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        }
    }

}
