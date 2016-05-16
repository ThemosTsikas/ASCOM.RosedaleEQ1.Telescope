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
using ASCOM.DeviceInterface;

namespace ASCOM.RosedaleEQ1
{
	/// <summary>
    /// Simple representation for pulse guiding instruction queues.
    /// </summary>
    internal class GuidingInstruction
    {
        private readonly GuideDirections m_direction;
        private readonly int m_duration;

        public GuidingInstruction(GuideDirections direction, int duration)
        {
            m_direction = direction;
            m_duration = duration;
            
        }

        public GuideDirections Direction
        {
            get { return m_direction; }
        }

        public int Duration
        {
            get { return m_duration; }
        }
        
    }
}
