using Game_Assets.Scripts.Utility;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game_Assets.Scripts.UI
{
    public class QuestUIController : MonoBehaviour
    {
        private GroupBox _choicesGroup;
        private Story _currentStory;
        private GroupBox _nextButton;
        private Label _questText;
        private GroupBox _questUi;
        private bool _storySet;
        private UIDocument document;

        private void Awake()
        {
            document = GetComponent<UIDocument>();
            var root = document.rootVisualElement;
            _questUi = root.Q<GroupBox>("quest-ui");
            _questText = root.Q<Label>("quest-text");
            _nextButton = root.Q<GroupBox>("quest-next-button");
            _choicesGroup = root.Q<GroupBox>("quest-choices-buttons");
            _questUi.visible = false;
        }

        private void OnEnable()
        {
            EventManager.OnDispatchQuest += HandleQuest;
            EventManager.OnDispatchLeaveQuest += HandleLeaveQuest;
        }

        private void OnDisable()
        {
            EventManager.OnDispatchQuest += HandleQuest;
            EventManager.OnDispatchLeaveQuest += HandleLeaveQuest;
        }

        private void HandleQuest(TextAsset story)
        {
            _currentStory = new Story(story.text);
            _questText.text = _currentStory.Continue();
            _questUi.visible = true;
        }

        private void HandleLeaveQuest()
        {
            _questUi.visible = false;
        }
    }
}