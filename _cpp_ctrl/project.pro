TEMPLATE = app
QT      += network opengl
CONFIG  += console

QMAKE_CXXFLAGS += -std=gnu++0x

DESTDIR      = bin
MOC_DIR      = moc
RCC_DIR      = resources
OBJECTS_DIR  = obj

TEMPLATE      = app
CONFIG       += release
QMAKE_CXXFLAGS_RELEASE -= -O2

#Modify this line according to target
LIBS += -L../Oscilloscope/ubuntu_intel_64 -lfixed_lib
#LIBS += -L../Oscilloscope/ubuntu_intel_32 -lfixed_lib
#LIBS += -L../Oscilloscope/ubuntu_arm -lfixed_lib
#LIBS += -L../Oscilloscope/windows_intel -lfixed_lib

TARGET   = epc
SOURCES += main.cpp

