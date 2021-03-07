using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField]
    private string key = null;
    [SerializeField]
    private string[] inlineValues = null;
    [SerializeField]
    private StringCase stringCase = StringCase.Unmodified;

    private TextMeshProUGUI text = null;

    public void SetKey(string key, params string[] inlineValues)
    {
        this.key = key;
        this.inlineValues = inlineValues;
    }

    private void UpdateText()
    {
        if (inlineValues != null)
            text.text = LocalizationManager.Instance.GetStringFormat(key, stringCase, inlineValues);

        else
            text.text = LocalizationManager.Instance.GetString(key, stringCase);
    }
}
