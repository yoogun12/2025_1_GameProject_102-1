using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTurnManager : MonoBehaviour
{
    //Àü¿ª º¯¼ö (¸ğµç °øÀÌ °øÀ¯)
    public static bool canPlay = true;                              //°øÀ» Ä¥ ¼ö ÀÖ´ÂÁö
    public static bool anyBallMoving = false;                       //¾î¶² °øÀÌ¶óµµ ¿òÁ÷ÀÌ´ÂÁö



    // Update is called once per frame
    void Update()
    {
        CheckAllBalls();                        //¸ğµç °øÀÇ ¿òÁ÷ÀÓ È®ÀÎ
        if(!anyBallMoving && !canPlay)          //¸ğµç °øÀÌ ¸ØÃß¸é ´Ù½Ã Ä¥ ¼ö ÀÖ°Ô ÇÔ
        {
            canPlay = true;
            Debug.Log("ÅÏ Á¾·á! ´Ù½Ã Ä¥ ¼ö ÀÖ½À´Ï´Ù. ");
        }
    }

    void CheckAllBalls()                    //¸ğµç °øÀÌ ¸Ø­Ÿ´ÂÁö È®ÀÎ
    {
        SimpleBallController[] allBalls = FindObjectsOfType<SimpleBallController>();         //¾À¿¡ ÀÖ´Â ¸ğµç °ø Ã£±â
        anyBallMoving = false;

        foreach(SimpleBallController ball in allBalls)
        {
            if(ball.IsMoving())
            {
                anyBallMoving = true;
                break;
            }
        }
    }

    public static void OnBallHit()          //°øÀ» ÇÃ·¹ÀÌ ÇßÀ» ¶§ È£Ãâ
    {
        canPlay = false;                    //´Ù¸¥ °øµéÀ» ¸ø ¿òÁ÷ÀÌ°Ô Èû
        anyBallMoving = true;
        Debug.Log("ÅÏ ½ÃÀÛ! °øÀÌ ¸ØÃâ ¶§ ±îÁö ±â´Ù¸®¼¼¿ä");
    }
}
