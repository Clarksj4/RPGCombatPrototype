using Sirenix.OdinInspector;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Localization", menuName = "Localization Data")]
public class LocalizationData : SerializedScriptableObject
{
    public string languageCode;
    [OdinSerialize]
    public Dictionary<string, string> strings;


}
