using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ���볡������

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

    // ��������������ť�������
    public void SwitchToFinalScene()
    {
        Debug.Log("please");
        SceneManager.LoadScene("FinalMap");
    }
}
