using Assets.Scripts.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    private static Language _language;
    public static Language language
    {
        get => _language; 
        set
        {
            _language = value;
            PlayerPrefs.SetInt("Settings.Language", (int)value);
            PlayerPrefs.Save();
        }
    }
    static Settings()
    {
        _language = (Language)(PlayerPrefs.HasKey("Settings.Language") ? PlayerPrefs.GetInt("Settings.Language") : 0);
    }
}
