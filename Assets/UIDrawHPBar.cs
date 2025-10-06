using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrawHPBar : MonoBehaviour
{
    public GameObject Full, increasing;
    List<GameObject> fulls = new List<GameObject>();
    // Update is called once per frame
    void Update()
    {
        if(Data.UIRefreshNeeded)
        {
            Refresh();
        }
    }
    public void Refresh()
    {
        foreach(var f in fulls)
        {
            Destroy(f);
        }
        fulls.Clear();
        Vector3 pos=new Vector3();
        for (int i = 0; i < Data.currentShadows; i++)
        {
            var go = Instantiate(Full, transform); // 将实例设为当前对象的子物体
            go.transform.localPosition = pos;      // 使用 localPosition 保持相对布局
            fulls.Add(go);
            pos.x += 120f;
        }
        if(Data.currentAddShadowTimer > 0)
        {
            var go = Instantiate(increasing, transform);
            go.transform.localPosition = pos;
            fulls.Add(go);
        }
    }
}
