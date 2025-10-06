using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理

public class switchscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 公开方法，供按钮点击调用
    public void SwitchToFinalScene()
    {
        Debug.Log("please");
        SceneManager.LoadScene("FinalMap");
    }
}
