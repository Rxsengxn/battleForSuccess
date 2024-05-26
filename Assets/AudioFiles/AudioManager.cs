using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [field: SerializeField] public AudioMixerGroup MasterSoundGroup { get; private set; }
    [field: SerializeField, Range(0.0001f, 1f)] public float MasterVolume { get; private set; }

    public Audio[] audios;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            this.MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        }
        else
        {
            AudioManager.Instance.Play("Theme");
            Destroy(gameObject);
            return;
        }

        foreach (Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;

            audio.source.outputAudioMixerGroup = MasterSoundGroup;
        }

        Play("Theme");

    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateMixerVolume(this.MasterVolume);
    }

    public void Play(string name)
    {
        Audio audio = System.Array.Find(audios, audio => audio.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Audio: " + name + " not found!");
            return;
        }
        audio.source.Play();
    }

    public void Stop(string name)
    {
        Audio audio = System.Array.Find(audios, audio => audio.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Audio: " + name + " not found!");
            return;
        }
        audio.source.Stop();
    }

    public void Pause(string name)
    {
        Audio audio = System.Array.Find(audios, audio => audio.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Audio: " + name + " not found!");
            return;
        }
        audio.source.Pause();
    }

    public void Resume(string name)
    {
        Audio audio = System.Array.Find(audios, audio => audio.name == name);
        if (audio == null)
        {
            Debug.LogWarning("Audio: " + name + " not found!");
            return;
        }
        audio.source.UnPause();
    }

    public string GetCurPlayingSong()
    {
        string song = "";

        foreach (Audio audio in audios)
        {
            if (audio.source.isPlaying)
            {
                song = audio.name;
                break;
            }
        }
        return song;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMixerVolume(float value) 
    { 
        MasterSoundGroup.audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }
}