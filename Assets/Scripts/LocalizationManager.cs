using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public enum StringCase
{
    Unmodified, Upper, Lower, Title
}

public class LocalizationManager : MonoSingleton<LocalizationManager>
{
    public string CurrentLanguage = CultureInfo.CurrentCulture.Name;
    public string FallbackLanguage = new CultureInfo("en-US").Name;
    public LocalizationData[] Data;

    private Dictionary<string, string> strings = new Dictionary<string, string>();
    private Dictionary<string, string> fallbackStrings = new Dictionary<string, string>();

    private CultureInfo cultureInfo;
    private CultureInfo fallbackCultureInfo;

    protected override void Awake()
    {
        base.Awake();

        cultureInfo = new CultureInfo(CurrentLanguage);
        fallbackCultureInfo = new CultureInfo(FallbackLanguage);

        strings = Data.FirstOrDefault(d => d.languageCode == CurrentLanguage)?.strings ?? new Dictionary<string, string>();
        fallbackStrings = Data.FirstOrDefault(d => d.languageCode == FallbackLanguage)?.strings ?? new Dictionary<string, string>();
    }

    public string GetStringFormat(string key, StringCase stringCase = StringCase.Unmodified, params string[] inlineKeys)
    {
        // Get base string
        string localizedString = GetString(key, stringCase);

        // Replace each '{x}' string with the inline value with same index 
        for (int i = 0; i < inlineKeys.Length; i++)
        {
            string searchText = $"{{{i}}}";
            string replacementText = GetString(inlineKeys[i], stringCase);
            localizedString = localizedString.Replace(searchText, replacementText);
        }

        return localizedString;
    }

    public string GetString(string key, StringCase stringCase = StringCase.Unmodified)
    {
        if (strings.TryGetValue(key, out string value))
            return Caseify(value, cultureInfo, stringCase);

        if (fallbackStrings.TryGetValue(key, out string fallback))
        {
            Debug.LogWarning($"Failed to find localized value for key: {key}, falling back to: {fallback}");
            return Caseify(fallback, fallbackCultureInfo, stringCase);
        }

        Debug.LogWarning($"Failed to find localized value or fallback value for key: {key}");
        return null;
    }

    private string Caseify(string value, CultureInfo cultureInfo, StringCase stringCase)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        switch (stringCase)
        {
            case StringCase.Upper:
                return cultureInfo.TextInfo.ToUpper(value);
            case StringCase.Lower:
                return cultureInfo.TextInfo.ToLower(value);
            case StringCase.Title:
                return cultureInfo.TextInfo.ToTitleCase(value);
            default:
                return value;
        }
    }
}
