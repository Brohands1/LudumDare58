using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllerSystem : MonoBehaviour
{
    [System.Serializable]
    public class Controller
    {
        public Independent independent;
        public List<Dependent> dependent;
    };

    public List<Controller> controllers = new List<Controller>();
    void Update()
    {
        foreach (var controller in controllers)
        {
            if (Vector3.Distance(controller.independent.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    controller.independent.Active = !controller.independent.Active;
                    foreach (var dep in controller.dependent)
                    {
                        dep.changeTo(controller.independent.Active);
                    }
                }
                if (Input.GetKeyDown(ShadowUtil.summonControllerKey)&&Data.currentShadows>1&&controller.independent.Shadowed==false&&Data.enableSummonController)
                {
                    Debug.Log($"Create controller shadow{Data.currentShadows}");
                    controller.independent.Shadowed = true;
                    for (int i = 0; i < Data.keys.Length; i++)
                    {
                        if (Data.occupied[i] == false)
                        {
                            Data.occupied[i] = true;
                            Data.shadows.Add(new Data.ShadowedController(controller,i));
                            
                            break;
                        }
                    }
                }
            }
        }
        foreach (var controller in Data.shadows)
        {
            if(controller.controller.independent.Shadowed)
            {
                controller.controller.independent.GetComponentInChildren<TextMeshPro>().SetText((controller.num+1).ToString());
            }
        }
    }
}