using Assets.Scripts.InteractableObjects;
using HairyEngine.HairyCamera;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [Serializable]
    struct ImageForLose
    {
        public string name;
        public Sprite sprite;
    }
    class CheckPanelController : MonoBehaviour
    {
        [SerializeField] float delay;
        [SerializeField] PanelSpawnController loader;
        [SerializeField] ComponentPanelController panel;
        [SerializeField] Text analizeText;
        [SerializeField] Text nextText;
        [SerializeField] Image resultImage;
        [SerializeField] List<ImageForLose> loseImage;
        [SerializeField] Sprite completeImage;

        public void Init(ObjectData data)
        {
            gameObject.SetActive(true);
            loader.OpenPanel();
            panel.DisableOnHide();
            StartCoroutine(AnalizeLevel(data));
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator AnalizeLevel(ObjectData data)
        {
            float timer = 0f;
            yield return null;
            //lose
            if (data != null)
            {
                foreach (ImageForLose image in loseImage)
                    if (image.name == data.name)
                        resultImage.sprite = image.sprite;
                nextText.text = "Заново";
                GameHandler.Instance.RestartLevel(true);
            }
            else
            {
                nextText.text = "Дальше";
                resultImage.sprite = completeImage;
                GameHandler.Instance.StartNextLevel(true);
            }

            while (timer < delay)
            {
                timer += Time.deltaTime;
                analizeText.text = "Аналилируем";
                float pointer = (timer % 1f).Remap(0, 1, 0, 3);
                for (int i = 0; i < pointer; i++)
                    analizeText.text += ".";
                yield return null;
            }
            loader.DisableOnHide();
            panel.OpenPanel();
        }
    }
}
