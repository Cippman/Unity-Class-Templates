using UnityEngine;

namespace CippSharp.ClassTemplates
{
    [CreateAssetMenu(menuName = "Class Templates/Template")]
    public class TemplateObject : ScriptableObject
    {
        [SerializeField] public string templateName = "Template Name";

        [SerializeField] public string templateTag = "Untagged";

        [TextArea(3, 255, order = 1)] 
        [SerializeField] public string template;
    }
}