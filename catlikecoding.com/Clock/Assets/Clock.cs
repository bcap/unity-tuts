using System;
using UnityEngine;

public class Clock : MonoBehaviour {
    const float degreesPerHour = 30f;
    const float degreesPerMinute = 6f; 
    const float degreesPerSecond = 6f;

    public bool continuous;

    public Transform hoursTransform, minutesTransform, secondsTransform;

    public void FixedUpdate () {
        if (continuous) {
            UpdateContinuous();
        } else {
            UpdateDiscrete();
        }
	}

    private void UpdateContinuous() {
        TimeSpan span = DateTime.Now.TimeOfDay;
		hoursTransform.localRotation = Quaternion.Euler(0f, (float)span.TotalHours * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, (float)span.TotalMinutes * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, (float)span.TotalSeconds * degreesPerSecond, 0f);
    }

    private void UpdateDiscrete() {
        DateTime now = DateTime.Now;
		hoursTransform.localRotation = Quaternion.Euler(0f, now.Hour * degreesPerHour, 0f);
        minutesTransform.localRotation = Quaternion.Euler(0f, now.Minute * degreesPerMinute, 0f);
        secondsTransform.localRotation = Quaternion.Euler(0f, now.Second * degreesPerSecond, 0f);
    }
}
