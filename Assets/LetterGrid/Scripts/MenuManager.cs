using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject PauseGameMenuPanel;
    public GameObject GamePanel; // Your main game UI (score, timer, etc.)

    public Button startGameButton;
    public Button quitButton;
    public Button resumeButton;
    public Button toMainMenuButton;
    public Button backToMainMenuButton;
    public Button pauseButton;
    public Button settingsButton;

    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Toggle musicMuteToggle;
    public Toggle sfxMuteToggle;    


    private bool isPaused = false;

    void Awake() {
        if (startGameButton != null)
            startGameButton.onClick.AddListener(StartGame);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
        if (toMainMenuButton != null)
            toMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        if (backToMainMenuButton != null)
            backToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(SettingsMenu);
        if (pauseButton != null)
            pauseButton.onClick.AddListener(ShowInGameMenu);
        if (musicVolumeSlider != null)
            musicVolumeSlider.onValueChanged.AddListener(AudioEvents.RaiseMusicVolumeChanged);
        if (sfxVolumeSlider != null)
            sfxVolumeSlider.onValueChanged.AddListener(AudioEvents.RaiseSFXVolumeChanged);
        if (musicMuteToggle != null)
            musicMuteToggle.onValueChanged.AddListener(AudioEvents.RaiseMusicMuteChanged);
        if (sfxMuteToggle != null)
            sfxMuteToggle.onValueChanged.AddListener(AudioEvents.RaiseSFXMuteChanged);


    }

    private void Start() {
        ShowMainMenu();
    }

    private void OnEnable() {
        // Subscribe to init values from the AudioManager
        AudioEvents.OnMusicVolumeInit += HandleMusicVolumeInit;
        AudioEvents.OnSFXVolumeInit += HandleSFXVolumeInit;
        AudioEvents.OnMusicMuteInit += HandleMusicMuteInit;
        AudioEvents.OnSFXMuteInit += HandleSFXMuteInit;
    }

    private void OnDisable() {
        // Unsubscribe
        AudioEvents.OnMusicVolumeInit -= HandleMusicVolumeInit;
        AudioEvents.OnSFXVolumeInit -= HandleSFXVolumeInit;
        AudioEvents.OnMusicMuteInit -= HandleMusicMuteInit;
        AudioEvents.OnSFXMuteInit -= HandleSFXMuteInit;
    }

    private void HandleMusicVolumeInit(float v) => musicVolumeSlider.value = v;
    private void HandleSFXVolumeInit(float v) => sfxVolumeSlider.value = v;
    private void HandleMusicMuteInit(bool b) => musicMuteToggle.isOn = b;
    private void HandleSFXMuteInit(bool b) => sfxMuteToggle.isOn = b;

    // MAIN MENU
    public void ShowMainMenu() {
        mainMenuPanel.SetActive(true);
        PauseGameMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
    }

    public void ShowSettingsMenu() {
        mainMenuPanel.SetActive(false);
        PauseGameMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    // GAME START
    public void StartGame() {
        mainMenuPanel.SetActive(false);
        PauseGameMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
        AudioEvents.RaiseStartGame();
        GameManager.Instance.ResetGameTime();
        GameManager.Instance.StartNewRoundAtCurrentLevel();
    }

    // IN-GAME MENU
    public void ShowInGameMenu() {
        isPaused = true;
        GameManager.Instance.PauseGameTime();
        PauseGameMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    public void ResumeGame() {
        isPaused = false;
        PauseGameMenuPanel.SetActive(false);
        GamePanel.SetActive(true);
        GameManager.Instance.ResetGameTime();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ReturnToMainMenu() {
        // You might want to reset game state here
        ShowMainMenu();
    }

    public void SettingsMenu() {
        // You might want to reset game state here
        ShowSettingsMenu();
        AudioEvents.RequestInitSettings();
    }

}
