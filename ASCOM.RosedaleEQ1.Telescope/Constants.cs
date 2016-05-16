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

using System;

namespace ASCOM.RosedaleEQ1
{
	/// <summary>
	/// Description of Constants.
	/// </summary>
	internal class Constants
	{
		/// <summary>
        /// Sidereal rate expressed as degrees per second.
        /// </summary>
        public const double SIDEREAL_DEGREES_PER_SEC = 0.004166666665;

        /// <summary>
        /// Conversion constant to turn the degrees, minutes and seconds coordinates to the more 
        /// usual hours, minutes and seconds used for right ascension.
        /// </summary>
        public const double DEGREES_PER_HOUR_OF_RA = 15;
        
        /// <summary>
        /// Common index for arrays.
        /// </summary>
		public const int AXIS_RA = 0;
        /// <summary>
        /// Common index for arrays.
        /// </summary>
        public const int AXIS_DEC = 1;
	}
}