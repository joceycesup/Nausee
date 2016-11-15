using UnityEngine;
using System.Collections;

public class micInput : MonoBehaviour {
	private SpriteRenderer noMicSR;

    public float volume;
    public static float MicLoudness;
    private string _device;
    private AudioClip _clipRecord = new AudioClip();
    private int _sampleWindow = 128;
    private bool _isInitialized;

	private bool micPluggedIn = false;

	void Start () {
		noMicSR = gameObject.GetComponent<SpriteRenderer> ();
		noMicSR.enabled = false;
		CheckMicPlugged ();
		InitMic ();
		_isInitialized = true;
	}

    void InitMic ()	{
		if (micPluggedIn) {
			_device = Microphone.devices [0];
			_clipRecord = Microphone.Start (_device, true, 999, 44100);
		}
    }

    void StopMicrophone () {
        Microphone.End(_device);
    }

    float LevelMax () {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1);
        if (micPosition < 0) {
            return 0;
        }
        _clipRecord.GetData(waveData, micPosition);
        for (int i = 0; i < _sampleWindow; ++i) {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak) {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

	void CheckMicPlugged () {
		micPluggedIn = (Microphone.devices.Length > 0);
		noMicSR.enabled = !micPluggedIn;
	}

    void Update () {
		CheckMicPlugged ();
		if (micPluggedIn) {
			MicLoudness = LevelMax();
			volume = MicLoudness;
		}
    }

    void OnEnable () {
        InitMic ();
        _isInitialized = true;
    }

    void OnDisable () {
        StopMicrophone();
    }

    void OnDestory () {
        StopMicrophone();
    }

    void OnApplicationFocus (bool focus) {
		if (focus) {
            if (!_isInitialized) {
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus) {
            StopMicrophone();
            _isInitialized = false;
        }
    }
}
