using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetListItemData : MonoBehaviour {
public void SetData(string name)
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = name;
    }

/*public void GetData(string name)
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = name;
    }*/
}
