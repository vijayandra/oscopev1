#include <stdio.h>
int main()
{
    int i;
    int j;
    j=0;
    i=0;
    for(j=200;j<4095;j+=50)
    {
    mysignal(0,1,0,j,0,4095-j,1000,0,1000);
        ms_sleep(100);

    }
    return 0;
}
