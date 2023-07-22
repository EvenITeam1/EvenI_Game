using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconApply : MonoBehaviour
{
    [SerializeField] List<GameObject> IconList = new List<GameObject>(10);
    [SerializeField] List<Image> userIcons;
    public void ApplyIcon(int n)
    {
        for (int i = 0; i < userIcons.Count; i++)
            userIcons[i].sprite = IconList[n - 1].GetComponent<Image>().sprite;

        for (int i = 0; i < IconList.Count; i++)
            IconList[i].GetComponent<Outline>().enabled = false;

        IconList[n - 1].GetComponent<Outline>().enabled = true;
    }
}
