using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuiaDeUso : CustomMenu
{
    List<Pagina> paginas = new List<Pagina>();
    int currentPage;
    int maxPages;
    Pagina openPage;

    public override void Init()
    {
        base.Init();
        paginas = GameObject.FindObjectsOfType<Pagina>().ToList();
        paginas.Sort();
        foreach (Pagina p in paginas)
        {
            p.gameObject.SetActive(false);
            p.Init();
        }
        paginas[0].gameObject.SetActive(true);
        currentPage = 0;
        maxPages = paginas.Count - 1;
        openPage = paginas[0];
        GetButton("Next").OnPress += ()=>ChangePage(1);    
        GetButton("Previous").OnPress += ()=>ChangePage(-1);  
        ChangePage(0);
        }

    public new void Close()
    {
        base.Close();
        ChangePage(-currentPage);
    }

    public void ChangeLanguage()
    {
        foreach (Pagina p in paginas)
        {
            p.ChangeLanguage();
        }
    }

    public void ChangePage(int value)
    {
        currentPage = Mathf.Clamp(currentPage + value, 0, maxPages);
        if (currentPage == 0)
        {
            GetButton("Previous").Blocked = true;
        }
        else if (currentPage == maxPages)
        {
            GetButton("Next").Blocked = true;
        }
        else if (GetButton("Next").Blocked)
        {
            GetButton("Next").Blocked = false;
        }
        else if (GetButton("Previous").Blocked)
        {
            GetButton("Previous").Blocked = false;
        }
        openPage.gameObject.SetActive(false);
        openPage = paginas[currentPage];
        openPage.gameObject.SetActive(true);
        UnlockButtonsDelayed(0.5f);
    }
}
