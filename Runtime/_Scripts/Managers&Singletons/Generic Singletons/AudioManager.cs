/*  (c) 2019 matthew fairchild
 *  
 *  Singleton class
 *  DontDestroyOnLoad
 *  This class manages all sources of audio that can be spawned independantly of a specific object.
 *  It allows for looping audio or one-time audio clips, both of which can be spatial or not
 */

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region VARIABLES

    private static AudioManager m_instance;
    /// <summary>
    /// the singleton instance of the Audio class
    /// </summary>
    public static AudioManager Instance
    {
        get { return m_instance; }
    }

    [Header("Looping Clips")]
    /// <summary>
    /// ambient sounds or music that should loop thoughout this level
    /// </summary>
    [Tooltip("audio clips that should loop thoughout the level.")]
    public AudioClip[] m_looping_clips;
    // private cointainer to hold the resulting audio sources for looping sounds (2d and 3d)
    private List<AudioSource> m_looping_sources;

    /// <summary>
    /// the audio volume for all of the clips that should loop throughout the scene
    /// </summary>
    [Tooltip("volume level for all of the looping clips")]
    [Range(0.0f, 1.0f)]
    public float m_looping_volume = 0.2f;

    [Header("Audio Settings")]
    /// <summary>
    /// whether the maximum number of sounds that can play at once should be limited or not. If so, limited to 'max_sources' value
    /// </summary>
    [Tooltip("whether the maximum number of sounds that can play at once should be limited or not. If so, limited to 'max_sources' value")]
    public bool m_limit_sources = false;

    /// <summary>
    /// the maximum number of audio clips that will play at the same time. when reached, then the earliest ones will be removed first
    /// </summary>
    [Tooltip("maximum number of audio clips that can be played at once. Does not include looping clips")]
    public int m_max_sources = 15;

    // simple list that saves all of the audiosources that are being played. Only used when max number of playing sources is limited
    private List<AudioSource> m_playing_sources;

    // memorize the index at which the next sound will be played. only relevant ig max number of sources enabled
    private int m_next_play_source = 0;

    #endregion

    #region UNITY LIFECYCLE

    // setup the singleton properties
    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(this);
        }

        m_looping_sources = new List<AudioSource>();
        m_playing_sources = new List<AudioSource>(m_max_sources);

        // start playing all of the looping sounds. Since in awake, we can reasonably assume they are ambient sounds
        play_2d_clips(m_looping_clips, m_looping_volume, true, 1000.0f);
    }

    #endregion

    #region AUDIO FUNCTIONS

    /// <summary>
    /// takes a list of clips and plays them as 2d sounds
    /// </summary>
    /// <param name="n_clips">array of audioclips</param>
    /// <param name="n_volume">volume to play all the clips at</param>
    /// <param name="n_loop">whether or not the clips should loop</param>
    /// <param name="n_max_distance">the max distance the sounds can be heard</param>
    public void play_2d_clips(AudioClip[] n_clips, float n_volume = 1.0f, bool n_loop = false, float n_max_distance = 100.0f)
    {
        foreach (AudioClip clip in n_clips)
        {
            play_2d_clip(clip, n_volume, n_loop, n_max_distance);
        }
    }

    /// <summary>
    /// play a single 3d audio source at a given location
    /// </summary>
    /// <param name="n_clip">the clip that will be played. will be played a single time</param>
    /// <param name="n_position">the world position at which the sound will be played</param>
    public void play_3d_clip(AudioClip n_clip, Vector3 n_position, float n_volume = 1.0f, bool n_loop = false, float n_max_distance = 10.0f)
    {
        GameObject new_sound = new GameObject(n_clip.name + "_clip object");
        new_sound.transform.position = n_position;
        new_sound.transform.SetParent(this.transform);

        AudioSource source = new_sound.AddComponent<AudioSource>();
        source.volume = n_volume;
        source.spatialBlend = 1.0f;
        source.playOnAwake = false;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.maxDistance = n_max_distance;
        source.loop = n_loop;
        source.clip = n_clip;

        if (!n_loop)
        {
            Lifetime life = new_sound.AddComponent<Lifetime>();
            life.Time = n_clip.length + 1.0f;

            add_to_limited_sources(source);
        }
        else
        {
            m_looping_sources.Add(source);
        }

        source.Play();
    }

    /// <summary>
    /// plays a single audioclip as a 2d sound
    /// </summary>
    /// <param name="n_clip">the audioclip that should be palyed</param>
    /// <param name="n_volume">the colume at which the sound should be palyed</param>
    /// <param name="n_loop">whether or not the clip should be looped</param>
    /// <param name="n_max_distance">the maximum distance that the sound will be heard</param>
    public void play_2d_clip(AudioClip n_clip, float n_volume = 1.0f, bool n_loop = false, float n_max_distance = 100.0f)
    {
        GameObject new_sound = new GameObject(n_clip.name + "_clip object");
        new_sound.transform.SetParent(this.transform);

        AudioSource source = new_sound.AddComponent<AudioSource>();
        source.spatialBlend = 0.0f;
        source.volume = n_volume;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.maxDistance = n_max_distance;
        source.loop = n_loop;
        source.clip = n_clip;
        source.playOnAwake = false;

        if (!n_loop)
        {
            Lifetime life = new_sound.AddComponent<Lifetime>();
            life.Time = n_clip.length + 1.0f;

            add_to_limited_sources(source);
        }
        else
        {
            m_looping_sources.Add(source);
        }

        Debug.Log("playing sound");
        source.Play();
    }

    /// <summary>
    /// add a given audiosource to our list of limited sources. will circularly replace older sources when limit is being hit and no empty indices exist
    /// </summary>
    /// <param name="n_source">the source that shall be added</param>
    private void add_to_limited_sources(AudioSource n_source)
    {
        // if we do not even have the limitation turned on, we don't do shit here
        if (!m_limit_sources)
        {
            return;
        }

        // find an empty index, if there is one
        int index = m_next_play_source;
        for (int i = 0; i < m_playing_sources.Count; i++)
        {
            if (m_playing_sources[i] == null)
            {
                index = i;
            }
        }

        // if we had an empty slot that was not the next intended index, then we do not move the circular buffer forward
        m_next_play_source = (index == m_next_play_source) ? ++m_next_play_source % m_max_sources : m_next_play_source;
        
        // if the list already has the maximum amount OR if we want to access an empty slot within the exisitng number of slots
        // then use exisiting list entries, otherwise add a new one
        if (m_playing_sources.Count >= m_max_sources || m_playing_sources.Count > index)
        {
            if (m_playing_sources[index] != null)
            {
                Destroy(m_playing_sources[index].gameObject);
            }
            m_playing_sources[index] = n_source;
        }
        else
        {
            m_playing_sources.Add(n_source);
        }
    }

    #endregion
}
