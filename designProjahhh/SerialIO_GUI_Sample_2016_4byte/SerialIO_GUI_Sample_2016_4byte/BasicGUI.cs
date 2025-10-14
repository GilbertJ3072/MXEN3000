// Curtin University
// Mechatronics Engineering
// Serial I/O Card - Sample GUI Code

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Reflection;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;

namespace SerialGUISample
{

    public partial class Form1 : Form
    {
        float freakyThang = 0.6f;
        int freakyBasespeed = 100;
        byte freakyAdjustment = 0;
        bool freakyFlipFlop = false;
        int freakyDelay = 300;


        // Declare variables to store inputs and outputs.
        bool runSerial = true;
        bool byteRead = false;
        int Input1 = 0;
        int Input2 = 0;

        byte[] Outputs = new byte[4];
        byte[] Inputs = new byte[4];

        const byte START = 255;
        const byte ZERO = 0;

        // things Finn has added - Errors and trouble shoot here
        private const int WHITE = 185; // change for white value on day? maybe make in GUI?
        private const byte forward = 170;
        private const byte ckward = (70); //same range forwards and back
        int cycleCount = 0;
        //PID control
        float error = 0;
        float kp = 33;
        float ki = 0.007f;
        float kd = 0.04f;
        int baseSpeed = 170; //slow work -> kp = 35, ki = 0.007, kd = 0.04, base Speed = 170 or 205 with kp = 33
                             // 33, 0.007, 0.06, base speed = 190
        float PID_influence = 1.0f;

        float previousError = 0;
        float integral = 0;
        float maxIntegral = 250;
        int PID;
        //average sensors
        int Input12, Input13, Input14, Input15, Input1Av;
        int Input22, Input23, Input24, Input25, Input2Av;
        //
        int lMin = 155, rMin = 130;
        int lMax = 235, rMax = 230;
        float leftMap, rightMap;
        int dt = 20; //ms measure - min = 10ms (tick speed)
        float derivative = 0, d1 = 0, d2 = 0, d3 = 0, d4 = 0;


        private enum ControlMode
        {
            Manual,
            PID,
            BangBang,
            Proportional,
            PI
        }
        private ControlMode currentMode = ControlMode.Manual;


        public Form1()
        {
            // Initialize required for form controls.
            InitializeComponent();
            this.KdBox.Text = kd.ToString();
            this.KpBox.Text = kp.ToString();
            this.KiBox.Text = ki.ToString();
            this.Integral.Text = integral.ToString();

            this.KdBox.TextChanged += (s, e) => float.TryParse(KdBox.Text, out kd);
            this.KpBox.TextChanged += (s, e) => float.TryParse(KpBox.Text, out kp);
            this.KiBox.TextChanged += (s, e) => float.TryParse(KiBox.Text, out ki);

            // Establish connection with serial
            //if (runSerial == true)
            if (runSerial == true)
            {
                if (!serial.IsOpen)                                  // Check if the serial has been connected.
                {
                    try
                    {
                        serial.Open();                               //Try to connect to the serial.
                    }
                    catch
                    {
                        statusBox.Enabled = false;
                        statusBox.Text = "ERROR: Failed to connect.";     //If the serial does not connect return an error.
                    }
                }
            }
        }

        private void KdBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Error2_TextChanged(object sender, EventArgs e)
        {

        }

        //click butotn
        private void ToggleModeButton_Click(object sender, EventArgs e) //Finn made this (may break)
        {
            //cycle through enum
            currentMode = (ControlMode)(((int)currentMode + 1) % Enum.GetValues(typeof(ControlMode)).Length);

            //update button text
            toggleModeButton.Text = currentMode.ToString();
        }
        private void resetIntegral_Click(object sender, EventArgs e)
        {
            integral = 0;
        }

