using UnityEngine;
using UnityEngine.UI;
using Photon.Pun.UserInterfaces;

public class CharacterCustomizePanel : BasePunUserInterface
{
    [SerializeField]
    private Button[] _colorButtons;
    [SerializeField]
    private Button[] _accessoriesButtons;
    [SerializeField]
    private Color[] _colors;
    [SerializeField]
    private Button _closeButton;

    private CharacterCustomizeController _characterCustomizeController;

    protected override void SetData()
    {
        _closeButton.onClick.AddListener(OnHide);
        
        for (int i = 0; i < _colorButtons.Length; i++)
        {
            int index = i;
            _colorButtons[i].onClick.AddListener(() => OnClickColorButton(index));
        }

        for (int j = 0; j < _accessoriesButtons.Length; j++)
        {
            int index = j;
            _accessoriesButtons[j].onClick.AddListener(() => OnClickAccessoriesButton(index));
        }
    }

    private void OnClickColorButton(int index)
    {
        _characterCustomizeController.SetColor(_colors[index]);
    }

    private void OnClickAccessoriesButton(int index)
    {
        _characterCustomizeController.SetAccessories(index);
    }

    private void Start()
    {
        foreach (CharacterCustomizeController characterCustomizeController in FindObjectsByType<CharacterCustomizeController>(FindObjectsSortMode.InstanceID))
        {
            if (characterCustomizeController.photonView.IsMine)
            {
                _characterCustomizeController = characterCustomizeController;
                return;
            }
        }
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }

    private void OnHide()
    {
        Debug.Log("HIDE");
        Hide();
    }
}
