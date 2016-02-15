// Please note :
// this is script "C"
// Do not compile this code, it should run as it is
// uses:
// $$ Prompt >> run_myscript timechg_signal.c
#include <stdio.h>

int main(void)
{
    char a[20];
    int x=0;

    int tmr=500;
    int tmrdiv=0;
    int numElem=400;

    int sig1type=0;
    int sig1min=0;
    int sig1max=4000;

    int sig2type=0;
    int sig2min=0;
    int sig2max=4000;
    int i;

    printf("Change tab continous press Ctrl-C to exit\n");

    while(1)
    {
        for(i=0;i<5000;i+=10)
        {
            Cl_signal(tmr,tmrdiv,numElem,sig1type,sig1min,i,sig2type,sig2min,5000-i);
            ClWait(100);
        }
    }
    return 0;
}
