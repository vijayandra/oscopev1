TEMPLATE = app
QT      += network opengl
CONFIG  += console

QMAKE_CXXFLAGS += -std=gnu++0x
QMAKE_CXXFLAGS += -U__STRICT_ANSI__

DESTDIR      = bin
MOC_DIR      = moc
RCC_DIR      = resources
OBJECTS_DIR  = obj

DEFINES      += COMMAND_LINE_USE

unix {
DEFINES      += LINUX_WAY
INCLUDEPATH  += ../common
}

win32 {
DEFINES += WINDOWS_WAY
INCLUDEPATH += c:/sw/opencv/include
INCLUDEPATH += c:/sw/opencv/include/opencv
INCLUDEPATH += c:/sw/opencv/include/opencv2
LIBS += "c:/sw/opencv/x86/mingw/bin/*.dll"
LIBS += "c:/sw/opencv/x86/mingw/bin/*.dll"
INCLUDEPATH  += ../common
}


unix {
    DEFINES += LINUX_WAY
    #DEFINES += FORCE_STATIC_LIB
    INCLUDEPATH += /usr/include
    INCLUDEPATH += /usr/include/qt4/wwWidgets
    INCLUDEPATH += /usr/include/opencv
    INCLUDEPATH += /usr/include/opencv2
    LIBS += -lgsl
    LIBS += -lgslcblas
}

win32 {
    DEFINES += WINDOWS_WAY
}

TEMPLATE      = app
CONFIG       += wwwidgets
CONFIG       += release
QMAKE_CXXFLAGS_RELEASE -= -O2

#Modify this line according to target

LIBS += -L../Oscilloscope/ubuntu_intel_64 -lfixed_lib

TARGET   = epc
SOURCES += main.cpp

