using UnityEngine.UI;
using UnityEngine;

public class SoundController : MonoBehaviour {

    public static SoundController instance;

    public AudioSource MusicAudioSource;
    public AudioSource PlayerShootAudioSource;
    public AudioSource ShieldHitAudioSource;
    public AudioSource PlayerDeathAudioSource;
    public AudioSource EnemyLaserAudioSource;
    public AudioSource EnemyDeathAudioSource;
    public AudioSource ReadyGoAudioSource;
    public AudioSource PetuniasAudioSource;

    public AudioClip[] LaserSounds;
    public AudioClip CowSound;

    public Slider MusicVolumeSlider;
    public Text MusicVolumeText;
    public Slider SoundEffectsVolumeSlider;
    public Text SoundEffectsVolumeText;

    private float SoundEffectsVolume = 100f;

    void Awake() {
        instance = this;
        MusicVolumeSlider.value = MusicAudioSource.volume * 100f;
        SoundEffectsVolumeSlider.value = SoundEffectsVolume;
    }

    void Update() {
        MusicVolumeText.text = MusicVolumeSlider.value.ToString();
        SoundEffectsVolumeText.text = SoundEffectsVolume.ToString();
    }

    public void ChangeBackgroundMusicVolume() {
        MusicAudioSource.volume = MusicVolumeSlider.value / 100;
    }

    public void ChangeSoundEffectsMusicVolume() {
        SoundEffectsVolume = SoundEffectsVolumeSlider.value;
    }

    public void PlayPlayerShoot() {
        if (PlayerShootAudioSource.isPlaying) {
            return;
        }
        if (GameController.instance.CurrentGameState.CurrentBullet == 1) {
            PlayerShootAudioSource.clip = CowSound;
        } else {
            int laserSound = Random.Range(0, LaserSounds.Length);
            PlayerShootAudioSource.clip = LaserSounds[laserSound];
        }
        PlayerShootAudioSource.volume = SoundEffectsVolume / 100f;
        PlayerShootAudioSource.Play();
    }

    public void PlayShieldHit() {
        if (ShieldHitAudioSource.isPlaying) {
            return;
        }

        ShieldHitAudioSource.volume = SoundEffectsVolume / 100f;
        ShieldHitAudioSource.Play();
    }

    public void PlayPlayerDeath() {
        if (PlayerDeathAudioSource.isPlaying) {
            return;
        }

        PlayerDeathAudioSource.volume = SoundEffectsVolume / 100f;
        PlayerDeathAudioSource.Play();
    }

    public void StopPlayerDeathSound() {
        PlayerDeathAudioSource.Stop();
    }

    public void PlayEnemyLaserShoot() {
        if (EnemyLaserAudioSource.isPlaying) {
            return;
        }

        EnemyLaserAudioSource.volume = SoundEffectsVolume / 100f;
        EnemyLaserAudioSource.Play();
    }

    public void PlayEnemyDeathSound() {
        if (EnemyDeathAudioSource.isPlaying) {
            return;
        }

        EnemyDeathAudioSource.volume = SoundEffectsVolume / 100f;
        EnemyDeathAudioSource.Play();
    }

    public void PlayReadyGoSound() {
        if (ReadyGoAudioSource.isPlaying) {
            return;
        }

        ReadyGoAudioSource.volume = SoundEffectsVolume / 100f;
        ReadyGoAudioSource.Play();
    }

    public void PlayPetuniasSound() {
        if (PetuniasAudioSource.isPlaying) {
            return;
        }

        PetuniasAudioSource.volume = SoundEffectsVolume / 100f;
        PetuniasAudioSource.Play();
    }
}
