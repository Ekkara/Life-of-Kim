using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnergyBar : MonoBehaviour
{
    [SerializeField] float changeSpeed;
    Slider slider;
    float currentGoal, maxVal, minVal;
    
    void Start() {
        slider = GetComponent<Slider>();
        maxVal = slider.maxValue;
        minVal = slider.minValue;
        currentGoal = slider.value;
    }

    public void ChangeValue(float addValue) {
        StopAllCoroutines();
        currentGoal += addValue;
        currentGoal = Mathf.Clamp(currentGoal, minVal, maxVal);    
        StartCoroutine(AlterValue(slider.value));
    }
    IEnumerator AlterValue(float currentValue) {
        float timeElapsed = 0;
        while (!slider.value.Equals(currentGoal)) {
          
            slider.value = Mathf.Lerp(currentValue, currentGoal, timeElapsed / (Mathf.Abs(currentGoal - currentValue) / changeSpeed));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
