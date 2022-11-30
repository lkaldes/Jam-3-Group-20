using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Padlock : MonoBehaviour
{
    // public MouseControl MouseCntrl;

    [System.Serializable]
    public class Question<T>
    {
        public Button button;
        public T answer;
        private T current;
        public T Current
        {
            get
            {
                return current;
            }
            set
            {
                SetCurrent(value);
            }
        }

        public bool Correct()
        {
            return answer.Equals(current);
        }

        public void SetCurrent(T value)
        {
            current = value;
        }
    }

    public UnityEvent onUnlock;

    public List<Question<bool>> questions;

    private bool unlocked = false;
    public bool Unlocked
    {
        get
        {
            return unlocked;
        }
    }

    public void SetCurrent(int index, bool value)
    {
        if (index >= 0 && index < questions.Count)
        {
            Question<bool> question = questions[index];
            question.SetCurrent(value);
        }
    }

    public void ClickButton(int index)
    {
        // trying to think of a way to not need a button here

        Question<bool> question = questions[index];
        Button button = question.button;
        bool current = question.Current;

        ColorBlock colorBlock = button.colors;
        if (current)
        {
            Color color = new Color(255, 255, 255, 1);
            colorBlock.normalColor = color;
            colorBlock.highlightedColor = color;
            colorBlock.pressedColor = color;
        }
        else 
        {
            Color color = new Color(255, 0, 0, 1);
            colorBlock.normalColor = color;
            colorBlock.highlightedColor = color;
            colorBlock.pressedColor = color;
        }
        
        button.colors = colorBlock;

        question.Current = !current;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Unlocked)
        {
            bool pass = true;
            for (int i = 0; i < questions.Count; i++)
            {
                Question<bool> question = questions[i];
                if (!question.Correct())
                {
                    pass = false;
                    break;
                }
            }

            if (pass)
            {
                unlocked = true;
                OnUnlock();
            }
        }
    }

    void OnUnlock()
    {
        EventManager.Events.Trigger("kitchenPadlockUnlocked");
        onUnlock.Invoke();
    }
}
