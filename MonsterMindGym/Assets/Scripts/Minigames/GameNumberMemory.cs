using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameNumberMemory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberDisplayText;
    [SerializeField] private TMP_InputField _numberInputField;
    [SerializeField] private Button _submitButton;

    private int _numberSize;
    private int _currentNumber;

    private void OnEnable()
    {
        _submitButton.onClick.RemoveAllListeners();
        _submitButton.onClick.AddListener(CheckSubmittedNumber);

        _numberInputField.onSelect.RemoveAllListeners();
        _numberInputField.onSelect.AddListener(HideNumber);

        _numberInputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        _numberSize = 0;

        GrowAndGenerateNumber();
    }
    private void CheckSubmittedNumber()
    {
        if (_numberInputField.text == null)
        {
            return;
        }

        if (_numberInputField.text == _currentNumber.ToString())
        {
            GrowAndGenerateNumber();
        }
        else
        {
            print("LOSE");
        }
    }
    private void GrowAndGenerateNumber()
    {
        _numberSize++;

        int minNumber = (int)Mathf.Pow(10, _numberSize - 1);
        int maxNumber = (int)Mathf.Pow(10, _numberSize) - 1;

        _currentNumber = Random.Range(minNumber, maxNumber + 1);

        _numberDisplayText.text = _currentNumber.ToString();
    }
    private void HideNumber(string arg)
    {
        _numberDisplayText.text = "???";
    }
}
