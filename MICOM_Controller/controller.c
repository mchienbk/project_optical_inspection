#include <16F887.h>
#device ADC=10

#FUSES NOWDT                    //No Watch Dog Timer
//#FUSES WDT128                   //Watch Dog Timer uses 1:128 Postscale
#FUSES NOBROWNOUT               //No brownout reset
#FUSES NOLVP                    //No low voltage prgming, B3(PIC16) or B5(PIC18) used for I/O
//#FUSES NOXINST                  //Extended set extension and Indexed Addressing mode disabled (Legacy mode)

#use delay(crystal=20000000)

#use rs232(baud=9600,parity=N,xmit=PIN_C6,rcv=PIN_C7,bits=8)

//Pin Define ===================
#define EnableXY          PIN_D0
#define Dir_X             PIN_D1
#define Dir_Y             PIN_D2
#define Step_X            PIN_D3
#define Step_Y            PIN_D4
#define Home_X            PIN_C0
#define Home_Y            PIN_C2

//Value ========================
#define PacketLength      10
#define StepsBackAtLimit  700

//Registers
#define IsMachineReady    0x61
#define MachineReady      0x62
#define OpenJogMode       0x63
#define JogModeOn         0x64
#define JogModeOff        0x65
#define MovingDone        0x66

#define GoHomeXY          0x70
#define GoHomeX           0x71
#define GoHomeY           0x72


//==============================
#define EmergencyStop     0x80
#define MachineStopped    0x81
#define ErrorLimitSwitch  0x90
#define Home_Switch_X     0x91
#define Limit_Switch_X    0x92
#define Home_Switch_Y     0x93
#define Limit_Switch_Y    0x94

//Function prototypes ========================
void goHome_X(); void goHome_Y();
void constspeed_X(int16 conststep);
void constspeed_Y(int16 conststep);
void constspeed_XY(int16 conststepX, int16 conststepY);
void dly_100us(int8 n);


//Global variables
//int8  rs232_buffer; //Store the return data from getc() 
int8  dataIn[PacketLength]; //Store the incoming data from COM port 
int16 manualModeLocation;
int16 autoModeLocation;
//int1  currentMachineMode;
//int8  axisDelay;



