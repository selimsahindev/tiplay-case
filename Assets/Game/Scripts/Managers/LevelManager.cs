using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Singletons;
using Game.Core.Events;
using EventType = Game.Core.Enums.EventType;

namespace Game.Managers
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        [SerializeField] private int levelCount;
        [SerializeField] private int playLevel;

        public int ActiveLevelIndex { get; private set; }

        private void Awake()
        {
            if (!SetupInstance(false))
            {
                return;
            }

            ConstructLevel();
        }

        private void ConstructLevel()
        {
            ActiveLevelIndex = playLevel == -1 ? DataManager.Instance.Level : playLevel;

            Instantiate(Resources.Load<GameObject>("Levels/Level-" + ActiveLevelIndex));
        }

        private void LevelUp()
        {
            int level = DataManager.Instance.Level + 1;

            if (level > levelCount)
            {
                level = 1;
            }

            DataManager.Instance.SetLevel(level);
        }

        public void AddMoney(int amount)
        {
            if (amount > 0)
            {
                DataManager.Instance.SetMoney(DataManager.Instance.Money + amount);
                EventBase.NotifyListeners(EventType.MoneyUpdated);
            }
        }

        public void LoadScene()
        {
            ServiceProvider.ResetProvider();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void HandleGameOverEvent(bool status)
        {
            if (status)
            {
                LevelUp();
            }
        }

        private void OnEnable()
        {
            EventBase.StartListening(EventType.GameOver, HandleGameOverEvent);
        }

        private void OnDisable()
        {
            EventBase.StopListening(EventType.GameOver, HandleGameOverEvent);
        }
    }
}