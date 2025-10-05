using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSystem : MonoBehaviour
{
    [System.Serializable]
    public class Controller
    {
        public Independent independent;
        public Dependent dependent;
    };

    public List<Controller> controllers = new List<Controller>();
    void Update()
    {
        foreach (var controller in controllers)
        {
            if (Vector3.Distance(controller.independent.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3)
            {
                //Debug.Log("Press F to interact with the lever");
                //���Լ���������ʾ�����硰��F�����˽�����
                if (Input.GetKeyDown(KeyCode.F))
                {
                    controller.independent.Active = !controller.independent.Active;
                    controller.dependent.changeTo(controller.independent.Active);
                }
                if (Input.GetKeyDown(ShadowUtil.summonControllerKey)&&Data.currentShadows>1&&controller.independent.Shadowed==false&&Data.enableSummonController)
                {
                    Data.currentShadows--;
                    Debug.Log($"Create controller shadow{Data.currentShadows}");
                    controller.independent.Shadowed = true;
                    for (int i = 0; i < Data.keys.Length; i++)
                    {
                        if (Data.occupied[i] == false)
                        {
                            Data.shadows.Add(new Data.shadow(Data.shadow.Type.controller,controller,i));
                            break;
                        }
                    }
                }
            }
        }
    }
}