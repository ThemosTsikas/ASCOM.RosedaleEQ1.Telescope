// This file is part of RosedaleEQ1.
//
// RosedaleEQ1 is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// RosedaleEQ1 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with RosedaleEQ1. If not, see <http://www.gnu.org/licenses/>.
//
// 
// Copyright © Guy Webb 2010
//
// ================
// Shared Resources
// ================
//
// This class is a container for all shared resources that may be needed
// by the drivers served by the Local Server. 
//
// NOTES:
//
//	* ALL DECLARATIONS MUST BE STATIC HERE!! INSTANCES OF THIS CLASS MUST NEVER BE CREATED!

using ASCOM.Utilities;

namespace ASCOM.RosedaleEQ1 
{
    public class SharedResources
    {
        private static Serial m_SharedSerial;		// Shared serial port
        private static int s_z;

        private SharedResources() { }							// Prevent creation of instances

        static SharedResources()								// Static initialization
        {
            m_SharedSerial = new Serial();
            s_z = 0;
        }

        //
        // Public access to shared resources
        //

        // Shared serial port 
        public static Serial SharedSerial { get { return m_SharedSerial; } }
        public static int z { get { return s_z++; } }
    }
}
