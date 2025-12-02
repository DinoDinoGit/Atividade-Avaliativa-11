using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Moedas")]
    public int totalMoedas;       // Quantidade total EXISTENTE no mapa
    public int moedasColetadas;   // Quantas o player já pegou

    [Header("Pontos")]
    public int pontos;
    public int pontosPorMoeda = 50;

    [Header("UI")]
    public TextMeshProUGUI moedasText;
    public TextMeshProUGUI pontosText;

    [Header("Portão")]
    public GateController gate;   // Referência ao portão

    private void Start()
    {
        AtualizarUI();
    }

    public void ColetarMoeda()
    {
        moedasColetadas++;
        pontos += pontosPorMoeda;

        AtualizarUI();

        if (moedasColetadas >= totalMoedas)
        {
            gate.AbrirPortao();
        }
    }

    void AtualizarUI()
    {
        if (moedasText != null)
            moedasText.text = "Moedas: " + moedasColetadas + "/" + totalMoedas;

        if (pontosText != null)
            pontosText.text = "Pontos: " + pontos;
    }
}
