#include <stdio.h>
int main()
{
    int i;
    int j;
    j=0;
    i=0;
    for(i=0;i<6;i++)
    for(j=0;j<16;j++)
    {
        setGUILed(i,j);
        LCDHex_Write((i+1),j);
        ms_sleep(300);
    }

    return 0;
}
