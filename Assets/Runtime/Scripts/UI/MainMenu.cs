using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _playButton;
    [SerializeField] GameObject _aboutButton;

    [Header("Config")]
    [SerializeField] GameObject _footerContainer;
    [SerializeField] GameObject _configButton;
    [SerializeField] Vector3 _footerOffsetWhenConfigIsOpen;
    Vector3 _footerPosWhenConfigIsClosed;

    [Header("Audio")]
    [SerializeField] AudioHandler _audioHandler;
    [SerializeField] AudioClip _clickButtonFx;

    enum Screen {Main, Settings, About}
    Screen screen;

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    IEnumerator StartGameRoutine()
    {
        _audioHandler.PlaySFX(_clickButtonFx);
        yield return new WaitForSeconds(0.25f); // audio time
        SceneManager.LoadScene(SceneConstants.GameplayScene);
    }

    void Start()
    {
        OpenMainScreen();
        _footerPosWhenConfigIsClosed = _footerContainer.transform.localPosition;
    }

    public void OpenMainScreen()
    {
        _playButton.SetActive(true);
        _playButton.SetActive(true);
        _playButton.SetActive(true);
    }

    public void OpenSettings()
    {
        _footerContainer.transform.DOLocalMove(_footerPosWhenConfigIsClosed+_footerOffsetWhenConfigIsOpen, 0.35f);
    }

    public void OpenAbout()
    {

    }

    void OpenScreen(Screen screen)
    {
        _playButton.SetActive(screen == Screen.Main);
        _playButton.SetActive(screen == Screen.Main);
    }
}