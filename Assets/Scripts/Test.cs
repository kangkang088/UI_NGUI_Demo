using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BasePanel<TipPanel>.Instance.ShowMe();
            BasePanel<TipPanel>.Instance.ChangeInfo("hahahhahahaha");
        }
    }
}
