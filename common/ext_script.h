#ifndef __EXTERNAL_SCRIPT_H
#define __EXTERNAL_SCRIPT_H

#include <qapplication.h>
#include <QtGui>



#ifdef CREATE_IMPROC_LIB
    #define SCRIPT_LIB_FUNC  Q_DECL_EXPORT
#else
    #define SCRIPT_LIB_FUNC Q_DECL_IMPORT
#endif

#ifdef _WIN32
#define PREFIX extern "C" SCRIPT_LIB_FUNC
#else
#define PREFIX extern "C"
#endif

PREFIX unsigned short TransmitTag();
PREFIX unsigned short WaitTag(unsigned short mSleep);
PREFIX int            lStart(void);
PREFIX bool           lStop(void);


PREFIX void           OnStart();
PREFIX void           OnStartIP();
PREFIX void           OnExit();

PREFIX void           _script_wait(int iwait_time);
PREFIX void           _script_version();
PREFIX unsigned short _script_wait_cmd(int iwait_time);
PREFIX unsigned short _gpio_rcmd(unsigned short i);
PREFIX unsigned short _gpio_wcmd(unsigned short i,unsigned short j);
PREFIX unsigned short __test_write(unsigned char *chptr,unsigned char len);
PREFIX unsigned short __test_read(unsigned char *chptr,unsigned char len);
PREFIX unsigned char start_CtrlFunction();

PREFIX void          startRemoteAccess(void *p,unsigned short ms);
PREFIX void          register_CtrlFunction(void (*p)(int));
PREFIX void          _start_server_ip(void *a);
PREFIX void          _stop_server_ip();
PREFIX int           _start_client_ip(void *a);
PREFIX void          _stop_client_ip();
PREFIX int           _is_server_alive(int ms);
PREFIX void          _cl_encode_msg(unsigned short cnstMsg,unsigned char *buff);
PREFIX void          _cl_wait_done();
PREFIX int           millisleep(unsigned long ms);

PREFIX unsigned short usCommandStatus(unsigned short cmdx);
PREFIX unsigned char  lPushTag(char cmdx,unsigned char *ucbuff,unsigned char len);
PREFIX unsigned short lPullTag(unsigned char *ucbuff);
PREFIX unsigned char  lusPushTag(char cmdx,unsigned short lm);

PREFIX void          _script_signal(unsigned short   numPoint,  \
                                   unsigned short   tmr,       \
                                   unsigned short   tmrdiv,    \
                                   unsigned short   h1Type,    \
                                   unsigned short   h2Type,    \
                                   unsigned short   ch1Max,    \
                                   unsigned short   ch1Min,    \
                                   unsigned short   ch2Max,    \
                                   unsigned short   ch2Min);


#if _WIN32
PREFIX void RegisterWin(HWND hwnd);
#else
PREFIX void RegisterWin(int hwnd);
#endif

#endif
