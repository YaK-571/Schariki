using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CsPerecluschenie_Prizela : MonoBehaviour
{
    [SerializeField] bool _muschka = false;
    [SerializeField] int tip_prizela;
    [SerializeField] int nomer_dostischenija;
    [SerializeField] Image _ProgressBar;

    bool razblokirovka = false;
    private void Start()
    {
        Progress MyGameInstance = Progress.GameInstance;
        //Progress.GameInstance.date._prizel_mushka;
        int ispolzovano_usilenij = MyGameInstance.date.ispolzovano_usilenij_minigun +
                    MyGameInstance.date.ispolzovano_usilenij_art +
                    MyGameInstance.date.ispolzovano_usilenij_schit +
                    MyGameInstance.date.ispolzovano_usilenij_samoroska;
        Debug.Log("Использовано усилений: " + ispolzovano_usilenij);

        int ispolzovano_raznuh_usilenij = 0;

        if (MyGameInstance.date.ispolzovano_usilenij_minigun > 0) ispolzovano_raznuh_usilenij++;
        if (MyGameInstance.date.ispolzovano_usilenij_art > 0) ispolzovano_raznuh_usilenij++;
        if (MyGameInstance.date.ispolzovano_usilenij_schit > 0) ispolzovano_raznuh_usilenij++;
        if (MyGameInstance.date.ispolzovano_usilenij_samoroska > 0) ispolzovano_raznuh_usilenij++;

        switch (nomer_dostischenija)
        {

            case 0:
                _ProgressBar.fillAmount = 1;
                razblokirovka = true; break;
            case 1:
                if (ispolzovano_raznuh_usilenij >= 4)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = ispolzovano_raznuh_usilenij / 4f; }
                break;

            case 2:if (MyGameInstance.date.sbito_gruza_na_nitke >= 25)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else
                { _ProgressBar.fillAmount = MyGameInstance.date.sbito_gruza_na_nitke / 25f; }
                break;
            case 3:
                if (MyGameInstance.date.pokupok > 0)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else
                {_ProgressBar.fillAmount = MyGameInstance.date.pokupok / 1f;}
                break;
            case 4:
                if (MyGameInstance.date.popadanij_v_zentr >= 10)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.popadanij_v_zentr / 10f; }
                break;
            case 5:
                if (ispolzovano_usilenij >= 2)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = ispolzovano_usilenij / 2f; }
                break;
            case 6:
                if (ispolzovano_usilenij >= 15)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = ispolzovano_usilenij / 15f; }
                break;
            case 7:
                if (ispolzovano_usilenij >= 25)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = ispolzovano_usilenij / 25f; }
                break;
            case 8:
                if (ispolzovano_usilenij >= 100)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = ispolzovano_usilenij / 100f; }
                break;
            case 9:
                if (MyGameInstance.date.Coin_record[0] >= 10000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin_record[0] / 10000f; }
                break;
            case 10:
                if (MyGameInstance.date.Coin_record[1] >= 10000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin_record[1] / 10000f; }
                break;
            case 11:
                if (MyGameInstance.date.Coin_record[2] >= 10000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin_record[2] / 10000f; }
                break;
            case 12:
                if (MyGameInstance.date.Coin >= 1000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin / 1000f; Debug.Log("Коинов: "+ MyGameInstance.date.Coin / 1000f); }
                break;
            case 13:
                if (MyGameInstance.date.Coin >= 10000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin / 10000f; }
                break;
            case 14:
                if (MyGameInstance.date.Coin >= 50000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin / 50000f; }
                break;
            case 15:
                if (MyGameInstance.date.Coin >= 100000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin / 100000f; }
                break;
            case 16:
                if (MyGameInstance.date.Coin >= 500000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin / 500000f; }
                break;
            case 17:
                if (MyGameInstance.date.Coin >= 1000000)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.Coin / 1000000f; }
                break;
            case 18:
                if (MyGameInstance.date.sbito_scharikov_za_5_sek >= 1)
                { _ProgressBar.fillAmount = 1; razblokirovka = true; }
                else { _ProgressBar.fillAmount = MyGameInstance.date.sbito_scharikov_za_5_sek / 1f; }
                break;
            default:
                // код если ни один case не подошел
                break;
        }
    }

    public void smena_prizela()
    {
        if (razblokirovka)
        {
            if (_muschka)
            {
                Progress.GameInstance.Set_prizel_mushka(tip_prizela);
            }
            else
            {
                Progress.GameInstance.Set_prizel_contur(tip_prizela);
            }
        }
    }
}
