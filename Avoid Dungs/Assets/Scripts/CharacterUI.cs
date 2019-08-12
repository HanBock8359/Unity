using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterUI : MonoBehaviour
{
    [SerializeField]
    private Slider hp;
    private Transform var;

    private float currHp;
    private float maxHp;
    private float dangerZone;

    // Start is called before the first frame update
    void Start()
    {
        currHp = 100.0f;
        maxHp = 100.0f;
        dangerZone = 0.3f;
        hp.value = getRatio();
        var = transform.Find("Bar Area");
    }

    // Update is called once per frame
    void Update()
    {
        handleHp();
    }

    private void handleHp()
    {
        if(currHp < 0)
        {
            currHp = 0;
        }
        else if(currHp > maxHp)
        {
            currHp = maxHp;
        }

        //if health is under 30 percent
        if(getRatio() <= dangerZone)
        {
            setColor(Color.red);
        }
        //if helath is over 30 percent
        else
        {
            setColor(Color.green);
        }

        hp.value = Mathf.Lerp(hp.value, getRatio(), Time.deltaTime * 10);
    }

    public void increaseHp(float val)
    {
        currHp += val;
    }

    public void decreaseHp(float val)
    {
        currHp -= val;
    }

    public void fillAllHp()
    {
        currHp = maxHp;
    }

    public bool isHpZero()
    {
        return currHp <= 0;
    }

    public float getRatio()
    {
        return (currHp / maxHp);
    }

    public void setColor(Color color)
    {
        var.Find("Bar").GetComponent<Image>().color = color;
    }

}
