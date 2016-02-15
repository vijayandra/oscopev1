// Please note :
// this is script "C"
// Do not compile this code, it should run as it is
// uses: Run this code which GUI is up and connected to HW ( Green LED is On)
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
        // following code produces signal which increasing amplitude
        // and other decreasing amplitude
        // Cl_signal
        // Arg 1 : TMR
        // Arg 2 : TMRDIV
        // Arg 3 : Number of Element
        // Arg 4 : Sig1 Type [ 0 SIN,1 COS, 2 SQUARE, 3 Rectgle, 4 Triangular,5 Trgn Sym1,6 Trng Sym2
        // Arg 5 : Sig1 Min
        // Arg 6 : Sig1 Max
        // Arg 7 : Sig2 Type [ 0 SIN,1 COS, 2 SQUARE, 3 Rectgle, 4 Triangular,5 Trgn Sym1,6 Trng Sym2
        // Arg 8 : Sig2 Min
        // Arg 9 : Sig2 Max
        for(i=0;i<5000;i+=10)
        {
            Cl_signal(tmr,tmrdiv,numElem,sig1type,sig1min,i,sig2type,sig2min,5000-i);
            ClWait(100);
        }
    }
    return 0;
}
