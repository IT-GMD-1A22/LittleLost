using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * When specified trigger event is fired, plays audio and runs through
 * enable, disable and destroy of added gameobjects.
 *
 * JH
 */
public class LevelTriggerManager : MonoBehaviour
{
    private LevelAudioPlayer _levelAudioPlayer;

    [Header("Trigger event")] [SerializeField]
    private TriggerEvent triggerEvent;

    [SerializeField] private List<AudioClip> onTriggerEventAudioClips;
    [SerializeField] private List<GameObject> shouldEnable;
    [SerializeField] private List<GameObject> shouldDisable;
    [SerializeField] private List<GameObject> shouldDestroy;


    [Header("Events")] [SerializeField] UnityEvent unityEvent;


    [Header("Settings")]
    // TODO: Maybe disablemovementonaudio can be its own script looking for when audio is being played on the audio manager
    // then it would work outside of trigger script also.
    // [SerializeField] private bool disableMovementOnAudio;
    [SerializeField]
    private bool handleCompleteEventExternally = false;

    [SerializeField] private float delayBeforeRunningEvent = 0.0f;
    [SerializeField] private bool waitForAudioFinish;
    [SerializeField] private bool interruptPreviousAudioBeforePlaying;
    [SerializeField] private bool destroySelfAfterEvent;
    [SerializeField] private bool printToConsoleOnCompletion = true;

    private void Awake()
    {
        _levelAudioPlayer = FindObjectOfType<LevelAudioPlayer>();
    }

    private void Play(AudioClip[] clips)
    {
        if (_levelAudioPlayer)
            _levelAudioPlayer.AddClipToQueue(clips, interruptPreviousAudioBeforePlaying);
    }


    private IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delayBeforeRunningEvent);
        Play(onTriggerEventAudioClips.ToArray());

        if (waitForAudioFinish)
            yield return new WaitForSeconds(_levelAudioPlayer.GetQueuePlayLength());

        if (!handleCompleteEventExternally)
            CompleteEvent();
    }

    public void CompleteEvent()
    {
        shouldEnable.ForEach(o => o.SetActive(true));
        shouldDisable.ForEach(o => o.SetActive(false));
        shouldDestroy.ForEach(Destroy);
        
        unityEvent.Invoke();

        if (printToConsoleOnCompletion)
            Debug.Log("Completed with " + gameObject.name);

        if (destroySelfAfterEvent)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (triggerEvent == TriggerEvent.START)
        {
            StartCoroutine(RunEvent());
        }
    }

    private void OnEnable()
    {
        if (triggerEvent == TriggerEvent.ENABLED)
        {
            StartCoroutine(RunEvent());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && triggerEvent == TriggerEvent.TRIGGER ||
            other.CompareTag("Boss") && triggerEvent == TriggerEvent.TRIGGER)
        {
            StartCoroutine(RunEvent());
        }
    }

    private enum TriggerEvent
    {
        ENABLED,
        START,
        TRIGGER
    }
}