void main()
{
//===========================================================================//
//Setup
   int16  axisStep;
   int8   axisSelect, config;
   int1   axisDir;
   int8   homeSelect;

   output_c(0xFF);
   
   //set_tris_b(0x01);
   set_tris_d(0x00);       //Set all out pin
   set_tris_c(0x00);
   //Starup
   printf("MACHINE_TESTER_V1.0 \r\n");
   delay_ms(100);

  
   //Setup lable
   manualModeLocation = label_address(MANUAL_MODE);
   autoModeLocation = label_address(AUTO_MODE);
   
   //enable_interrupts(INT_RDA);
   //enable_interrupts(GLOBAL);
   disable_interrupts(GLOBAL);


//===========================================================================//
//Wait for start coment

   output_bit(EnableXY, 0);   //Enable 2 motor
   goHome_X();
   goHome_Y();
   while(getc() != IsMachineReady); 
   putc(MachineReady);
//===========================================================================//

MANUAL_MODE:
//currentMachineMode = 0;
   
   dataIn[0] = getc(); // Axis Configuration Byte
   dataIn[1] = getc(); // Axis Step Low Byte
   dataIn[2] = getc(); // Axis Step High Byte

   bit_clear(*0x0B,1); // Clear INTF Flag of INTCONreg
   //if dataIn bit7=0 exit jog mode
   
   if((dataIn[0] == 0x80))
   {
      printf("AUTO_INSPECTION\r\n");
      goto_address(autoModeLocation);
   }
   //else Process data input
   config = dataIn[0];
   axisStep = make16(dataIn[1], dataIn[2]);
   
   homeSelect = config & 0x03;
   
   //Construction control bit
   axisSelect = (config & 0x60) >> 5; //01=X, 10=Y
   axisDir = (config & 0x10) >>4;
   
   // Before movement, enable rs232 and RB0 interrupt for
   // Emergency Stop and Axis Limit Switches.
   //ext_int_edge(H_TO_L);
   //enable_interrupts(INT_EXT);
   //enable_interrupts(INT_RDA);
   //enable_interrupts(GLOBAL);
   //Enable all drivers
   output_bit(EnableXY, 0);   //Enable 2 motor
   //Do the axis movement according to given parameters
   switch (axisSelect) {
   case 0: //0=00b -> Other function
      switch (homeSelect) {
         case 1:
         goHome_X(); putc(MovingDone);
         break;
         case 2:
         goHome_Y(); putc(MovingDone);
         break;
         case 3:
         goHome_X(); goHome_Y(); putc(MovingDone);
         break;
      }
   case 1: // 1=01b -> X-Axis
      //puts("X");
      output_bit(Dir_X, axisDir);
      //constspeed(axisStep,0); // (Stepnumber, axis)
      constspeed_X(axisStep); // (Stepnumber, axis)
   break;
   case 2: // 2=10b -> Y-Axis
      //puts("Y");
      output_bit(Dir_Y, axisDir);
      //constspeed(axisStep,1); // (Stepnumber, axis)
      constspeed_Y(axisStep); // (Stepnumber, axis)
   break;
   }
   goto MANUAL_MODE;
//===========================================================================//

AUTO_MODE:

   dataIn[0] = getc(); // Axis Configuration Byte
   dataIn[1] = getc(); // Axis Step Low Byte
   dataIn[2] = getc(); // Axis Step High Byte

   bit_clear(*0x0B,1); // Clear INTF Flag of INTCONreg
   //if dataIn bit7=0 exit jog mode
   if(dataIn[0] == 0 )
   {
      printf("STOP_INSPECTION\r\n");
      goto MANUAL_MODE;
   }
   //else make the jog movement
   config = dataIn[0];
   axisStep = make16(dataIn[1], dataIn[2]);
   
   //Construction control bit
   axisSelect = (config & 0x60) >> 5; //01=X, 10=Y
   axisDir = (config & 0x10) >>4;
   
   // Before movement, enable rs232 and RB0 interrupt for
   // Emergency Stop and Axis Limit Switches.
   //ext_int_edge(H_TO_L);
   //enable_interrupts(INT_EXT);
   //enable_interrupts(INT_RDA);
   //enable_interrupts(GLOBAL);
   //Enable all drivers
   
   output_bit(EnableXY, 0);   //Enable 2 motor
   //delay_ms(50);
   //Do the axis movement according to given parameters
   switch (axisSelect)
   {
      case 0: //0=00b -> Other function
         config = config && 0x0F;
         switch (config) {
            case 1:
            goHome_X(); putc(MovingDone);
            break;
            case 2:
            goHome_Y(); putc(MovingDone);
            break;
            case 3:
            goHome_X(); goHome_Y(); putc(MovingDone);
            break;
         }   
      case 1: // 1=01b -> X-Axis
         //puts("X");
         output_bit(Dir_X, axisDir);
         //constspeed(axisStep,0); // (Stepnumber, axis)
         constspeed_X(axisStep); // (Stepnumber, axis)
      break;
      case 2: // 2=10b -> Y-Axis
         //puts("Y");
         output_bit(Dir_Y, axisDir);
         //constspeed(axisStep,1); // (Stepnumber, axis)
         constspeed_Y(axisStep); // (Stepnumber, axis)
      break;
      }
      delay_ms(250);
      //Movement complete
      putc(MovingDone);
      //disable_interrupts(GLOBAL);
      goto AUTO_MODE;

} // End of Main()
//===========================================================================//

void constspeed_X(int16 conststep)
{
   int16 i;
   for (i=0; i<conststep; i++)
   {
      output_bit(Step_X,1);
      delay_us(30);
      output_bit(Step_X,0);
      delay_us(30);
   }
   output_bit(Step_X,1);   // Last step
   delay_us(100);
} // End of constspeed()

void constspeed_Y(int16 conststep)
{
   int16 i;
   for (i=0; i<conststep; i++)
   {
      output_bit(Step_Y,1);
      delay_us(30);
      output_bit(Step_Y,0);
      delay_us(30);
   }
   output_bit(Step_Y,1);   // Last step
   delay_us(100);
} // End of constspeed()

void goHome_X()
{
   int16 i;
   output_bit(Dir_X, 0);
   
   while (input(Home_X)) {
      output_bit(Step_X,1);
      delay_us(30);
      output_bit(Step_X,0);
      delay_us(30);
   }
   delay_us(100);
   //Run out 1mm
   //output_bit(Dir_X, 1);
   //for (i=0; i<50; i++) {
   //   output_bit(Step_X,1);
   //   delay_us(500);
   //   output_bit(Step_X,0);
   //   delay_us(500);
   //}
   //delay_us(100);
   //printf("HOMEX \r\n");
} // End of Home_X()

void goHome_Y()
{
   int16 i;
   output_bit(Dir_Y, 0);
   while (input(Home_Y)) {
      output_bit(Step_Y,1);
      delay_us(30);
      output_bit(Step_Y,0);
      delay_us(30);
   }
   delay_us(100);
   //Run out 1mm
   //output_bit(Dir_Y, 1);
   //for (i=0; i<50; i++) {
   //   output_bit(Step_Y,1);
   //   delay_us(50);
   //   output_bit(Step_Y,0);
   //   delay_us(50);
   //}
   //delay_us(100);
   //printf("HOMEY \r\n");
} // End of Home_Y()

