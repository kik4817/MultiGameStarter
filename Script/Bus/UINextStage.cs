using TMPro;
using Unity.Netcode;
using UnityEngine;
using Essence;
using System.Collections;

public class UINextStage : NetworkBehaviour
{
    [Header("Next Stage")]
    [SerializeField] private GameObject uiPanel; // 이벤트 실행할때 활성화
    [SerializeField] private TextMeshProUGUI countDownText; // 남은 시간에 맞추어 변경
    [SerializeField] private int remainCount = 3;
    [SerializeField] private string loadSceneName = "Game2";

    private void Start()
    {
        uiPanel.SetActive(false);
    }

    public override void OnNetworkSpawn()
    {
        if(!IsServer) { return; }

        Bus<IStageClearEvent>.Onevent += HandleStageClear;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer) { return; }

        Bus<IStageClearEvent>.Onevent -= HandleStageClear;
    }

    private void HandleStageClear(IStageClearEvent evt)
    {
        StageClearClientRpc();
    }

    [Rpc(SendTo.Everyone)]
    private void StageClearClientRpc()
    {
        uiPanel.SetActive(true);       
        UICountDown();
    }

    IEnumerable UICountDown()
    {
        int loopCount = remainCount;
        for (int i = 0; i < loopCount; i++)
        {
            countDownText.SetText(remainCount.ToString());
            remainCount--;
            yield return new WaitForSeconds(1);

        }

        NetworkManager.SceneManager.LoadScene(loadSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
