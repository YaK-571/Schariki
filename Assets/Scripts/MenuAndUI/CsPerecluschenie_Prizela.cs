using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsPerecluschenie_Prizela : MonoBehaviour
{
    [SerializeField] bool _muschka = false;
    [SerializeField] int tip_prizela;
    [SerializeField] int nomer_dostischenija;

    bool razblokirovka = false;
    private void Start()
    {
        Progress MyGameInstance = Progress.GameInstance;
        //Progress.GameInstance.date._prizel_mushka;
        int ispolzovano_usilenij = MyGameInstance.date.ispolzovano_usilenij_minigun +
                    MyGameInstance.date.ispolzovano_usilenij_art +
                    MyGameInstance.date.ispolzovano_usilenij_schit +
                    MyGameInstance.date.ispolzovano_usilenij_samoroska;
        Debug.Log("»спользовано усилений: " + ispolzovano_usilenij);

        switch (nomer_dostischenija)
        {
            
            case 0: razblokirovka = true; break;
            case 1:
                if (MyGameInstance.date.ispolzovano_usilenij_minigun > 0 &&
                    MyGameInstance.date.ispolzovano_usilenij_art > 0 &&
                    MyGameInstance.date.ispolzovano_usilenij_schit > 0 &&
                    MyGameInstance.date.ispolzovano_usilenij_samoroska > 0)
                { razblokirovka = true; }
                break;

            case 2:
                if (MyGameInstance.date.sbito_gruza_na_nitke >= 25)
                { razblokirovka = true; }
                break;
            case 3:
                if (MyGameInstance.date.pokupok >0)
                { razblokirovka = true; }
                break;
            case 4:
                if (MyGameInstance.date.popadanij_v_zentr >= 10)
                { razblokirovka = true; }
                break;
            case 5:
                if (ispolzovano_usilenij >= 2)
                { razblokirovka = true; }
                break;
            case 6:
                if (ispolzovano_usilenij >= 15)
                { razblokirovka = true; }
                break;
            case 7:
                if (ispolzovano_usilenij >= 25)
                { razblokirovka = true; }
                break;
            case 8:
                if (ispolzovano_usilenij >= 100)
                { razblokirovka = true; }
                break;
            case 9:
                if (MyGameInstance.date.Coin_record[0] >= 10000)
                { razblokirovka = true; }
                break;
            case 10:
                if (MyGameInstance.date.Coin_record[1] >= 10000)
                { razblokirovka = true; }
                break;
            case 11:
                if (MyGameInstance.date.Coin_record[2] >= 10000)
                { razblokirovka = true; }
                break;
            case 12:
                if (MyGameInstance.date.Coin >= 1000)
                { razblokirovka = true; }
                break;
            case 13:
                if (MyGameInstance.date.Coin >= 10000)
                { razblokirovka = true; }
                break;
            case 14:
                if (MyGameInstance.date.Coin >= 50000)
                { razblokirovka = true; }
                break;
            case 15:
                if (MyGameInstance.date.Coin >= 100000)
                { razblokirovka = true; }
                break;
            case 16:
                if (MyGameInstance.date.Coin >= 500000)
                { razblokirovka = true; }
                break;
            case 17:
                if (MyGameInstance.date.Coin >= 1000000)
                { razblokirovka = true; }
                break;
            case 18:
                if (MyGameInstance.date.sbito_scharikov_za_5_sek >= 1)
                { razblokirovka = true; }
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
