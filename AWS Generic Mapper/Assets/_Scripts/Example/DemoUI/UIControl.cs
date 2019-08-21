using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DynamoDBMapper.Backend.Service;

public class UIControl : MonoBehaviour
{
    private const string AddSimplePlayerName = "Add_Simple_Player";
    private const string AddComplexPlayerName = "Add_Complex_Player";
    private const string LoadSimplePlayerName = "Load_Simple_Player";
    private const string LoadComplexPlayerName = "Load_Complex_Player";

    public void ButtonPressed(Button button)
    {
        PlayerHeroBackendService service = new PlayerHeroBackendService();
        switch (button.name)
        {
            case AddSimplePlayerName:
                service.AddSimplePlayers();
                break;
            case AddComplexPlayerName:
                service.AddComplexPlayers();
                break;
            case LoadSimplePlayerName:
                service.LoadSimplePlayers();
                break;
            case LoadComplexPlayerName:
                service.LoadComplexPlayers();
                break;
        }
    }
}
