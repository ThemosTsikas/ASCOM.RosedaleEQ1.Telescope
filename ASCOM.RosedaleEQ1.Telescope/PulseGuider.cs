// This file is part of ArduinoAstroControl.
//
// ArduinoAstroControl is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// ArduinoAstroControl is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with ArduinoAstroControl. If not, see <http://www.gnu.org/licenses/>.
//
// Copyright © Guy Webb 2010

using System;
using System.Collections.Generic;
using System.Threading;
using ThreadState=System.Threading.ThreadState;
using ASCOM.DeviceInterface;

namespace ASCOM.RosedaleEQ1
{
    internal class PulseGuider
    {
        /// <summary>
        /// Flag to signify dual axis guiding support.
        /// If this is false then only RA guiding commands will be followed.
        /// </summary>
        private readonly bool m_DualAxis;
        /// <summary>
        /// Pulse guiding instructions may need to be queued, so we have one for each axis.
        /// </summary>
        private readonly Queue<GuidingInstruction>[] m_AxisInstructions;
        /// <summary>
        /// Worker thread for RA axis.
        /// </summary>
        private Thread m_RightAscensionExecutor;
        /// <summary>
        /// Worker thread for DEC axis.
        /// </summary>
        private Thread m_DeclinationExecutor;
        

        /// <param name="dualAxis">If true then both RA and DEC axis can be guided. If false only RA is used.</param>
        public PulseGuider(bool dualAxis)
        {
            m_DualAxis = dualAxis;

            m_AxisInstructions = new Queue<GuidingInstruction>[2];
            m_AxisInstructions[Constants.AXIS_RA] = new Queue<GuidingInstruction>();
            m_AxisInstructions[Constants.AXIS_DEC] = new Queue<GuidingInstruction>();
        }      

        /// <summary>
        /// Register a pulse guide instruction for execution.
        /// </summary>
        /// <param name="direction">The direction of movement.</param>
        /// <param name="duration">The duration of the movement (milliseconds).</param>
      
        public void RegisterInstruction(GuideDirections direction, int duration)
        {
        	GuidingInstruction newInstruction = new GuidingInstruction(direction, duration);             	
        	
            // East / West relates to Right Ascension.
            if (direction == GuideDirections.guideEast || direction == GuideDirections.guideWest)
            {
                lock (m_AxisInstructions[Constants.AXIS_RA])
                {
                    m_AxisInstructions[Constants.AXIS_RA].Enqueue(newInstruction);
                }
                
                // If the Right Ascension executor thread is not currently running, start it.
                if ((m_RightAscensionExecutor == null) || !m_RightAscensionExecutor.IsAlive)
                {
                    m_RightAscensionExecutor = new Thread(ExecuteGuidingQueue);
                    m_RightAscensionExecutor.Start(m_AxisInstructions[Constants.AXIS_RA]);
                }

                // If only the RA axis is being guided we block until the guiding instruction has been completed.
                if (!m_DualAxis)
                    m_RightAscensionExecutor.Join();
            }
            else
            {
                if (m_DualAxis)
                {
                    // Therefore North / Sount is Declination.
                    lock (m_AxisInstructions[Constants.AXIS_DEC])
                    {
                        m_AxisInstructions[Constants.AXIS_DEC].Enqueue(newInstruction);
                    }

                    // If the Declination executor thread is not currently running, start it.
                    if ((m_DeclinationExecutor == null) || !m_DeclinationExecutor.IsAlive)
                    {
                        m_DeclinationExecutor = new Thread(ExecuteGuidingQueue);
                        m_DeclinationExecutor.Start(m_AxisInstructions[Constants.AXIS_DEC]);
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if either of the guiding instruction executors are currently running.
        /// </summary>
        public bool IsPulseGuiding
        {
            get
            {
                return ((m_RightAscensionExecutor != null) && (m_RightAscensionExecutor.ThreadState == ThreadState.Running)) ||
                    ((m_DeclinationExecutor != null) && (m_DeclinationExecutor.ThreadState == ThreadState.Running));
            }
        }

        /// <summary>
        /// Execute any pending guiding instructions queued for a given axis.
        /// </summary>
        /// <param name="instructions">Queue of guiding instructions waiting to be executed.</param>
        private static void ExecuteGuidingQueue(object instructions)
        {
            Queue<GuidingInstruction> queue = instructions as Queue<GuidingInstruction>;

            if (queue == null) return;

            while (queue.Count > 0)
            {
                GuidingInstruction instruction;

                lock (queue)
                {
                    instruction = queue.Dequeue();
                }

                switch(instruction.Direction) {
	        		case GuideDirections.guideEast:
	        			SharedResources.SharedSerial.Transmit("E1");
	                	Thread.Sleep(instruction.Duration);
	                	SharedResources.SharedSerial.Transmit("E0");
	        			break;
	        		case GuideDirections.guideWest:
	        			SharedResources.SharedSerial.Transmit("W1");
	                	Thread.Sleep(instruction.Duration);
	                	SharedResources.SharedSerial.Transmit("W0");
	                	break;
	                case GuideDirections.guideNorth:
	        			SharedResources.SharedSerial.Transmit("N1");
	                	Thread.Sleep(instruction.Duration);
	                	SharedResources.SharedSerial.Transmit("N0");
	                	break;
	                case GuideDirections.guideSouth:
	        			SharedResources.SharedSerial.Transmit("S1");
	                	Thread.Sleep(instruction.Duration);
	                	SharedResources.SharedSerial.Transmit("S0");
	                	break;
	                default:
	                	break;
	        	}
            }
        }
    }
}