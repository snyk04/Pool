using UnityEngine;

namespace Pool.Audio
{
    public abstract class Sound : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip[] _sounds;

        protected void PlaySound()
        {
            AudioClip sound = _sounds[Random.Range(0, _sounds.Length)];
            _audioSource.clip = sound;
            _audioSource.Play();
        }
    }
}