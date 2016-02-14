#ifndef __EXTERNAL_HSCRIPT_H
#define __EXTERNAL_HSCRIPT_H

#define IW_WHITE       0
#define IW_BLACK       1
#define IW_RED         2
#define IW_DARKRED     3
#define IW_GREEN       4
#define IW_DARKGREEN   5
#define IW_BLUE        6
#define IW_DARKBLUE    7
#define IW_CYAN        8
#define IW_DARKCYAN    9
#define IW_MAGENTA     10
#define IW_DARKMAGENTA 11

#define IW_YELLOW      12
#define IW_DARKYELLOW  13
#define IW_GRAY        14
#define IW_DARKGRAY    15
#define IW_LIGHTGRAY   16
#define IW_TRANSPARENT 17

#define CLK_DIV_001  0
#define CLK_DIV_002  1
#define CLK_DIV_004  2
#define CLK_DIV_008  3
#define CLK_DIV_016  4
#define CLK_DIV_032  8
#define CLK_DIV_064  6
#define CLK_DIV_256  7


enum QQCol
{
    Qt_red=0,
    Qt_green,
    Qt_blue,
    Qt_cyan,
    Qt_magenta,
    Qt_gray,
    Qt_yellow,
    Qt_darkRed,
    Qt_darkGreen,
    Qt_darkBlue,
    Qt_darkCyan,
    Qt_darkMagenta,
    Qt_darkGray,
    Qt_darkYellow,
    Qt_black,
    Qt_lightGray
};

enum
{
    __IW_Sin=0,
    __IW_Cos,
    __IW_Square,
    __IW_Rect1,
    __IW_Rect2,
    __IW_Triangle,
    __IW_Sawtooth1,
    __IW_Sawtooth2,
    __IW_Spike,
    __IW_Raw,
    __IW_Nosig=0x80
};


enum
{
    INOCMD=0,
    ICHANGE_TAB,
    IUSER_TAB,
    ISCOPE_CTRL,
    ISIGNAL_CTRL,
    IINPUT_CAP,
    IOUTPUT_CAP,
    II2C_MASTER,
    IUART_WRITE,
    IUART_READ,
    ILED_CTRL,
    IGPIO_READ,
    IGPIO_WRITE,
    IGRAPH_CTRL,
    ITAG_CTRL=0xEE
};


enum
{
    NOCMD=0,
    CHANGE_TAB,
    CAPTURE_IMG,
    PROCESS_IMG,
    RUN_FILTER,
    APPLY_MASK,
    CAPTURE_N,
    CAPTURE_N_AVG,
    VIEW_BUFFER,
    GRAPH_CTRL,
    DOMATH,
    DOTHRESHOLD,
    INVERTIMG,
    COL2GRAY,
    DO_SIFT
};

#define SCRIPT_PACKET_SIZE 56
typedef union
{
    char           _ch_buff[SCRIPT_PACKET_SIZE];
    unsigned char  _uc_buff[SCRIPT_PACKET_SIZE];

    struct
    {
        unsigned short TMR;
        unsigned short TMRDIV;
        unsigned short numElem;
        unsigned short actChnl;
        unsigned char  buff[50];
    } scope;

    struct
    {
        unsigned short TMR;
        unsigned short TMRDIV;
        unsigned short numElem;

        unsigned short sig1type;
        unsigned short sig1max;
        unsigned short sig1min;

        unsigned short sig2type;
        unsigned short sig2max;
        unsigned short sig2min;

        unsigned char  buff[38];
    } signal;

    struct
    {
        unsigned short tabNum;
        unsigned char  ledNum;
        unsigned char  ledColor;
        unsigned char  plotNum;
        unsigned char  plotPar;
        char           msg[20];
        unsigned char  buff[30];
    } tab;

} ModeData  __attribute__ ((aligned(4)));


#define BLACK       0x0F
#define LIGHTCYAN   0x10

enum
{
    BUS_I2C_ADDOK=0x00,
    BUS_COLL_ERROR=0x01,
    BUS_IDLE_WAIT=0x02,
    BUS_ACK_ERROR=0x03,
    BUS_TRAN_TOERR=0x04,
    BUS_START_ERROR=0x05,
    UNKNOWN_ERROR
};

#define INIT_TAG       0x00
#define TAG            0x01
#define I2C1START      0x01
#define I2C1STOP       0x02
#define I2C1RESTART    0x03
#define I2C1WRITEADDR  0x04
#define I2C1READ       0x05
#define I2C1SPEED      0x06
#define I2C1DATA       0x07
#define I2C1READADDR   0x08
#define I2C2START      0x09
#define I2C2STOP       0x0A
#define I2C2RESTART    0x0B
#define I2C2WRITEADDR  0x0C
#define I2C2READ       0x0D
#define I2C2SPEED      0x0E
#define I2C2DATA       0x0F
#define I2C2READADDR   0x10
#define SET_PWM_RES    0x11
#define SET_PWM_VAL    0x12
#define UART1BAUD      0x13
#define UART1READ      0x14
#define UART1WRITE     0x15
#define UART2BAUD      0x16
#define UART2READ      0x17
#define UART2WRITE     0x18
#define READ_ADC1      0x19
#define READ_ADC2      0x1A
#define WRITE_DAC1     0x1B
#define WRITE_DAC2     0x1C
#define SET_GPIO_DIR   0x1D
#define READ_GPIO      0x1E
#define WRITE_GPIO     0x1F
#define READ_FREQ1     0x20
#define READ_FREQ2     0x21
#define I2C1STATUS     0x22
#define I2C2STATUS     0x23
#define SIGCH1         0x24
#define SIGCH2         0x25
#define SIGMIN         0x26
#define SIGMAX         0x27
#define SIGTYPE        0x28
#define SIGCNT         0x29
#define SIGDIV         0x38
#define SIGELEM        0x39
#define SIGSTART       0x2A

#define GPIOINDEX      0x2B
#define DIR_IN         0x2C
#define DIR_OUT        0x2D
#define GPIO_READ      0x2E
#define GPIO_WRITE     0x2F

#define INCAP_SETTING  0x3A
#define INCAP_RES      0x30
#define INCAP_READ     0x31

#define OUTCAP_SETTING 0x3B
#define OUTCAP_RES     0x32
#define OUTCAP_WRITE   0x33

#define FREQ_RES       0x34
#define FREQ_READ      0x35
#define I2C1_ERROR     0x36
#define I2C2_ERROR     0x37
#define I2C1_CLR_ERR   0x3D
#define I2C2_CLR_ERR   0x3E

#define SOFT_RESET     0xff

// RESPONSE
#define I2C_ADR_ERROR  0x01
#define I2C_COL_ERROR  0x02
#define FREQ_OUT       0x03
#define PWM_OUT        0x04


#define  ID_CH00       0
#define  ID_CH01       1
#define  ID_CH02       2
#define  ID_CH04       4
#define  ID_CH05       5
#define  ID_CH06       6
#define  ID_CH07       7
#define  ID_CH08       8
#define  ID_CH09       9
#define  ID_CH10       9


#endif