        private void Error1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void reCallibrate_Click(object sender, EventArgs e) //recallibrate Lmin,Lmax etc automatically
        {
            int currLMin, currLMax, currRMin, currRMax; //temp values

            currLMin = Input1;
            currLMax = Input1;
            currRMin = Input2;
            currRMax = Input2;

            reCallibrate.Text = "recallibrating... beep boop"; // change text
            reCallibrate.BackColor = Color.Red;
            LMinBox.BackColor = Color.Red;
            LMaxBox.BackColor = Color.Red;
            RMinBox.BackColor = Color.Red;
            RMaxBox.BackColor = Color.Red;

            for (int i = 0; i < 500; i++)
            {
                if (i % 20 < 10)
                    reCallibrate.BackColor = Color.YellowGreen;
                else
                    reCallibrate.BackColor = Color.Red;

                await Task.Delay(12); //delay for sensors to update

                //compare to current data
                if (Input1 < currLMin)
                    currLMin = Input1;
                else if (Input1 > currLMax)
                    currLMax = Input1;
                if (Input2 < currRMin)
                    currRMin = Input2;
                else if (Input2 > currRMax)
                    currRMax = Input2;

                LMinBox.Text = currLMin.ToString();
                LMaxBox.Text = currLMax.ToString();
                RMinBox.Text = currRMin.ToString();
                RMaxBox.Text = currRMax.ToString();
            }

            reCallibrate.Text = "recalibrate";
            reCallibrate.BackColor = Color.Yellow;
            LMinBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            LMaxBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            RMinBox.BackColor = Color.Orange;
            RMaxBox.BackColor = Color.Orange;

            //clear integral
            integral = 0;



            //update values
            lMin = currLMin - 2;
            lMax = currLMax + 2;
            rMin = currRMin - 2;
            rMax = currRMax + 2;
            /*if(lMax > 255) //some rounding - maybe not needed idk
                lMax = 255;
            if (rMax > 255)
                rMax = 255;*/

            //update GUI
            LMinBox.Text = lMin.ToString();
            LMaxBox.Text = lMax.ToString();
            RMinBox.Text = rMin.ToString();
            RMaxBox.Text = rMax.ToString();

        }
        // Send a four byte message to the Arduino via serial.
        private void sendIO(byte PORT, byte DATA)
        {
            Outputs[0] = START;    //Set the first byte to the start value that indicates the beginning of the message.
            Outputs[1] = PORT;     //Set the second byte to represent the port where, Input 1 = 0, Input 2 = 1, Output 1 = 2 & Output 2 = 3. This could be enumerated to make writing code simpler... (see Arduino driver)
            Outputs[2] = DATA;  //Set the third byte to the value to be assigned to the port. This is only necessary for outputs, however it is best to assign a consistent value such as 0 for input ports.
            Outputs[3] = (byte)(START + PORT + DATA); //Calculate the checksum byte, the same calculation is performed on the Arduino side to confirm the message was received correctly.

            if (serial.IsOpen)
            {
                serial.Write(Outputs, 0, 4);         //Send all four bytes to the IO card.                      
            }
        }

        private void Send1_Click(object sender, EventArgs e) //Press the button to send the value to Output 1, Arduino Port A.
        {
            if (currentMode == ControlMode.Manual) // only trigger if in manual mode
            {
                byte code = 127;

                double pwm = (double)OutputBox1.Value; //user inputs pwm
                double codeDouble = 255.0 * (pwm / 100.0); //remaps pwm to a code

                if (codeDouble < 0) codeDouble = 0;
                if (codeDouble > 255) codeDouble = 255; //restrict values (avoid overflow/underflow)


                int codeInt = (int)Math.Round(codeDouble, MidpointRounding.AwayFromZero);

                code = (byte)codeInt; //convert code to a sendable byte


                sendIO(2, code); // The value 2 indicates Output1, value for output set in OutputBox1.
            }
        }

        private void Send2_Click(object sender, EventArgs e) //Press the button to send the value to Output 2, Arduino Port C.
        {
            if (currentMode == ControlMode.Manual)
            {
                byte code = 127; // stop by default

                double pwm = (double)OutputBox1.Value; //user inputs pwm
                double codeDouble = 255.0 * (pwm / 100.0); //remaps pwm to a code

                if (codeDouble < 0) codeDouble = 0;
                if (codeDouble > 255) codeDouble = 255; //restrict values (avoid overflow/underflow)


                int codeInt = (int)Math.Round(codeDouble, MidpointRounding.AwayFromZero);

                code = (byte)codeInt; //convert code to a sendable byte

                sendIO(3, code); // The value 2 indicates Output1, value for output set in OutputBox1.
            }

        }

        private void Get1_Click(object sender, EventArgs e) //Press the button to request value from Input 1, Arduino Port F.
        {
            sendIO(0, ZERO);  // The value 0 indicates Input 1, ZERO just maintains a fixed value for the discarded data in order to maintain a consistent package format.
        }

        private void Get2_Click(object sender, EventArgs e) //Press the button to request value from Input 1, Arduino Port K.
        {
            sendIO(1, ZERO);  // The value 1 indicates Input 2, ZERO maintains a consistent value for the message output.
        }

