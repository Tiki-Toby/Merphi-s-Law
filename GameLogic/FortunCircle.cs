using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortunCircle : MonoBehaviour
{
    private List<float> _turnsSpeed;
    private static bool win;
    private IEnumerator RotateCircle()
    {
        if (!win)
        {
            _turnsSpeed = new List<float>();
            float firstNum = 27.8f;
            for (int i = 50; i >= 0; i--)
            {
                _turnsSpeed.Add(Mathf.Abs(firstNum));
                firstNum = firstNum - 1.6f;
            }
            _turnsSpeed.Sort();
            float sum = 0;
            for (int i = 49; i >= 0; i--)
            {
                transform.Rotate(0, 0, _turnsSpeed[i]);
                sum += _turnsSpeed[i];
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        else
        {
            _turnsSpeed = new List<float>();
            int nums = Random.Range(60, 90);

            for (int i = nums; i >= 0; i--)
                _turnsSpeed.Add(i);
            for (int i = nums - 1; i>=0; i--)
            {
                transform.Rotate(0, 0, _turnsSpeed[i]);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
    }
}
