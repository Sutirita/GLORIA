using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace LCBM.Core.Console
{


        internal class ConsolePanel : MonoBehaviour
        {

                private GameObject consoleCanvas;

                public ScrollRect scrollRect;

                private Text consoleHistoryText;

                private Text placeholderText;

                private InputField inputField;


                private bool isOpen = false;
                public bool IsOpen => isOpen;


                private static ConsolePanel _instance;

                public static ConsolePanel Instance
                {
                        get
                        {
                                if (_instance is null) _instance = new ConsolePanel();
                                return _instance;
                        }
                }

                void Awake()
                {
                        CreateConsoleUI();
                        consoleCanvas.SetActive(false);
                }



                void Update()
                {
                        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Backspace)) ToggleConsole();

                        if (!isOpen) return;

                        if (Input.GetKeyDown(KeyCode.Escape)) ToggleConsole();
                       
                    

                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                                string value = Console.Instance.ViewInputHistoryUP();
                                if (string.IsNullOrEmpty(value)) return;
                                inputField.text = value;
                                inputField.Select();
                                

                        }
                        if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                                string value = Console.Instance.ViewInputHistoryDown();
                                if (string.IsNullOrEmpty(value)) return;
                                inputField.text = value;
                                inputField.Select();
                        }





                }


                public void SetPlaceholderText(string value)
                {
                        placeholderText.text = value;
                }


                void ToggleConsole()
                {
                        isOpen = !isOpen;
                        consoleCanvas.SetActive(isOpen);
                        if (isOpen)
                        {
                                inputField.ActivateInputField(); // 自动聚焦输入框
                        }
                        else
                        {
                                EventSystem.current.SetSelectedGameObject(null); // 释放焦点
                        }
                }

                void CreateConsoleUI()
                {
                        //background
                        consoleCanvas = new GameObject("RootCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                        consoleCanvas.transform.SetParent(this.transform, true);
                        Canvas canvas = consoleCanvas.GetComponent<Canvas>();
                        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                        canvas.sortingOrder = 1000;

                        CanvasScaler scaler = consoleCanvas.GetComponent<CanvasScaler>();
                        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                        scaler.referenceResolution = new Vector2(Screen.height, Screen.width);

                        GameObject panelObj = new GameObject("BackgroundPanel", typeof(Image));
                        panelObj.transform.SetParent(consoleCanvas.transform, false);
                        Image panelImage = panelObj.GetComponent<Image>();
                        panelImage.color = new Color(0.2f, 0.2f, 0.2f, 0.7f);

                        RectTransform panelRect = panelObj.GetComponent<RectTransform>();
                        panelRect.anchorMin = Vector2.zero;
                        panelRect.anchorMax = Vector2.one;
                        panelRect.offsetMin = Vector2.zero;
                        panelRect.offsetMax = Vector2.zero;
                        

                        //history
                        GameObject scrollViewObj = new GameObject("HistoryScrollView", typeof(ScrollRect), typeof(Mask));
                        scrollViewObj.transform.SetParent(panelObj.transform, false);
                        ScrollRect scrollRect = scrollViewObj.GetComponent<ScrollRect>();
                        Mask mask = scrollViewObj.GetComponent<Mask>();
                        mask.showMaskGraphic = false;

                        scrollRect.scrollSensitivity = 30f;

                        RectTransform scrollRectTransform = scrollViewObj.GetComponent<RectTransform>();
                        scrollRectTransform.anchorMin = new Vector2(0, 0.1f); 
                        scrollRectTransform.anchorMax = new Vector2(1, 1);
                        scrollRectTransform.offsetMin = new Vector2(10, 10);
                        scrollRectTransform.offsetMax = new Vector2(-10, -10);

                        GameObject viewport = new GameObject("Viewport", typeof(RectTransform), typeof(Mask));
                        viewport.transform.SetParent(scrollViewObj.transform, false);
                        Mask viewportMask = viewport.GetComponent<Mask>();
                        viewportMask.showMaskGraphic = false;
                        RectTransform viewportRect = viewport.GetComponent<RectTransform>();
                        viewportRect.anchorMin = Vector2.zero;
                        viewportRect.anchorMax = Vector2.one;
                        viewportRect.offsetMin = Vector2.zero;
                        viewportRect.offsetMax = Vector2.zero;


                        GameObject content = new GameObject("Content", typeof(RectTransform));
                        content.transform.SetParent(viewport.transform, false);
                        RectTransform contentRect = content.GetComponent<RectTransform>();
                        contentRect.anchorMin = new Vector2(0, 0);      
                        contentRect.anchorMax = new Vector2(1, 0);      
                        contentRect.pivot = new Vector2(0.5f, 0);       
                        contentRect.offsetMin = Vector2.zero;
                        contentRect.offsetMax = Vector2.zero;

                        GameObject historyTextObj = new GameObject("HistoryText", typeof(Text));
                        historyTextObj.transform.SetParent(content.transform, false);
                        consoleHistoryText = historyTextObj.GetComponent<Text>();
                        consoleHistoryText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                        consoleHistoryText.fontSize = 24;
                        consoleHistoryText.color = Color.white;
                        consoleHistoryText.alignment = TextAnchor.UpperLeft;
                        consoleHistoryText.horizontalOverflow = HorizontalWrapMode.Wrap;
                        consoleHistoryText.verticalOverflow = VerticalWrapMode.Overflow; 

                        RectTransform textRect = historyTextObj.GetComponent<RectTransform>();
                        textRect.anchorMin = Vector2.zero;
                        textRect.anchorMax = Vector2.one;
                        textRect.offsetMin = new Vector2(10, 10);
                        textRect.offsetMax = new Vector2(-10, -10);

                        scrollRect.viewport = viewportRect;
                        scrollRect.content = contentRect;
                        scrollRect.horizontal = false;
                        scrollRect.vertical = true;
                        scrollRect.movementType = ScrollRect.MovementType.Clamped;
                        this.scrollRect = scrollRect;



                        //input

                        GameObject inputFieldObj = new GameObject("InputField", typeof(Image), typeof(InputField));
                        inputFieldObj.transform.SetParent(panelObj.transform, false);
                        inputField = inputFieldObj.GetComponent<InputField>();
                        Image inputImage = inputFieldObj.GetComponent<Image>();
                        inputImage.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);


                        GameObject textObj = new GameObject("Text", typeof(Text));
                        textObj.transform.SetParent(inputFieldObj.transform, false);
                        Text textComponent = textObj.GetComponent<Text>();
                        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                        textComponent.fontSize = 24;
                        textComponent.color = Color.white;
                        textComponent.alignment = TextAnchor.MiddleLeft;
                        textComponent.supportRichText = false;
                        RectTransform textRect2 = textObj.GetComponent<RectTransform>();
                        textRect2.anchorMin = Vector2.zero;
                        textRect2.anchorMax = Vector2.one;
                        textRect2.offsetMin = new Vector2(10, 5);
                        textRect2.offsetMax = new Vector2(-10, -5);
                        inputField.textComponent = textComponent;
                        inputField.targetGraphic = inputImage;

                        
                        GameObject placeholderObj = new GameObject("Placeholder", typeof(Text));
                        placeholderObj.transform.SetParent(inputFieldObj.transform, false);
                        placeholderText = placeholderObj.GetComponent<Text>();
                        placeholderText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                        placeholderText.fontSize = 24;
                        placeholderText.color = new Color(0.5f, 0.5f, 0.5f, 1);
                        placeholderText.alignment = TextAnchor.MiddleLeft;
                        RectTransform placeholderRect = placeholderObj.GetComponent<RectTransform>();
                        placeholderRect.anchorMin = Vector2.zero;
                        placeholderRect.anchorMax = Vector2.one;
                        placeholderRect.offsetMin = new Vector2(10, 5);
                        placeholderRect.offsetMax = new Vector2(-10, -5);
                        inputField.placeholder = placeholderText;

                        RectTransform inputRect = inputFieldObj.GetComponent<RectTransform>();
                        inputRect.anchorMin = new Vector2(0, 0);
                        inputRect.anchorMax = new Vector2(1, 0.1f);
                        inputRect.offsetMin = new Vector2(10, 10);
                        inputRect.offsetMax = new Vector2(-10, -5);

                        inputField.onEndEdit.AddListener((string value) => InputListener(value));


                        SetPlaceholderText(LCBMStaticData.CONSOLE_PLACEHOLDER);










                }






                void InputListener(string value)
                {
                        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                        {
                                if (string.IsNullOrEmpty(value)) return;
                                Console.Instance.OnSubmit(value);
                                inputField.text = "";
                                inputField.ActivateInputField();
                                Canvas.ForceUpdateCanvases();
                                scrollRect.verticalNormalizedPosition = 0f;
                                UpdateConsoleHistory(Console.Instance.LogEntrys);

                        }
                }



                public void UpdateConsoleHistory(List<LogEntry> logs)
                {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < logs.Count; i++)
                        {
                                string RichText = ConsoleTools.LogEntryToRichText(logs[i]);
                                sb.Append(RichText);
                                if (i < logs.Count - 1)
                                        sb.Append('\n'); 
                        }
                        consoleHistoryText.text = sb.ToString();

                        Canvas.ForceUpdateCanvases();
                        LayoutRebuilder.ForceRebuildLayoutImmediate(consoleHistoryText.rectTransform);

                        float preferredHeight = consoleHistoryText.preferredHeight + 20f;
                        RectTransform contentRect = consoleHistoryText.rectTransform.parent.GetComponent<RectTransform>();
                        contentRect.sizeDelta = new Vector2(0, preferredHeight);
                        contentRect.anchoredPosition = Vector2.zero;
                        Canvas.ForceUpdateCanvases();
                        scrollRect.verticalNormalizedPosition = 0f;
                }
        }

}
