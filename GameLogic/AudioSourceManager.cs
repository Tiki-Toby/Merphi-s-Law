using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Data
{
    public class AudioSourceManager : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioClip lobbyMusic;
        [SerializeField] AudioClip inGameMusic;
        [SerializeField] AudioClip loadingMusic;

        [Header("Player Sounds")]
        [SerializeField] AudioSource stepSource;
        [SerializeField] AudioClip stepSound;
        [SerializeField] AudioClip runSound;

        [Header("Objects Sounds")]
        [SerializeField] AudioSource useSource;
        [SerializeField] AudioClip pickClip;
        [SerializeField] AudioClip wrondUse;

        private void Start()
        {
            float musicVolume = PlayerPrefs.HasKey("Settings.MusicVolume") ? PlayerPrefs.GetFloat("Settings.MusicVolume") : 1f;
            float soundVolume = PlayerPrefs.HasKey("Settings.SoundVolume") ? PlayerPrefs.GetFloat("Settings.SoundVolume") : 1f;

            SetMusicVolume(musicVolume);
            SetStepSoundVolume(soundVolume);
        }

        public void StartGameMusic()
        {
            StartMusic(inGameMusic);
        }
        public void StartLobbyMusic()
        {
            StartMusic(lobbyMusic);
        }
        public void StartLoadingMusic()
        {
            StartMusic(loadingMusic);
        }
        public void StartMusic(AudioClip music)
        {
            musicSource.Stop();
            musicSource.clip = music;
            musicSource.Play();
        }
        private bool isRun;
        public void PlayStepSound(bool isWalk, bool isRun)
        {
            if (!isWalk)
            {
                stepSource.Stop();
                return;
            }
            if (this.isRun != isRun)
            {
                AudioClip sound = isRun ? runSound : stepSound;
                float soundPitch = isRun ? 0.65f : 0.95f;
                this.isRun = isRun;
                stepSource.clip = sound;
                stepSource.pitch = soundPitch;
                stepSource.Stop();
            }
            if(!stepSource.isPlaying)
                stepSource.Play();
        }
        public void PlayUseSound(AudioClip sound = null)
        {
            if (sound == null) sound = pickClip;
            useSource.clip = sound;
            useSource.Play();
        }
        public void PlayWrongSound()
        {
            useSource.clip = wrondUse;
            useSource.Play();
        }
        public void SetMusicVolume(Slider slider)
        {
            SetMusicVolume(slider.value);
        }
        public void SetMusicVolume(float value)
        {
            musicSource.volume = value;
            PlayerPrefs.SetFloat("Settings.MusicVolume", value);
        }
        public void SetStepSoundVolume(Slider slider)
        {
            SetStepSoundVolume(slider.value);
        }
        private void SetStepSoundVolume(float value)
        {
            stepSource.volume = value;
            useSource.volume = value;
            PlayerPrefs.SetFloat("Settings.SoundVolume", value);
        }
    }
}