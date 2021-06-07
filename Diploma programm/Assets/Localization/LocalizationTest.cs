using System.Collections.Generic;
using UnityEngine;

public class LocalizationTest : MonoBehaviour
{
    [SerializeField]
    private LocalizedText text_play;
    [SerializeField]
    private LocalizedText text_option;
    [SerializeField]
    private LocalizedText text_quit;
    [SerializeField]
    private LocalizedText text_quitmenu;
    [SerializeField]
    private LocalizedText text_quitmenu_yes;
    [SerializeField]
    private LocalizedText text_quitmenu_no;
    [SerializeField]
    private LocalizedText text_level1;
    [SerializeField]
    private LocalizedText text_level2;
    [SerializeField]
    private LocalizedText text_level3;
    [SerializeField]
    private LocalizedDropdown dropdown;

    public void LocalizeText_level1()
    {
        text_level1.Localize("level1_text");
    }
    public void LocalizeText_level2()
    {
        text_level2.Localize("level2_text");
    }
    
    public void LocalizeText_level3()
    {
        text_level3.Localize("level3_text");
    }
    public void LocalizeDropdown()
    {
        var options = new List<string>() { "Blue_Key", "Green_Key", "Black_Key" };
        dropdown.Localize(options);
    }
}
