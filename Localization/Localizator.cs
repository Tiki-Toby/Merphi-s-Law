using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Localization
{
    [RequireComponent(typeof(Text))]
    public class Localizator : MonoBehaviour
    {
        [SerializeField] protected string translateName;
        protected Text textField;
        void Awake()
        {
            textField = GetComponent<Text>();
        }
        private void OnEnable()
        {
            Translate();
        }
        public virtual void Translate()
        {
            textField.text = LocalizationManager.Instance.GetText(translateName);
        }
    }
}