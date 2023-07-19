using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetPackageData : MonoBehaviour
{
    public List<Image> image;
    public List<TextMeshProUGUI> nameText;
    public List<TextMeshProUGUI> contentsText;
    public List<TextMeshProUGUI> costText;
    public List<TextMeshProUGUI> descriptionText;

    public void LoadPackageData(int n)
    {
        image[0] = image[n];
        nameText[0].text = nameText[n].text;
        contentsText[0].text = contentsText[n].text;
        costText[0].text = costText[n].text;
    }
}
