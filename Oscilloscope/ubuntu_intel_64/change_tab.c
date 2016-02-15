// Please note :
// this is script "C"
// Do not compile this code, it should run as it is
// uses:
// $$ Prompt >> run_myscript change_tab.c
#include <stdio.h>

int main(void)
{
    printf("Change tab continous press Ctrl-C to exit\n");
    while(1)
    {
            Cl_tab(0);
            ClWait(1000);
            Cl_tab(1);
            ClWait(1000);
            Cl_tab(2);
            ClWait(1000);
    }

    return 0;
}
