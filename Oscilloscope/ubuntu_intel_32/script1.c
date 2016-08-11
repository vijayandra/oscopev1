#include <stdio.h>
int main()
{
    int i;
    int j;
    j=0;
    i=0;
    while(1)
    {
        if(i<2) i++;
        else    i=0;
        setCurrTab(i);
        ms_sleep(1000);
        j++;
        if(j>10) return 10;
        printf("i=%d j=%d\n",i,j);
    }
    return 0;
}
