#include<iostream>
#include<stdio.h>
#include <stddef.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <sys/time.h>
#include <unistd.h>
#include <QMutex>
#include <QThread>

#include "ext_hscript.h"
#include "ext_script.h"
#include "i2c_display.h"

using namespace std;

//#define EXAMPLE_SIGNAL
//#define EXAMPLE_SCOPE
#define GPIO_CTRL
//#define EXAMPLE_I2C

void ext_main(int x)
{
    int i_max=0;
    int i_min=0;
    unsigned short k=0;
    unsigned char  byteArr0[20];
    char buffer[20];


    //printf("balle balle %d\n",x);
    ModeData m;

    memset(m._uc_buff,'\0',40);

    while(1)
    {
#ifdef GPIO_CTRL
        // I2C messages are treated in special way
        // Create a new information to deliver to Scope hardware
        lPushTag(INIT_TAG,NULL,0);

        byteArr0[0]=ID_CH00;
        lPushTag(DIR_OUT,byteArr0,1);

        byteArr0[0]=ID_CH09;
        lPushTag(DIR_OUT,byteArr0,1);

        byteArr0[0]=ID_CH08;
        lPushTag(DIR_OUT,byteArr0,1);

        _cl_encode_msg(ITAG_CTRL,m._uc_buff);
        _cl_wait_done();

        while(1)
        {
            byteArr0[0]=ID_CH08;
            byteArr0[1]=1;

            lPushTag(INIT_TAG,NULL,0);
            lPushTag(GPIO_WRITE,byteArr0,2);
            _cl_encode_msg(ITAG_CTRL,m._uc_buff);
            _cl_wait_done();
            millisleep(100);

            byteArr0[0]=ID_CH09;
            byteArr0[1]=1;

            lPushTag(INIT_TAG,NULL,0);
            lPushTag(GPIO_WRITE,byteArr0,2);
            _cl_encode_msg(ITAG_CTRL,m._uc_buff);
            _cl_wait_done();
            millisleep(100);

            byteArr0[0]=ID_CH08;
            byteArr0[1]=0;

            lPushTag(INIT_TAG,NULL,0);
            lPushTag(GPIO_WRITE,byteArr0,2);
            _cl_encode_msg(ITAG_CTRL,m._uc_buff);
            _cl_wait_done();
            millisleep(100);

            byteArr0[0]=ID_CH09;
            byteArr0[1]=0;

            lPushTag(INIT_TAG,NULL,0);
            lPushTag(GPIO_WRITE,byteArr0,2);
            _cl_encode_msg(ITAG_CTRL,m._uc_buff);
            _cl_wait_done();
            millisleep(100);

        }

#endif
        //millisleep(1000);
#ifdef EXAMPLE_I2C

        // I2C messages are treated in special way
        // Create a new information to deliver to Scope hardware
        lPushTag(INIT_TAG,NULL,0);

        // Initialize I2C2 Hardware, 0 => 4000 Hz I2C Clock
        lPushTag(I2C2SPEED,NULL,0);

        // transmit package to hardware
        _cl_encode_msg(ITAG_CTRL,m._uc_buff);
        // wait until done
        _cl_wait_done();

        // Create a new packet
        lPushTag(INIT_TAG,NULL,0);
        // Add I2C2 Start
        lPushTag(I2C2START,NULL,0);
        // Add I2C2 device Address
        byteArr0[0]= I2C_DISPLAY_ADDR;
        lPushTag(I2C2WRITEADDR,byteArr0,1);


        // Add Command (As per matrix orbital data sheet)
        byteArr0[0]= I2C_COMMAND;
        byteArr0[1]= CLEAR_DISPLAY;
        // Add to package
        lPushTag(I2C2DATA,byteArr0,2);
        // Add I2C2 Stop
        lPushTag(I2C2STOP,NULL,0);
        ////////
        lPushTag(I2C2START,NULL,0);
        byteArr0[0]= I2C_DISPLAY_ADDR;
        lPushTag(I2C2WRITEADDR,byteArr0,1);

        sprintf(buffer,"#Counter=%d",k++);
        //lPushTag(I2C2DATA,(unsigned char *)"SINGHVijayandraKumar",20);
        lPushTag(I2C2DATA,(unsigned char *)buffer,strlen(buffer));
        lPushTag(I2C2DATA,(unsigned char *)buffer,strlen(buffer));
        lPushTag(I2C2STOP,NULL,0);
        _cl_encode_msg(ITAG_CTRL,m._uc_buff);
        _cl_wait_done();


#endif

#ifdef EXAMPLE_SIGNAL
        //m.tab.tabNum=1;
        //.._cl_encode_msg(ICHANGE_TAB,m._uc_buff);
        m.signal.TMR     = 800;
        m.signal.TMRDIV  = 1;
        m.signal.numElem = 400;

        m.signal.sig1type = 1;
        m.signal.sig1min  = 100;
        m.signal.sig1max  = 4400;

        m.signal.sig2type = 0;
        m.signal.sig2min  = 40;
        m.signal.sig2max  = 2900;

        _cl_encode_msg(ISIGNAL_CTRL,m._uc_buff);
        _cl_wait_done();
        millisleep(100);
        //_cl_wait_done();

        m.signal.TMR     = 800;
        m.signal.TMRDIV  = 1;
        m.signal.numElem = 400;

        m.signal.sig1type = 1;
        m.signal.sig1min  = 100;
        m.signal.sig1max  = 4400;

        m.signal.sig2type = 0;
        m.signal.sig2min  = 40;
        m.signal.sig2max  = 4400;

        _cl_encode_msg(ISIGNAL_CTRL,m._uc_buff);
        _cl_wait_done();
        millisleep(100);


        m.signal.TMR     = 800;
        m.signal.TMRDIV  = 2;
        m.signal.numElem = 400;

        m.signal.sig1type = 1;
        m.signal.sig1min  = 0;
        m.signal.sig1max  = 4000;

        m.signal.sig2type = 0;
        m.signal.sig2min  = 0;
        m.signal.sig2max  = 4000;

        _cl_encode_msg(ISIGNAL_CTRL,m._uc_buff);
        _cl_wait_done();
        millisleep(100);

        m.signal.TMR     = 800;
        m.signal.TMRDIV  = 2;
        m.signal.numElem = 400;

        m.signal.sig1type = 0;
        m.signal.sig1min  = 0;
        m.signal.sig1max  = 4500;

        m.signal.sig2type = 1;
        m.signal.sig2min  = 200;
        m.signal.sig2max  = 4900;

        _cl_encode_msg(ISIGNAL_CTRL,m._uc_buff);
        _cl_wait_done();
        millisleep(100);

#endif

#ifdef EXAMPLE_SCOPE
        m.signal.TMR     = 800;
        m.signal.TMRDIV  = 1;
        m.signal.numElem = 1000;
        //m.signal.actChnl = 3;

        m.signal.sig1max=4100;
        m.signal.sig1min=100;
        m.signal.sig1type=1;

        m.signal.sig2max=5000;
        m.signal.sig2min=10;
        m.signal.sig2type=2;

        _cl_encode_msg(ISIGNAL_CTRL,m._uc_buff);
        _cl_wait_done();

        m.scope.TMR     = 800;
        m.scope.TMRDIV  = 1;
        m.scope.numElem = 1000;
        m.scope.actChnl = 3;

        _cl_encode_msg(ISCOPE_CTRL,m._uc_buff);
        _cl_wait_done();
#endif
    }
}


int main(int argc, char *argv[])
{
    void (*foo)(int);

    QCoreApplication a(argc, argv);

    printf("\r\n");

    //lStart();
    millisleep(200);

    // Register local function, that to be called
    register_CtrlFunction(&ext_main);

    // Start TCP listener
    startRemoteAccess(&a,299);

    // Start local function in back ground
    start_CtrlFunction();

    a.exec();
    millisleep(2000);

    return 0;
}
