using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelSkillPointsDisplay: UIElement
{
    [SerializeField] private TextMeshProUGUI text = null;

    void DisplaySkillPoints()
    {
        if (text != null)
        {
            text.text = GameManager.LevelSp.ToString();
        }
    }

    public override void UpdateElement()
    {
        base.UpdateElement();
        
        DisplaySkillPoints();
    }
}