        private void getIOtimer_Tick(object sender, EventArgs e) //It is best to continuously check for incoming data as handling the buffer or waiting for event is not practical in C#.
        {
            this.FlipBox.Text = freakyFlipFlop.ToString(); //update freaky flipflop GUI text
            
            error = leftMap - rightMap; //reCalc error and PID
            PID = (int)calculatePID(error);

            if (cycleCount == 0)
            {
                sendIO(0, ZERO);
                sendIO(1, ZERO);
            }
            cycleCount = (cycleCount + 1) % (100 / dt);

            if (serial.IsOpen) //Check that a serial connection exists.
            {
                if (serial.BytesToRead >= 4) //Check that the buffer contains a full four byte package.
                {
                    //statusBox.Text = "Incoming"; // A status box can be used for debugging code.
                    Inputs[0] = (byte)serial.ReadByte(); //Read the first byte of the package.

                    if (Inputs[0] == START) //Check that the first byte is in fact the start byte.
                    {
                        //statusBox.Text = "Start Accepted";

                        //Read the rest of the package.
                        Inputs[1] = (byte)serial.ReadByte();
                        Inputs[2] = (byte)serial.ReadByte();
                        Inputs[3] = (byte)serial.ReadByte();

                        //Calculate the checksum.
                        byte checkSum = (byte)(Inputs[0] + Inputs[1] + Inputs[2]);

                        //Check that the calculated check sum matches the checksum sent with the message.
                        if (Inputs[3] == checkSum)
                        {
                            //statusBox.Text = "CheckSum Accepted";

                            //Check which port the incoming data is associated with.
                            switch (Inputs[1])
                            {
                                case 0: //Save the data to a variable and place in the textbox.
                                    //statusBox.Text = "Input1";
                                    Input15 = Input14;
                                    Input14 = Input13;
                                    Input13 = Input12;
                                    Input12 = Input1;
                                    Input1 = Inputs[2];
                                    //Input1Av = (Input1 + Input12 + Input13 + Input14 + Input15) / 5;
                                    InputBox1.Text = Input1.ToString();
                                    leftMap = ((float)(lMax - Input1)) / ((float)(lMax - lMin));
                                    break;
                                case 1: //Save the data to a variable and place in the textbox.
                                        //statusBox.Text = "Input2";
                                    Input25 = Input24;
                                    Input24 = Input23;
                                    Input23 = Input22;
                                    Input22 = Input2;
                                    Input2 = Inputs[2];
                                    //Input2Av = (Input2 + Input22 + Input23 + Input24 + Input25) / 5;
                                    InputBox2.Text = Input2.ToString();
                                    rightMap = ((float)(rMax - Input2)) / ((float)(rMax - rMin));
                                    break;
                            }
                        }
                    }
                }
            }

            if (currentMode != ControlMode.Manual)
            {
                leftContControl();
                rightContControl();
            }
        }

        private void OutputBox1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void leftContControl() //left continuous controls
        {
            byte code = 127;
            int codeInt = 127;
            // byte code;

            switch (currentMode)
            {

                case ControlMode.BangBang: //this is the whole bangbang - no seperate motor control
                    {
                        if (freakyFlipFlop) //correct for only right
                        {
                            if(error < -freakyThang)
                            {
                                codeInt = 255 - freakyAdjustment;
                                code = (byte) codeInt;
                                sendIO(3, code);
                                sendIO(2, (byte)freakyAdjustment);
                                Thread.Sleep(freakyDelay); // crank it for 100 ms
                                sendIO(2, (byte)freakyBasespeed);
                                sendIO(3, (byte)freakyBasespeed); // stop motors after crank

                                freakyFlipFlop = !freakyFlipFlop;

                            }
                            else
                            {
                                codeInt = freakyBasespeed;
                            }
                        }

                        else // correct only for left
                        {
                            if (error > freakyThang)
                            {
                                codeInt = 255 - freakyAdjustment;
                                code = (byte)codeInt;
                                sendIO(2, code);
                                sendIO(3, (byte)freakyAdjustment);
                                Thread.Sleep(freakyDelay); // crank it for 100 ms
                                sendIO(2, (byte)freakyBasespeed);
                                sendIO(3, (byte)freakyBasespeed); // stop motors after crank

                                freakyFlipFlop = !freakyFlipFlop;

                            }
                            else
                            {
                                codeInt = freakyBasespeed;
                            }
                        }

                        break;
                    }

                case ControlMode.Proportional:
                    // proportional control code

                    break;

                case ControlMode.PI:
                    // PI control code
                    break;

                case ControlMode.PID:
                    // PID control code
                    codeInt = (int)(baseSpeed - 0.8 * PID);

                    break;
            }


            if (codeInt > 250) // some rounding stuff
                codeInt = 250;
            if (codeInt < 5)
                codeInt = 5;
            code = (byte)codeInt;

            sendIO(2, code);
        }

        private void rightContControl() //right continuous controls
        {
            byte code = 127;
            int codeInt = 127;

            switch (currentMode)
            {

                case ControlMode.Proportional:
                    //insert the code stuff here
                    break;

                case ControlMode.PI:
                    //insert the code stuff here
                    break;

                case ControlMode.PID:
                    codeInt = (int)(baseSpeed + 0.8 * PID);

                    break;

            }


            if (codeInt > 250)
                codeInt = 250;
            if (codeInt < 5)
                codeInt = 5;
            code = (byte)codeInt;
            if(currentMode != ControlMode.BangBang)
                sendIO(3, code); // The value 2 indicates Output1, value for output set in OutputBox1.
        }

        private float calculatePID(float error)
        {
            float output;
            float derivativeAv;
            d4 = d3;
            d3 = d2;
            d2 = d1;
            d1 = derivative;

            derivative = (error - previousError) / dt;

            derivativeAv = (d4 + d3 + d2 + d1 + derivative) / 5;
            integral += error * dt;
            if (integral > maxIntegral)
            {
                integral = maxIntegral;
            }
            if (integral < -maxIntegral)
            {
                integral = -maxIntegral;
            }
            Integral.Text = integral.ToString();

            output = (kp * error) + (ki * integral) + (kd * derivativeAv);
            if (output >= 250)
                output = 250;

            previousError = error;
            Error1.Text = error.ToString();
            // InputBox2.Text = error.ToString();
            return PID_influence * output;
        }

    }


}
