// File: SizeButtonUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SizeButtonUI : MonoBehaviour
{
    public TextMeshProUGUI sizeText;
    private Button button;
    private DetailManager detailManager;
    private SizeInfo mySizeInfo; // Simpan seluruh objek SizeInfo

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners(); 
        button.onClick.AddListener(OnClick);
    }

    public void Setup(SizeInfo sizeInfo, DetailManager manager)
    {
        mySizeInfo = sizeInfo;
        sizeText.text = mySizeInfo.size;
        detailManager = manager;
    }

    void OnClick()
    {
        detailManager.OnSizeSelected(this);
    }
    
    public SizeInfo GetSizeInfo()
    {
        return mySizeInfo;
    }
    
    public void SetSelected(bool isSelected)
    {
        Image buttonImage = button.GetComponent<Image>();
        buttonImage.color = isSelected ? detailManager.selectedColor : detailManager.deselectedColor;
    }
}