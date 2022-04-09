using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour
{
    public float maxValue = 100f;
    public float minValue = 0f;
    public int amount = 5;
    public GameObject fullPrefab;
    public GameObject halfPrefab;
    public GameObject zeroPrefab;
    public float separation = 32;

    private float currentValue = 0f;

    private List<GameObject> fullPool = new List<GameObject>();
    private List<GameObject> halfPool = new List<GameObject>();
    private List<GameObject> zeroPool = new List<GameObject>();

    private void Awake()
    {
        currentValue = maxValue;
        CreatePool();
        UpdateRepresentation();
    }

    public void SetValue(float value)
    {
        currentValue = value;
        UpdateRepresentation();
    }

    void CreatePool()
    {
        for(var i=0;i<amount;i++)
        {
            var fullInstance = Instantiate(fullPrefab);
            fullInstance.SetActive(false);
            var halfInstance = Instantiate(halfPrefab);
            halfInstance.SetActive(false);
            var zeroInstance = Instantiate(zeroPrefab);
            zeroInstance.SetActive(false);
            fullPool.Add(fullInstance);
            halfPool.Add(halfInstance);
            zeroPool.Add(zeroInstance);
        }
    }
    GameObject pullFull()
    {
        foreach(var element in fullPool)
        {
            if (!element.activeSelf)
                return element;
        }
        return null;
    }

    GameObject pullHalf()
    {
        foreach (var element in halfPool)
        {
            if (!element.activeSelf)
                return element;
        }
        return null;
    }

    GameObject pullZero()
    {
        foreach (var element in zeroPool)
        {
            if (!element.activeSelf)
                return element;
        }
        return null;
    }

    void PoolBack()
    {
        foreach (var element in fullPool)
            element.SetActive(false);
        foreach (var element in halfPool)
            element.SetActive(false);
        foreach (var element in zeroPool)
            element.SetActive(false);
    }

    void UpdateRepresentation()
    {
        PoolBack();
        var finalNumber = amount * (currentValue / maxValue - minValue);
        var fullHeartAmount = (int)Mathf.Floor(finalNumber);
        var halfHeartAmount = fullHeartAmount + (int)Mathf.Ceil(finalNumber - fullHeartAmount);
        for(var i=0;i<amount;i++)
        {
            GameObject heart;
            if (i < fullHeartAmount)
            {
                heart = pullFull();
            }
            else
            {
                if (i < halfHeartAmount)
                    heart = pullHalf();
                else
                    heart = pullZero();
            }
            heart.SetActive(true);
            heart.transform.parent = this.transform;
            heart.transform.localPosition = new Vector3(i * separation,0f,0f);
        }
    }
}
